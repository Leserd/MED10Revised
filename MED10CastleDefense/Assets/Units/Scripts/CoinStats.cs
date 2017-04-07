using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinStats {

    public static int Health = 2;
    public static int Damage = 5;
    public static float Speed = 4;
    public static float Cooldown = 2f;
    public static int UpgradeLevel = 1;
    public static GameObject explosion;

    public static string[] UpgradedValues()
    {
        return new string[3] { "", "+.5","-0.1"  };
    }
    public static void Upgrade()
    {
        //speed cooldown damage
        Damage += 0; //Mathf.RoundToInt((Damage * (UpgradeLevel*0.5f)));
        Cooldown -= 0.1f; //Cooldown * UpgradeLevel;
        Speed += 0.5f; //Speed * UpgradeLevel;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
    }
}
