using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUpgrade : MonoBehaviour
{
    // Slap this on the sprite for an upgrade pickup
    // Use checkmarks to choose what it does

    public bool Usable = true;
    public bool UpgradesLegs = false;
    public bool UpgradeTyres = false;
    public bool FixesEyes = false;
    public GameEvent FadeOut;

    public ArrayList upgrades = new ArrayList();

    private GameObject _playerPref;

    public PickupUpgrade()
    {
        //if (UpgradesLegs) upgrades.Add(new LegsUpgrade());
    }

    private void Start()
    {

        if (UpgradesLegs)
        {
            upgrades.Add(new LegsUpgrade());
            _playerPref = GameManager.Instance.PlayerPrefabs[2];
        }
        if (UpgradeTyres)
        {
            upgrades.Add(new TyreFix());
            _playerPref = GameManager.Instance.PlayerPrefabs[0];
        }
        if (FixesEyes)
        {
            upgrades.Add(new EyesFixed());
            _playerPref = GameManager.Instance.PlayerPrefabs[1];
        }
    }

    public void ApplyAllUpgrades(GameObject player)
    {
        if (!this.Usable) return;

        AudioManager.Instance.Play("Repair");
        foreach (UpgradeItem upgrade in upgrades)
        {
            FadeOut.Raise();
            if (FixesEyes)
            {
                StartCoroutine(Eyes(player));
            }
            upgrade.ApplyAlterations(player);
            StartCoroutine(Delay(player));
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



    public IEnumerator Eyes(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        player.transform.GetChild(0).gameObject.SetActive(false);
        StopCoroutine("Eyes");
    }

    public IEnumerator Delay(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        Vector3 oldTrasform = player.transform.position;
        Destroy(player);
        GameObject newPlayer = Instantiate(_playerPref, oldTrasform, Quaternion.identity);
        Camera.main.GetComponent<Follow>().target = newPlayer.transform;
        this.Usable = false;
        StopCoroutine("Delay");
    }
}
