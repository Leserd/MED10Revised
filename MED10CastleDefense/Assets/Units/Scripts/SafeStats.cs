using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SafeStats {

    public static int Health = 1;
    public static int Damage = 20;
    public static float Speed = 3;
    public static float Cooldown = 20f;
    public static int UpgradeLevel = 1;
    public static GameObject explosion;

    public static string[] UpgradedValues()
    {
        return new string[3] { "", "","-"+( Cooldown-(Cooldown *0.9f)).ToString("F1") };
    }
    public static void Upgrade()
    {
        Damage +=2;
        Cooldown = Cooldown *0.9f;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
    }

}
