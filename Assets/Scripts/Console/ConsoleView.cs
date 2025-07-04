﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class ConsoleView : MonoBehaviour
{
    private const int CHARACTER_LIMIT = 13000;
    [SerializeField] private TMP_Text consoleBody;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submit;
    [SerializeField] private ConsoleWrapper consoleWrapper;

    [SerializeField] private GameObject debugConsole;
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private InputActionReference toggleAction;

    [SerializeField] private Button toggleButton;

    private void Awake()
    {
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleConsole);
    }
    private void Start()
    {
        debugConsole.SetActive(false);
    }
    private void OnEnable()
    {
        submit?.onClick.AddListener(HandleSubmitClick);
        inputField?.onSubmit.AddListener(SubmitInput);

        if (consoleWrapper)
            consoleWrapper.log += WriteToOutput;

        if (toggleAction != null)
        {
            toggleAction.action.Enable();
            toggleAction.action.performed += OnToggleAction;
        }
    }

    private void OnToggleAction(InputAction.CallbackContext context)
    {
        ToggleConsole();
    }

    private void OnDisable()
    {
        submit?.onClick.RemoveListener(HandleSubmitClick);
        inputField?.onSubmit.RemoveListener(SubmitInput);

        if (consoleWrapper)
            consoleWrapper.log -= WriteToOutput;

        if (toggleAction != null)
        {
            toggleAction.action.performed -= OnToggleAction;
            toggleAction.action.Disable();
        }
    }

    private void HandleSubmitClick() => SubmitInput(inputField.text);

    private void SubmitInput(string input)
    {
        if (input == string.Empty)
            return;
        if (consoleWrapper == null)
        {
            Debug.LogError($"{name}: {nameof(consoleWrapper)} is null");
            return;
        }
        _ = consoleWrapper.TryUseInput(input);
        inputField?.SetTextWithoutNotify(string.Empty);
    }

    public void WriteToOutput(string newFeedBack)
    {
        if (!consoleBody)
        {
            Debug.LogError($"{name}: {nameof(consoleBody)} is null");
            return;
        }
        consoleBody.text += "\n" + newFeedBack;
        var watchdog = 10;
        var bodyText = consoleBody.text;
        while (watchdog-- > 0 && bodyText.Length >= CHARACTER_LIMIT)
        {
            var newBody = bodyText[(bodyText.IndexOf('\n') + 1)..];
            consoleBody.text = newBody;
        }
        inputField?.ActivateInputField();
    }

    private void ToggleConsole()
    {
        if (debugConsole != null)
        {
            bool isActive = !debugConsole.activeSelf;
            debugConsole.SetActive(isActive);
            if (isActive && inputField != null)
            {
                inputField.ActivateInputField();
            }
        }
    }
}
