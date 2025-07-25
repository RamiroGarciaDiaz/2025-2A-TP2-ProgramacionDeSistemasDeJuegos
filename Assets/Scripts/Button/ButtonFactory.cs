using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonFactory : IButtonFactory<ButtonConfig.ButtonEntry>
{
    private readonly Button _buttonPrefab;
    private Transform _parent;

    public CharacterButtonFactory(Button buttonPrefab)
    {
        _buttonPrefab = buttonPrefab;
    }

    public void Setup(Transform parent)
    {
        _parent = parent;
    }

    public Button CreateButton(ButtonConfig.ButtonEntry entry, Action<ButtonConfig.ButtonEntry> onClick)
    {
        var buttonInstance = UnityEngine.Object.Instantiate(_buttonPrefab, _parent);
        var text = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = entry.buttonTitle;
        buttonInstance.onClick.AddListener(() => onClick?.Invoke(entry));
        return buttonInstance;
    }
}
