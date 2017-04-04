using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SafeStats {

    public static int Health = 3;
    public static int Damage = 3;
    public static float Speed = 10f;
    public static float Cooldown = 20f;
    public static int UpgradeLevel = 1;

    public static string[] UpgradedValues()
    {
        return new string[4] { "", (Damage +2).ToString(), "", (Cooldown *0.9f).ToString() };
    }
    public static void Upgrade()
    {
        Damage +=2;
        Cooldown = Cooldown *0.9f;
        UpgradeLevel++;
    }
    public static string[] Values()
    {
        return new string[4] { Health.ToString(), Damage.ToString(), Speed.ToString(), Cooldown.ToString() };
    }

}
