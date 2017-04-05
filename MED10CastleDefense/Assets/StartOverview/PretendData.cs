using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretendData : MonoBehaviour {
    public InputData[] Data;
    private  static PretendData _instance;
    public static PretendData Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(PretendData)) as PretendData;
                if (!_instance)
                {
                    var go = Resources.Load("FakeInput") as GameObject;
                    DontDestroyOnLoad(go);
                    _instance = go.GetComponent<PretendData>();
                }
            }

            return _instance;
        }
    }
 
}
[System.Serializable]
public class InputData
{
    // public string BSdataName;

    public string BSDataName;
    public string BSDataAmount;
}



