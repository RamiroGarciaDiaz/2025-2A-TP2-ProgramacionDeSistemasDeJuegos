using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    [SerializeField] private ButtonFactory buttonFactory;
    [SerializeField] private Transform parent;
    [SerializeField] private ButtonConfig[] configs;

    private void Start()
    {
        buttonFactory.Setup(parent);

        foreach (var config in configs)
        {          
            foreach (var entry in config.entries)
            {
                buttonFactory.CreateButton(entry, OnButtonClick);
            }
        }
    }

    private void OnButtonClick(ButtonConfig.ButtonEntry entry)
    {
        var spawner = FindFirstObjectByType<CharacterSpawner>();
        if (spawner && entry.characterPrefab)
            spawner.Spawn(entry.characterPrefab);
    }
}
