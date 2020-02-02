using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUpgrade : MonoBehaviour
{
    // Slap this on the sprite for an upgrade pickup
    // Use checkmarks to choose what it does

    public bool UpgradesLegs = false;
    public bool UpgradeTyres = false;
    public bool EyesFix = false;
    public GameEvent FadeOut;

    public ArrayList upgrades = new ArrayList();

    public PickupUpgrade()
    {
        //if (UpgradesLegs) upgrades.Add(new LegsUpgrade());
    }

    private void Start()
    {

        if (UpgradesLegs) upgrades.Add(new LegsUpgrade());
        if (UpgradeTyres) upgrades.Add(new TyreFix());
        if (EyesFix) upgrades.Add(new EyesFixed());
    }

    public void ApplyAllUpgrades(GameObject player)
    {
        foreach (UpgradeItem upgrade in upgrades)
        {
            FadeOut.Raise();
            upgrade.ApplyAlterations(player);            
        }
    }
    public void RevertAllUpgrades(GameObject player)
    {
        foreach (UpgradeItem upgrade in upgrades)
        {
            FadeOut.Raise();
            upgrade.RevertAlterations(player);
        }
    }
}
