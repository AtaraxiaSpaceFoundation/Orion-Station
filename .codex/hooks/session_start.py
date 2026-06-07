#!/usr/bin/env python3
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
