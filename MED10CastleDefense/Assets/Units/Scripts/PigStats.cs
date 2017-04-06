using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PigStats {

    public static int Health = 2;
    public static int Damage = 2;
    public static float Speed = 2;
    public static float Cooldown = 10f;
    public static int UpgradeLevel = 1;
    public static GameObject explosion;

    public static string[] UpgradedValues()
    {
        return new string[3] { (Health +3).ToString(), (Speed +1).ToString(),"" };
    }
    public static void Upgrade()
    {
        Health +=3;
        Speed  +=1;
        UpgradeLevel++;
        
    }
    public static string[] Values()
    {
        return new string[3] { Health.ToString(), Speed.ToString(), Cooldown.ToString() };
    }

}