using UnityEngine;

public interface ICharacterFactory
{
    GameObject CreateCharacter(CharacterPrefab config, Vector3 position, Quaternion rotation);
}