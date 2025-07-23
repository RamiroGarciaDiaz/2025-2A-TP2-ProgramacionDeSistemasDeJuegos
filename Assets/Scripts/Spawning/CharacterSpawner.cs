using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICharacterSpawner
{
    private void Awake()
    {
        ServiceLocator.Register<ICharacterSpawner>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ICharacterSpawner>();
    }

    public void Spawn(CharacterPrefab config)
    {
        var abstractFactory = ServiceLocator.Get<ICharacterAbstractFactory>();
        var factory = abstractFactory.GetFactory(config);
        factory.CreateCharacter(config, transform.position, transform.rotation);
    }
}
