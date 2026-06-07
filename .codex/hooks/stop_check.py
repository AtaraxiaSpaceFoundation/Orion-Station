#!/usr/bin/env python3
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
