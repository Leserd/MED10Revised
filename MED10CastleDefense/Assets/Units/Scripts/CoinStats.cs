﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinStats {

    public static int Health = 1;
    public static int Damage = 1;
    public static float Speed = 10f;
    public static float Cooldown = 2f;
    public static int UpgradeLevel = 1;

    public static string[] UpgradedValues()
    {
        return new string[4] { "", (Damage +1).ToString(), (Speed +0.5f).ToString(), (Cooldown -.1f).ToString() };
    }
    public static void Upgrade()
    {
        //speed cooldown damage
        Damage += 1; //Mathf.RoundToInt((Damage * (UpgradeLevel*0.5f)));
        Cooldown -= 0.1f; //Cooldown * UpgradeLevel;
        Speed += 0.5f; //Speed * UpgradeLevel;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[4] { Health.ToString(), Damage.ToString(), Speed.ToString(), Cooldown.ToString() };
    }
}
