using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    [SerializeField] private Button ButtonPrefab;

    private void Awake()
    {
        var defaultFactory = new CharacterFactory();
        var abstractFactory = new CharacterAbstractFactory(defaultFactory);
        ServiceLocator.Register<ICharacterAbstractFactory>(abstractFactory);

        var buttonAbstractFactory = new ButtonAbstractFactory();
        var characterButtonFactory = new CharacterButtonFactory(ButtonPrefab);
        buttonAbstractFactory.RegisterFactory(characterButtonFactory);
        ServiceLocator.Register<IButtonAbstractFactory>(buttonAbstractFactory);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterAbstractFactory>();
        ServiceLocator.Unregister<IButtonAbstractFactory>();
    }
}