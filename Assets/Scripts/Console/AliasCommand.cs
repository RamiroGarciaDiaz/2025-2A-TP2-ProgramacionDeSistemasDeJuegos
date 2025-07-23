public class AliasCommand : IConsoleCommand
{
    public string Name => "aliases";
    public string[] Aliases => new[] { "aliases", "as" };
    public string Description => "Displays all aliases for a command: aliases <command>";

    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            ConsoleController.Instance.AppendLog("Use: aliases <command>");
            return;
        }

        var cmdName = args[1];
        if (ConsoleController.Instance.TryGetCommandInfo(cmdName, out var cmd))
        {
            var list = string.Join(", ", cmd.Aliases);
            ConsoleController.Instance.AppendLog($"Aliases of {cmd.Name}: {list}");
        }
        else
            ConsoleController.Instance.AppendLog($"Command '{cmdName}' not found.");
    }
}