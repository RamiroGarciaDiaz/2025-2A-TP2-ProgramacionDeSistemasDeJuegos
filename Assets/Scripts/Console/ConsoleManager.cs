using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ConsoleController : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionReference toggleConsoleAction;

    public static ConsoleController Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text outputText;
    [SerializeField] private Button toggleButton;
    [SerializeField] private ScrollRect scrollRect;

    private Dictionary<string, IConsoleCommand> commands = new Dictionary<string, IConsoleCommand>(StringComparer.OrdinalIgnoreCase);
    private ILogHandler defaultLogHandler;
    private LogInterceptor interceptor;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
       
        RegisterCommand(new HelpCommand());
        RegisterCommand(new AliasCommand());
        RegisterCommand(new PlayAnimationCommand());

        consolePanel.SetActive(false);
        toggleButton.onClick.AddListener(ToggleConsole);
        inputField.onSubmit.AddListener(OnInputSubmit);

        defaultLogHandler = Debug.unityLogger.logHandler;
        interceptor = new LogInterceptor(defaultLogHandler, AppendLog);
        Debug.unityLogger.logHandler = interceptor;
    }

    private void Update()
    {
       
    }

    public void RegisterCommand(IConsoleCommand cmd)
    {
        commands[cmd.Name] = cmd;
        foreach (var alias in cmd.Aliases)
            commands[alias] = cmd;
    }

    private void ToggleConsole()
    {
        consolePanel.SetActive(!consolePanel.activeSelf);
        if (consolePanel.activeSelf)
            inputField.ActivateInputField();
    }

    private void OnInputSubmit(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;
        AppendLog($"> {text}");
        ParseAndExecute(text);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void ParseAndExecute(string input)
    {
        var split = input.Split(' ');
        if (split.Length == 0) return;

        if (commands.TryGetValue(split[0], out var cmd))
        {
            try
            {
                cmd.Execute(split);
            }
            catch (Exception e)
            {
                AppendLog($"<color=red>Error:</color> {e.Message}");
            }
        }
        else
        {
            AppendLog($"<color=yellow>Comand not found:</color> {split[0]}");
        }
    }

    public void AppendLog(string line)
    {
        outputText.text += line + "\n";

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public bool TryGetCommandInfo(string name, out IConsoleCommand command)
    {
        return commands.TryGetValue(name, out command);
    }

    private void OnDestroy()
    {
        if (Debug.unityLogger.logHandler == interceptor)
            Debug.unityLogger.logHandler = defaultLogHandler;
    }

    private void OnEnable()
    {
        toggleConsoleAction.action.Enable();
        toggleConsoleAction.action.performed += OnToggleConsole;
    }

    private void OnDisable()
    {
        toggleConsoleAction.action.performed -= OnToggleConsole;
        toggleConsoleAction.action.Disable();
    }

    private void OnToggleConsole(InputAction.CallbackContext ctx)
    {
        ToggleConsole();
    }

}
