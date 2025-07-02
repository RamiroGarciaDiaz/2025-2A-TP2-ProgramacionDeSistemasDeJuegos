using UnityEngine;

public interface ICharacterSpawner
{
    void Setup(ICharacterFactory factory);
    void Spawn(CharacterPrefab config);
}