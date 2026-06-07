#!/usr/bin/env python3

# SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

from __future__ import annotations

import json
import py_compile
import re
import sys
import tomllib
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
SKILLS = ROOT / ".agents" / "skills"
CATALOG = ROOT / ".agents" / "CATALOG.md"
KEBAB = re.compile(r"^[a-z0-9]+(?:-[a-z0-9]+)*$")
LINK = re.compile(r"\[[^\]]+\]\(([^)]+)\)")
FORBIDDEN = (
    "LicenseRef-OpenSpace-AgentPrompts-Restricted",
    "ss14-art/open-space",
    "Agent 007" + " imported",
)


def fail(errors: list[str], message: str) -> None:
    errors.append(message)


def parse_frontmatter(path: Path, errors: list[str]) -> dict[str, str]:
    text = path.read_text(encoding="utf-8")
    lines = text.splitlines()
    if not lines or lines[0].strip() != "---":
        fail(errors, f"{path.relative_to(ROOT)}: frontmatter must start on line 1")
        return {}
    try:
        end = lines.index("---", 1)
    except ValueError:
        fail(errors, f"{path.relative_to(ROOT)}: unterminated frontmatter")
        return {}
    data: dict[str, str] = {}
    for line in lines[1:end]:
        if not line.strip() or line.lstrip().startswith("#"):
            continue
        if ":" not in line:
            fail(errors, f"{path.relative_to(ROOT)}: invalid frontmatter line: {line}")
            continue
        key, value = line.split(":", 1)
        data[key.strip()] = value.strip().strip('"\'')
    return data


def check_links(path: Path, errors: list[str]) -> None:
    text = path.read_text(encoding="utf-8")
    for target in LINK.findall(text):
        if target.startswith(("http://", "https://", "#", "mailto:")):
            continue
        clean = target.split("#", 1)[0]
        if clean and not (path.parent / clean).resolve().exists():
            fail(errors, f"{path.relative_to(ROOT)}: broken link {target}")


def main() -> int:
    errors: list[str] = []
    skill_names: set[str] = set()

    if not SKILLS.is_dir():
        fail(errors, ".agents/skills is missing")
    else:
        for skill_dir in sorted(path for path in SKILLS.iterdir() if path.is_dir()):
            skill = skill_dir / "SKILL.md"
            if not skill.is_file():
                fail(errors, f"{skill_dir.relative_to(ROOT)}: missing SKILL.md")
                continue
            data = parse_frontmatter(skill, errors)
            name = data.get("name", "")
            description = data.get("description", "")
            if name != skill_dir.name:
                fail(errors, f"{skill.relative_to(ROOT)}: name must match directory")
            if not KEBAB.fullmatch(name):
                fail(errors, f"{skill.relative_to(ROOT)}: invalid kebab-case name")
            if len(description) < 24:
                fail(errors, f"{skill.relative_to(ROOT)}: description is too short")
            skill_names.add(name)
            agent = skill_dir / "agents" / "openai.yaml"
            if not agent.is_file():
                fail(errors, f"{skill_dir.relative_to(ROOT)}: missing agents/openai.yaml")
            for markdown in skill_dir.rglob("*.md"):
                check_links(markdown, errors)

    catalog_text = CATALOG.read_text(encoding="utf-8") if CATALOG.is_file() else ""
    for name in sorted(skill_names):
        if f"`{name}`" not in catalog_text:
            fail(errors, f".agents/CATALOG.md: missing skill {name}")

    for path in (ROOT / ".github" / "instructions").glob("*.instructions.md"):
        parse_frontmatter(path, errors)
        check_links(path, errors)
    for path in (ROOT / ".cursor" / "rules").glob("*.mdc"):
        parse_frontmatter(path, errors)
        check_links(path, errors)

    for path in (ROOT / ".codex").rglob("*.json"):
        try:
            json.loads(path.read_text(encoding="utf-8"))
        except Exception as exc:
            fail(errors, f"{path.relative_to(ROOT)}: invalid JSON: {exc}")
    for path in (ROOT / ".codex").rglob("*.toml"):
        try:
            tomllib.loads(path.read_text(encoding="utf-8"))
        except Exception as exc:
            fail(errors, f"{path.relative_to(ROOT)}: invalid TOML: {exc}")
    for path in list((ROOT / ".codex" / "hooks").glob("*.py")) + list((ROOT / "Tools" / "orion_agent_guidance").glob("*.py")):
        try:
            py_compile.compile(str(path), doraise=True)
        except Exception as exc:
            fail(errors, f"{path.relative_to(ROOT)}: Python compile failed: {exc}")

    guidance_paths = [
        ROOT / "AGENTS.md",
        ROOT / ".agents",
        ROOT / ".claude",
        ROOT / ".cursor",
        ROOT / ".codex",
        ROOT / ".github" / "instructions",
        ROOT / "CLAUDE.md",
        ROOT / "GEMINI.md",
        ROOT / "Tools" / "orion_agent_guidance",
    ]
    for base in guidance_paths:
        paths = [base] if base.is_file() else list(base.rglob("*")) if base.exists() else []
        for path in paths:
            if not path.is_file():
                continue
            try:
                text = path.read_text(encoding="utf-8")
            except UnicodeDecodeError:
                continue
            for token in FORBIDDEN:
                if token in text:
                    fail(errors, f"{path.relative_to(ROOT)}: forbidden legacy token {token}")

    required_paths = (
        "Modules/Orion/Content.Orion.Common",
        "Modules/Orion/Content.Orion.Shared",
        "Modules/Orion/Content.Orion.Server",
        "Modules/Orion/Content.Orion.Client",
        "Modules/Orion/Resources",
        "SpaceStation14.slnx",
        "global.json",
    )
    for value in required_paths:
        if not (ROOT / value).exists():
            fail(errors, f"documented repository path is missing: {value}")

    if errors:
        print("Orion Agent Guidance validation failed:", file=sys.stderr)
        for error in errors:
            print(f"- {error}", file=sys.stderr)
        return 1
    print(f"Orion Agent Guidance validation passed: {len(skill_names)} skills")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
