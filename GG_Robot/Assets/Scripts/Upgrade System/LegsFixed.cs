using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsFixed : UpgradeItem
{
    private GameObject _player;

    public override void ApplyAlterations(GameObject player)
    {
        if (Movement.Instance.CanDoubleJump)
        {
            Movement.Instance.InvertedControls = false;
        }
    }
    public override void RevertAlterations(GameObject player)
    {
        if (Movement.Instance.CanDoubleJump)
        {
            Movement.Instance.InvertedControls = true;
    }
    }
}