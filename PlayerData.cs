using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public float gold;
    public float armour;
    public float airDash;

    public bool chestDash;

    public int longestDistance;

    public PlayerData (PlayerCollision player)
    {
        gold = player.goldAmnt;
        armour = player.maxArmour;

        longestDistance = player.maxDistance;
        airDash = player.maxAirDash;

        chestDash = player.chestDash;
    }
}
