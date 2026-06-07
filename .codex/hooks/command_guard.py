#!/usr/bin/env python3

# SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

import json
import re
import shlex
import sys
from collections.abc import Iterable


DANGEROUS_PATTERNS = [
    re.compile(r"(?:^|\s)git\s+reset\s+--hard(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+clean\s+-(?:[^\s]*f[^\s]*d|[^\s]*d[^\s]*f)(?:x)?(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+checkout\s+--\s+\.(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+restore\s+\.(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+push\b[^\n]*(?:--force(?:-with-lease)?|-f)(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)rm\s+-[^\s]*r[^\s]*f[^\s]*(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)remove-item\b[^\n]*(?:-recurse\b[^\n]*-force|-force\b[^\n]*-recurse)", re.I),
    re.compile(r"(?:^|\s)rmdir\s+/s\s+/q(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+worktree\s+remove(?:\s|$)", re.I),
    re.compile(r"(?:^|\s)git\s+(?:rebase|filter-branch|filter-repo)(?:\s|$)", re.I),
]


def iter_strings(value: object) -> Iterable[str]:
    if isinstance(value, str):
        yield value
    elif isinstance(value, list):
        if value and all(isinstance(item, str) for item in value):
            yield shlex.join(value)
        for item in value:
            yield from iter_strings(item)
    elif isinstance(value, dict):
        for item in value.values():
            yield from iter_strings(item)


def split_segments(command: str) -> list[str]:
    return [part.strip() for part in re.split(r"&&|\|\||;|(?<!\|)\|(?!\|)", command) if part.strip()]


def find_danger(payload: object) -> str | None:
    for command in iter_strings(payload):
        for segment in split_segments(command):
            for pattern in DANGEROUS_PATTERNS:
                if pattern.search(segment):
                    return segment
    return None


def main() -> int:
    try:
        payload = json.load(sys.stdin)
    except json.JSONDecodeError:
        payload = {}
    dangerous = find_danger(payload)
    if dangerous:
        json.dump({
            "decision": "block",
            "reason": f"Destructive command requires explicit user approval: {dangerous}",
        }, sys.stdout)
        return 0
    json.dump({"decision": "allow"}, sys.stdout)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
