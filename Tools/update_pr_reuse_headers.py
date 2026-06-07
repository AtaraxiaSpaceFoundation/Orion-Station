#!/usr/bin/env python3
# SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later


import argparse
import re
import subprocess
from dataclasses import dataclass
from datetime import datetime, timezone
from pathlib import Path

# Orion-Edit: Heavily

LICENSE_CONFIG = {
    "agpl": {"id": "AGPL-3.0-or-later", "path": "LICENSES/AGPL-3.0-or-later.txt"},
    "mit-wizards": {"id": "MIT-WIZARDS", "path": "LICENSES/MIT-WIZARDS.txt"},
    "mit": {"id": "MIT-GOOB", "path": "LICENSES/MIT-GOOB.txt"},
    "mpl": {"id": "MPL-2.0", "path": "LICENSES/MPL-2.0.txt"},
    "mpl-no-copyleft": {
        "id": "MPL-2.0-no-copyleft-exception",
        "path": "LICENSES/MPL-2.0-no-copyleft-exception.txt",
    },
}

DEFAULT_LICENSE_LABEL = "agpl"
BROKEN_AUTOMATION_AUTHOR = "Space Station 14 Contributors"
BROKEN_AUTOMATION_LICENSE = "MIT-WIZARDS"

COMMENT_STYLES = {
    # C-style single-line comments
    ".cs": ("//", None),
    ".js": ("//", None),
    ".ts": ("//", None),
    ".jsx": ("//", None),
    ".tsx": ("//", None),
    ".c": ("//", None),
    ".cpp": ("//", None),
    ".cc": ("//", None),
    ".h": ("//", None),
    ".hpp": ("//", None),
    ".java": ("//", None),
    ".scala": ("//", None),
    ".kt": ("//", None),
    ".swift": ("//", None),
    ".go": ("//", None),
    ".rs": ("//", None),
    ".dart": ("//", None),
    ".groovy": ("//", None),
    ".php": ("//", None),

    # Hash-style single-line comments
    ".yaml": ("#", None),
    ".yml": ("#", None),
    ".ftl": ("#", None),
    ".py": ("#", None),
    ".rb": ("#", None),
    ".pl": ("#", None),
    ".pm": ("#", None),
    ".sh": ("#", None),
    ".bash": ("#", None),
    ".zsh": ("#", None),
    ".fish": ("#", None),
    ".ps1": ("#", None),
    ".r": ("#", None),
    ".rmd": ("#", None),
    ".jl": ("#", None),
    ".tcl": ("#", None),
    ".perl": ("#", None),
    ".conf": ("#", None),
    ".toml": ("#", None),
    ".ini": ("#", None),
    ".cfg": ("#", None),
    ".gitignore": ("#", None),
    ".dockerignore": ("#", None),

    # Other single-line comment styles
    ".bat": ("REM", None),
    ".cmd": ("REM", None),
    ".vb": ("'", None),
    ".vbs": ("'", None),
    ".bas": ("'", None),
    ".asm": (";", None),
    ".s": (";", None),
    ".lisp": (";", None),
    ".clj": (";", None),
    ".f": ("!", None),
    ".f90": ("!", None),
    ".m": ("%", None),
    ".sql": ("--", None),
    ".ada": ("--", None),
    ".adb": ("--", None),
    ".ads": ("--", None),
    ".hs": ("--", None),
    ".lhs": ("--", None),
    ".lua": ("--", None),

    # Multi-line comment styles
    ".xaml": ("<!--", "-->"),
    ".xml": ("<!--", "-->"),
    ".html": ("<!--", "-->"),
    ".htm": ("<!--", "-->"),
    ".svg": ("<!--", "-->"),
    ".css": ("/*", "*/"),
    ".scss": ("/*", "*/"),
    ".sass": ("/*", "*/"),
    ".less": ("/*", "*/"),
    ".md": ("<!--", "-->"),
    ".markdown": ("<!--", "-->"),
    ".mdc": ("<!--", "-->"),
}

AUTOMATED_COMMIT_SUBJECTS = {"chore: automatically update reuse headers"}
BOT_MARKERS = (
    "[bot]",
    "github actions",
    "github-actions",
    "dependabot",
    "renovate",
    "coderabbit",
    "codex",
    "copilot",
    "devin",
    "orion-github",
    "automation",
    " ci ",
    "token",
)
CO_AUTHOR_RE = re.compile(r"^Co-authored-by:\s*(.+?)\s*<([^>]+)>\s*$", re.IGNORECASE | re.MULTILINE)
COPYRIGHT_RE = re.compile(r"^SPDX-FileCopyrightText:\s*(\d{4})(?:-(\d{4}))?\s+(.+?)\s*$")
LICENSE_RE = re.compile(r"^SPDX-License-Identifier:\s*(.+?)\s*$")


@dataclass(frozen=True)
class Author:
    name: str
    email: str

    def spdx(self):
        return f"{self.name} <{self.email}>" if self.email else self.name


