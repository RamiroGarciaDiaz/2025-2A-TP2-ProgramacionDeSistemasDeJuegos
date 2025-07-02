using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonConfig", menuName = "ButtonConfig")]
public class ButtonConfig : ScriptableObject
{
    public ButtonEntry[] entries;
    [Serializable]
    public class ButtonEntry
    {
        public string buttonTitle;
        public CharacterPrefab characterPrefab;
    }
}
