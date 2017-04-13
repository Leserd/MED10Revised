﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PigStats {

    public static int Health = 3;
    public static int Damage = 15;
    public static float Speed = 2;
    public static float Cooldown = 5f;
    public static int UpgradeLevel = 0;
    public static GameObject explosion;
    private static bool _isActivated = false;

    public static string[] UpgradedValues()
    {
        return new string[3] { "(+2)", "(+.5)","" };
    }
    public static void Upgrade()
    {
        Health += 2;
        Speed  += 0.5f;
        UpgradeLevel++;
        
    }
    public static string[] Values()
    {
        if(Unlocked) return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
        return new string[3] { "", "", "" };
    }
    public static bool Unlocked
    {
        get
        {
            return _isActivated;
        }
        set
        {
            _isActivated = value;
            UpgradeLevel++;
        }
    }

}