using UnityEngine;

[CreateAssetMenu(fileName = "ButtonConfig", menuName = "Scriptable Objects/ButtonConfig")]
public class ButtonConfig : ScriptableObject
{
    public ButtonEntry[] buttons;

    [System.Serializable]
    public class ButtonEntry
    {
        public string buttonTitle;
        public CharacterPrefab characterPrefab;
    }
}
