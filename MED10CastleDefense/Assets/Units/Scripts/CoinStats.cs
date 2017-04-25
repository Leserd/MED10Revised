﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinStats {

    public static int Health = 2;
    public static int Damage = 15;
    public static float Speed = 5;
    public static float Cooldown = 2f;
    public static int UpgradeLevel = 1;
    public static GameObject explosion;

    public static string[] UpgradedValues()
    {
        if (Cooldown <= 0.3f)
        {
            return new string[3] { "", "(+1)", "" };
        }
        return new string[3] { "", "(+1)","(-0.1)"  };
    }
    public static void Upgrade()
    {
        //speed cooldown damage
        Damage += 0; //Mathf.RoundToInt((Damage * (UpgradeLevel*0.5f)));
       // Cooldown -= 0.1f; //Cooldown * UpgradeLevel;
        Cooldown = (Cooldown >= 0.3f) ? Cooldown - 0.1f : Cooldown;
        Speed += 1f; //Speed * UpgradeLevel;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
    }
}
