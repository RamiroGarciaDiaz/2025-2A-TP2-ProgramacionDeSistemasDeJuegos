public class HelpCommand : IConsoleCommand
{
    public string Name => "help";
    public string[] Aliases => new[] { "h", "?" };
    public string Description => "Displays information for a command: help <command>";

    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            ConsoleController.Instance.AppendLog("Use: help <comand>");
            return;
        }

        var cmdName = args[1];
        if (ConsoleController.Instance.TryGetCommandInfo(cmdName, out var cmd))
            ConsoleController.Instance.AppendLog($"{cmd.Name}: {cmd.Description}");
        else
            ConsoleController.Instance.AppendLog($"Comando '{cmdName}' no encontrado.");
    }
}