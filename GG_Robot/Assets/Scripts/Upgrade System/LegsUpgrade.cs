using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsUpgrade : UpgradeItem
{
    public override void ApplyAlterations(GameObject player)
    {
        Debug.Log("beep boop you have legs now");
        // player.movementSpeed = 2;

    }
}
