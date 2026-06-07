<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion Agent Guidance tooling

`validate.py` checks the structure and consistency of the guidance package.

It validates:

- `SKILL.md` frontmatter and directory/name agreement;
- required descriptions and `agents/openai.yaml` adapters;
- catalog coverage;
- Markdown relative links;
- GitHub and Cursor frontmatter;
- Codex JSON and TOML syntax;
- Python compilation;
- required repository paths;
- forbidden legacy licensing markers.

`test_command_guard.py` verifies safe read-only commands and destructive Git, shell, and PowerShell command detection.

Run locally:

```bash
python3 -m py_compile .codex/hooks/*.py Tools/orion_agent_guidance/*.py
python3 -m unittest discover -s Tools/orion_agent_guidance -p "test_*.py"
python3 Tools/orion_agent_guidance/validate.py
```
