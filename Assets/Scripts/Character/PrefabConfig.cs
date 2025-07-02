using UnityEngine;

[CreateAssetMenu(fileName = "PrefabConfig", menuName = "PrefabConfig")]
public class CharacterPrefab : ScriptableObject
{
    public GameObject prefab;
    public CharacterModel characterModel;
    public PlayerControllerModel controllerModel;
    public RuntimeAnimatorController animatorController;

    public GameObject Prefab => prefab;
    public CharacterModel CharacterModel => characterModel;
    public PlayerControllerModel ControllerModel => controllerModel;
    public RuntimeAnimatorController AnimatorController => animatorController;
}