using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public GameObject CreateCharacter(CharacterPrefab config, Vector3 position, Quaternion rotation)
    {
        var result = Object.Instantiate(config.prefab, position, rotation);

        if (result.TryGetComponent(out Character character))
            character.Setup(config.characterModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (!animator)
            animator = result.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = config.animatorController;
       
        var controller = result.GetComponent<PlayerController>();
        if (!controller)
            controller = result.AddComponent<PlayerController>();
        controller.Setup(config.controllerModel);

        return result.gameObject;
    }
}