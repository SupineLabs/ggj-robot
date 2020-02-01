using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUpgrade : MonoBehaviour
{
    // Slap this on the sprite for an upgrade pickup
    // Use checkmarks to choose what it does

    public bool UpgradesLegs = false;

    public ArrayList upgrades;

    public PickupUpgrade()
    {
        if (UpgradesLegs) upgrades.Add(new LegsUpgrade());
    }

    public void ApplyAllUpgrades(GameObject player)
    {
        foreach (UpgradeItem upgrade in upgrades)
        {
            upgrade.ApplyAlterations(player);
        }
    }
    public void RevertAllUpgrades(GameObject player)
    {
        foreach (UpgradeItem upgrade in upgrades)
        {
            upgrade.RevertAlterations(player);
        }
    }
}
