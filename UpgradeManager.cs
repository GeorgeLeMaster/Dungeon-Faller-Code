using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    public PlayerCollision player;

    public TextMeshProUGUI armourCost;
    public TextMeshProUGUI airdashCost;

    public GameObject chestDashButton;

    public float armourUpgradeCost;
    public float airdashUpgradeCost;

        // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (player.chestDash == true)
        {
            Destroy(chestDashButton);

        }

        armourCost.text = 500 + (player.maxArmour * 500) + " GOLD";
        armourUpgradeCost = 500 + (player.maxArmour * 500);

        airdashCost.text = 500 + (player.maxAirDash * 500) + " GOLD";
        airdashUpgradeCost = 500 + (player.maxAirDash * 500);
    }

    public void UpgradeArmour()
    {
        if (player.goldAmnt >= armourUpgradeCost)
        {
            player.maxArmour += 1;
            player.goldAmnt -= armourUpgradeCost;

            SaveSystem.SavePlayer(player);

            FindObjectOfType<AudioManager>().Play("SpendCoins");
            FindObjectOfType<AudioManager>().Play("Select");

        }
    }

    public void UpgradeAirDash()
    {
        if (player.goldAmnt >= airdashUpgradeCost)
        {
            player.maxAirDash += 1;
            player.goldAmnt -= airdashUpgradeCost;

            SaveSystem.SavePlayer(player);

            FindObjectOfType<AudioManager>().Play("SpendCoins");
            FindObjectOfType<AudioManager>().Play("Select");

        }
    }

    public void UpgradeChestDash()
    {
        if (player.goldAmnt >= 2000)
        {
            player.chestDash = true;
            player.goldAmnt -= 2000;

            SaveSystem.SavePlayer(player);

            if (chestDashButton != null)
            Destroy(chestDashButton);

            FindObjectOfType<AudioManager>().Play("SpendCoins");
            FindObjectOfType<AudioManager>().Play("Select");

        }
    }
}
