using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupUpgrade : MonoBehaviour
{
    // Slap this on the sprite for an upgrade pickup
    // Use checkmarks to choose what it does

    public bool Usable = true;
    public bool FixesTyres = false;
    public bool FixesEyes = false;
    public bool FixesLegs = false;
    public bool UpgradesLegs = false;
    public bool AddsSprings = false;
    public GameEvent FadeOut;
    public Transform spawnTransform;

    public ArrayList upgrades = new ArrayList();

    private bool changePrefab;
    private GameObject _playerPref;

    public PickupUpgrade()
    {
        //if (UpgradesLegs) upgrades.Add(new LegsUpgrade());
    }

    private void Start()
    {
        GameObject old = _playerPref;

        if (UpgradesLegs)
        {
            upgrades.Add(new LegsUpgrade());
            _playerPref = GameManager.Instance.PlayerPrefabs[2];
        }
        if (FixesTyres)
        {
            upgrades.Add(new TyreFix());
            _playerPref = GameManager.Instance.PlayerPrefabs[0];
        }
        if (FixesEyes)
        {
            upgrades.Add(new EyesFixed());
            _playerPref = GameManager.Instance.PlayerPrefabs[1];
        }
        if (FixesLegs)
        {
            // check for legs
            upgrades.Add(new LegsFixed());
            _playerPref = GameManager.Instance.PlayerPrefabs[2];
        }

        if (AddsSprings)
        {
            upgrades.Add(new EmptyUpgrade());
            _playerPref = GameManager.Instance.PlayerPrefabs[3];
        }

        changePrefab = (old != _playerPref);
    }

    public void ApplyAllUpgrades(GameObject player)
    {
        if(FixesLegs && !player.GetComponent<Movement>().CanDoubleJump) { return; }
        if(AddsSprings && !player.GetComponent<Item_And_Door_Interactions>().hasItem) { return; }

        if (!this.Usable) return;

        AudioManager.Instance.Play("Repair");
        foreach (UpgradeItem upgrade in upgrades)
        {
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
        FadeOut.Raise();
        yield return new WaitForSeconds(2f);

        Destroy(player);
        
        GameObject newPlayer = Instantiate(_playerPref, spawnTransform.position, Quaternion.identity);
        Camera.main.GetComponent<Follow>().target = newPlayer.transform;

        if (FixesEyes || FixesLegs || FixesTyres) {
            Movement.Instance.InvertedControls = false;
        } else {
            Movement.Instance.InvertedControls = true;
        }

        if (UpgradesLegs)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (player.GetComponent<Item_And_Door_Interactions>() && player.GetComponent<Item_And_Door_Interactions>().hasItem)
        {
            Destroy(player.GetComponent<Item_And_Door_Interactions>().item.gameObject);
        }

        this.Usable = false;
        StopCoroutine("Delay");
    }
}
