using UnityEngine;

[CreateAssetMenu(fileName = "PrefabConfig", menuName = "PrefabConfig")]
public class CharacterPrefab : ScriptableObject
{
    public Character prefab;
    public CharacterModel characterModel;
    public PlayerControllerModel controllerModel;
    public RuntimeAnimatorController animatorController;

}