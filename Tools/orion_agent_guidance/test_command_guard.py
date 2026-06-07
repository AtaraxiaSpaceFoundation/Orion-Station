import importlib.util
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
MODULE_PATH = ROOT / ".codex" / "hooks" / "command_guard.py"
SPEC = importlib.util.spec_from_file_location("orion_command_guard", MODULE_PATH)
assert SPEC is not None and SPEC.loader is not None
MODULE = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(MODULE)
find_danger = MODULE.find_danger


class CommandGuardTests(unittest.TestCase):
    def test_allows_read_only_git(self):
        self.assertIsNone(find_danger({"command": "git status && git diff --stat"}))

    def test_blocks_reset_hard(self):
        self.assertIsNotNone(find_danger({"command": "git reset --hard HEAD~1"}))

    def test_blocks_clean_variants(self):
        for command in ("git clean -fd", "git clean -dfx", "git clean -xfd"):
            with self.subTest(command=command):
                self.assertIsNotNone(find_danger({"command": command}))

    def test_blocks_chained_command(self):
        self.assertIsNotNone(find_danger({"command": "echo ok && rm -rf build"}))

    def test_blocks_pipeline(self):
        self.assertIsNotNone(find_danger({"command": "echo ok | git reset --hard"}))

    def test_blocks_powershell_order_variants(self):
        for command in (
            "Remove-Item cache -Force -Recurse",
            "Remove-Item -Recurse cache -Force",
        ):
            with self.subTest(command=command):
                self.assertIsNotNone(find_danger({"command": command}))

    def test_blocks_argument_array(self):
        self.assertIsNotNone(find_danger({"args": ["git", "push", "--force", "origin", "Agents"]}))

    def test_blocks_nested_payload(self):
        payload = {"tool": {"input": {"commands": [{"value": "git checkout -- ."}]}}}
        self.assertIsNotNone(find_danger(payload))

    def test_empty_and_malformed_shapes_are_safe(self):
        self.assertIsNone(find_danger({}))
        self.assertIsNone(find_danger(None))


if __name__ == "__main__":
    unittest.main()