def run_git(repo, *args, check=True):
    result = subprocess.run(
        ["git", *args], cwd=repo, text=True, encoding="utf-8", errors="replace",
        capture_output=True, check=False,
    )
    if check and result.returncode:
        raise RuntimeError(result.stderr.strip() or f"git {' '.join(args)} failed")
    return result.stdout


def is_bot(author):
    value = f" {author.name} {author.email} ".lower()
    return not author.name.strip() or author.name.strip().lower() == "unknown" or any(marker in value for marker in BOT_MARKERS)


def add_author(authors, author, year):
    if is_bot(author):
        return
    key = author.spdx()
    first, last = authors.get(key, (year, year))
    authors[key] = (min(first, year), max(last, year))


def authors_by_file_from_git(repo, file_paths, base_sha, head_sha, fallback=None):
    authors_by_file = {file_path: {} for file_path in file_paths}
    if base_sha and head_sha and file_paths:
        output = run_git(
            repo,
            "log",
            f"{base_sha}..{head_sha}",
            "--format=%x1e%aI%x1f%an%x1f%ae%x1f%B%x1d",
            "--name-status",
            "--find-renames",
            "--",
            *file_paths,
            check=False,
        )
        for record in output.split("\x1e"):
            metadata, separator, names = record.partition("\x1d")
            if not separator:
                continue
            fields = metadata.strip().split("\x1f", 3)
            if len(fields) != 4:
                continue
            date, name, email, body = fields
            subject = body.splitlines()[0].strip().lower() if body.splitlines() else ""
            if subject in AUTOMATED_COMMIT_SUBJECTS:
                continue
            try:
                year = int(date[:4])
            except ValueError:
                continue
            commit_authors = [Author(name.strip(), email.strip())]
            commit_authors.extend(Author(co_name.strip(), co_email.strip()) for co_name, co_email in CO_AUTHOR_RE.findall(body))
            for line in names.splitlines():
                fields = line.split("\t")
                if len(fields) < 2:
                    continue
                file_path = fields[-1]
                if file_path not in authors_by_file:
                    continue
                for author in commit_authors:
                    add_author(authors_by_file[file_path], author, year)
    if fallback:
        year = datetime.now(timezone.utc).year
        for authors in authors_by_file.values():
            if not authors:
                add_author(authors, fallback, year)
    return authors_by_file

def merge_authors(existing, added):
    merged = dict(existing)
    for author, (first, last) in added.items():
        old_first, old_last = merged.get(author, (first, last))
        merged[author] = (min(first, old_first), max(last, old_last))
    return merged

def format_years(first, last):
    return str(first) if first == last else f"{first}-{last}"

def comment_style(file_path):
    return COMMENT_STYLES.get(Path(file_path).suffix.lower())


def preamble_end(lines, style, file_path=None):
    if not lines:
        return 0
    suffix = Path(file_path).suffix.lower() if file_path else ""
    if suffix in {".md", ".markdown", ".mdc"} and lines[0].strip() == "---":
        for index in range(1, len(lines)):
            if lines[index].strip() == "---":
                return index + 1
    if style[1] and lines[0].lstrip().startswith("<?xml"):
        return 1
    if style[0] == "#" and lines[0].startswith("#!"):
        return 1
    return 0


def parse_spdx_value(value, authors, license_id):
    copyright_match = COPYRIGHT_RE.match(value)
    if copyright_match:
        first = int(copyright_match.group(1))
        last = int(copyright_match.group(2) or copyright_match.group(1))
        authors[copyright_match.group(3)] = (first, last)
    license_match = LICENSE_RE.match(value)
    if license_match:
        license_id = license_match.group(1)
    return bool(copyright_match or license_match), license_id


def parse_header(content, style, file_path=None):
    lines = content.splitlines()
    index = preamble_end(lines, style, file_path)
    while index < len(lines) and not lines[index].strip():
        index += 1
    start = index
    prefix, suffix = style
    authors = {}
    license_id = None
    saw_spdx = False

    if suffix is None:
        while index < len(lines):
            line = lines[index]
            if not line.startswith(prefix):
                break
            value = line[len(prefix):].strip()
            matched, license_id = parse_spdx_value(value, authors, license_id)
            if value and not matched:
                break
            saw_spdx = saw_spdx or matched
            index += 1
        return authors, license_id, start, index if saw_spdx else start

    while index < len(lines):
        line = lines[index].strip()
        if line == prefix:
            block_index = index + 1
            block_authors = {}
            block_license = None
            block_saw_spdx = False
            while block_index < len(lines) and lines[block_index].strip() != suffix:
                matched, block_license = parse_spdx_value(lines[block_index].strip(), block_authors, block_license)
                block_saw_spdx = block_saw_spdx or matched
                block_index += 1
            if block_index >= len(lines) or not block_saw_spdx:
                break
            authors.update(block_authors)
            license_id = block_license or license_id
            saw_spdx = True
            index = block_index + 1
        elif line.startswith(prefix) and line.endswith(suffix):
            value = line[len(prefix):-len(suffix)].strip()
            matched, license_id = parse_spdx_value(value, authors, license_id)
            if not matched:
                break
            saw_spdx = True
            index += 1
        else:
            break
        while index < len(lines) and not lines[index].strip():
            index += 1

    return authors, license_id, start, index if saw_spdx else start

