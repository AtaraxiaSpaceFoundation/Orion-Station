#!/usr/bin/env python3

# SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

import json
import sys


def main() -> int:
    try:
        payload = json.load(sys.stdin)
    except json.JSONDecodeError:
        payload = {}
    message = (
        "Read AGENTS.md, the nearest scoped AGENTS.md, .agents/CATALOG.md, "
        "relevant .agents/rules, and task-specific skills before editing. "
        "Confirm module ownership and verification requirements."
    )
    json.dump({"additionalContext": message, "input": payload}, sys.stdout)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
