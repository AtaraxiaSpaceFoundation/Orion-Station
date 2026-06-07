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
    reminder = (
        "Before finishing, inspect git status and diff, report exact checks, "
        "state unrun checks, and verify whether any commit was actually pushed."
    )
    json.dump({"additionalContext": reminder, "input": payload}, sys.stdout)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
