using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreFix : UpgradeItem
{
    public override void ApplyAlterations(GameObject player)
    {
        Movement.Instance.InvertedControls = false;
        // player.movementSpeed = 2;

    }
    public override void RevertAlterations(GameObject player)
    {
        Movement.Instance.CanDoubleJump = true;
        // player.movementSpeed = 1;

    }
}
