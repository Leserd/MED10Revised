using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretendData : MonoBehaviour {
    public InputData[] Data;
    public static PretendData instance;

    private void Awake()
    {
        instance = this;

    }
    
}
[System.Serializable]
public class InputData
{
    // public string BSdataName;

    public string BSDataName;
    public string BSDataAmount;
}



