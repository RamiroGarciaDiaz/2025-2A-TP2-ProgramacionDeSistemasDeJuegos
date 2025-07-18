using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationCommand : IConsoleCommand
{
    public string Name => "playanimation";
    public string[] Aliases => new[] { "playanim", "pa" };
    public string Description => "Plays an animation on all characters: playanimation <animName>";

    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            ConsoleController.Instance.AppendLog("Use: playanimation <animName>");
            return;
        }

        string animName = args[1];
        var animators = UnityEngine.Object.FindObjectsOfType<Animator>();
        int playedCount = 0;
        var validNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var animator in animators)
        {
            var controller = animator.runtimeAnimatorController;
            if (controller == null)
                continue;

            foreach (var clip in controller.animationClips)
                validNames.Add(clip.name);

            if (controller.animationClips.Any(c =>
                    c.name.Equals(animName, StringComparison.OrdinalIgnoreCase)))
            {
                animator.Play(animName);
                playedCount++;
            }
        }

        if (playedCount > 0)
        {
            ConsoleController.Instance.AppendLog(
                $"Playing '{animName}' in {playedCount} animator(s)."
            );
        }
        else
        {
            string list = validNames.Count > 0 ? string.Join(", ", validNames.OrderBy(n => n)) : "None";
            ConsoleController.Instance.AppendLog(
                $"Animation '{animName}' not found. Valid Names: {list}"
            );
        }
    }
}
