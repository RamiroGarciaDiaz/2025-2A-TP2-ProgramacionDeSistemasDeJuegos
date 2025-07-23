using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ButtonConfig menuConfig;
    [SerializeField] private Transform buttonLayout;
    private MenuSpawner<ButtonConfig.ButtonEntry> _menuSpawner;
    private ICharacterSpawner _spawner;

    private void Start()
    {
        _spawner = ServiceLocator.Get<ICharacterSpawner>();
        _menuSpawner = new MenuSpawner<ButtonConfig.ButtonEntry>();

        _menuSpawner.BuildMenu(
            menuConfig.buttons,
            buttonLayout,
            entry => entry.buttonTitle,
            entry => _spawner.Spawn(entry.characterPrefab)
        );
    }
}