def create_header(authors, license_id, style):
    prefix, suffix = style
    copyright_lines = [
        f"SPDX-FileCopyrightText: {format_years(first, last)} {author}"
        for author, (first, last) in sorted(authors.items(), key=lambda item: (item[1][0], item[0].casefold()))
    ]
    if suffix is None:
        return "\n".join([*(f"{prefix} {line}" for line in copyright_lines), prefix, f"{prefix} SPDX-License-Identifier: {license_id}"])
    return "\n".join([prefix, *copyright_lines, "", f"SPDX-License-Identifier: {license_id}", suffix])

def update_content(content, style, authors, license_id, file_path=None):
    _, _, start, end = parse_header(content, style, file_path)
    lines = content.splitlines()
    preamble = preamble_end(lines, style, file_path)
    body_start = end if end > start else preamble
    while body_start < len(lines) and not lines[body_start].strip():
        body_start += 1
    header_lines = create_header(authors, license_id, style).splitlines()
    new_lines = lines[:preamble]
    if new_lines:
        new_lines.append("")
    new_lines.extend(header_lines)
    if body_start < len(lines):
        new_lines.append("")
        new_lines.extend(lines[body_start:])
    return "\n".join(new_lines) + "\n"

def validate_license_files(repo):
    for config in LICENSE_CONFIG.values():
        if not (Path(repo) / config["path"]).is_file():
            raise RuntimeError(f"Missing license file: {config['path']}")

def process_file(repo, file_path, explicit_license, git_authors, dry_run=False):
    style = comment_style(file_path)
    path = Path(repo) / file_path
    if not style or not path.is_file() or path.is_symlink():
        return False

    raw_content = path.read_bytes()
    has_bom = raw_content.startswith(b"\xef\xbb\xbf")
    content = raw_content.decode("utf-8-sig", errors="replace")
    existing_authors, existing_license, _, _ = parse_header(content, style, file_path)

    broken_header = existing_license == BROKEN_AUTOMATION_LICENSE and set(existing_authors) == {BROKEN_AUTOMATION_AUTHOR}
    if broken_header:
        existing_authors = {}
        existing_license = None

    authors = merge_authors(existing_authors, git_authors)
    if not authors:
        print(f"Skipping {file_path}: no real author found")
        return False

    # Existing upstream licenses are preserved. An explicit PR choice controls new or unlicensed files only.
    license_label = explicit_license or DEFAULT_LICENSE_LABEL
    license_id = existing_license or LICENSE_CONFIG[license_label]["id"]
    new_content = update_content(content, style, authors, license_id, file_path)
    if new_content == content:
        return False
    action = "Would update" if dry_run else "Updated"
    print(f"{action} {file_path}: {license_id}; {', '.join(authors)}")
    if not dry_run:
        encoded = new_content.encode("utf-8")
        path.write_bytes((b"\xef\xbb\xbf" if has_bom else b"") + encoded)
    return True

def changed_files(repo, base_sha, head_sha):
    output = run_git(repo, "diff", "--name-status", "--find-renames", f"{base_sha}..{head_sha}")
    added, modified = [], []
    for line in output.splitlines():
        fields = line.split("\t")
        status = fields[0]
        path = fields[-1]
        if status == "A":
            added.append(path)
        elif status.startswith(("M", "R")):
            modified.append(path)
    return added, modified

def main():
    parser = argparse.ArgumentParser(description="Update REUSE headers for files changed by a pull request")
    parser.add_argument("--files-added", nargs="*", default=[])
    parser.add_argument("--files-modified", nargs="*", default=[])
    parser.add_argument("--pr-license", choices=sorted(LICENSE_CONFIG))
    parser.add_argument("--pr-base-sha")
    parser.add_argument("--pr-head-sha")
    parser.add_argument("--pr-author-name")
    parser.add_argument("--pr-author-email")
    parser.add_argument("--repo", default=".")
    parser.add_argument("--check", action="store_true", help="Report required updates without modifying files")
    args = parser.parse_args()

    validate_license_files(args.repo)
    added, modified = args.files_added, args.files_modified
    if not added and not modified and args.pr_base_sha and args.pr_head_sha:
        added, modified = changed_files(args.repo, args.pr_base_sha, args.pr_head_sha)
    file_paths = list(dict.fromkeys([*added, *modified]))
    fallback = Author(args.pr_author_name, args.pr_author_email or "") if args.pr_author_name else None
    authors_by_file = authors_by_file_from_git(args.repo, file_paths, args.pr_base_sha, args.pr_head_sha, fallback)
    changed = sum(
        process_file(args.repo, path, args.pr_license, authors_by_file.get(path, {}), args.check)
        for path in file_paths
    )
    print(f"{'Would update' if args.check else 'Updated'} {changed} file(s)")
    return 1 if args.check and changed else 0


if __name__ == "__main__":
    raise SystemExit(main())
