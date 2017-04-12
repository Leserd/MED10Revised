using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SafeStats {

    public static int Health = 1;
    public static int Damage = 20;
    public static float Speed = 3;
    public static float Cooldown = 20f;
    public static int UpgradeLevel = 0;
    public static GameObject explosion;
    private static bool _isActivated = false;

    public static string[] UpgradedValues()
    {
        return new string[3] { "", "","-"+( Cooldown-(Cooldown *0.9f)).ToString("F1") };
    }
    public static void Upgrade()
    {
        Cooldown = Cooldown *0.9f;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
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
