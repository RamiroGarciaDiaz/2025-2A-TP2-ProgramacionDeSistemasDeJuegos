using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterPrefab config, Vector3 position, Quaternion rotation)
    {
        var result = Object.Instantiate(config.prefab, position, rotation);

        var character = result.GetComponent<Character>();
        if (!character)
            character = result.AddComponent<Character>();

        character.Setup(config.characterModel);

        var controller = result.GetComponent<PlayerController>();
        if (!controller)
            controller = result.AddComponent<PlayerController>();
        controller.Setup(config.controllerModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (!animator)
            animator = result.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;

        return result.gameObject;
    }
}