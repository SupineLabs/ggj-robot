using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesFixed : UpgradeItem
{
    private GameObject _player;

    public override void ApplyAlterations(GameObject player)
    {
        _player = player;
        // player.movementSpeed = 2;
    }
    public override void RevertAlterations(GameObject player)
    {
        player.transform.GetChild(0).gameObject.SetActive(true);
        // player.movementSpeed = 1;
    }
}

    //public void Eyes()
    //{
    //    _player.transform.GetChild(0).gameObject.SetActive(false);
    //}
