using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private CharacterFactory _factory;

    private void Awake()
    {
        _factory = new CharacterFactory();
    }
    public void Spawn(CharacterPrefab prefab)
    {
        
       _factory.CreateCharacter(prefab, transform.position, transform.rotation);
    }
}
