using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PretendData : MonoBehaviour {
    public InputData[] Data;
    private static PretendData _instance;
    public List<InputData> GetListData
    {
        get
        {
            var list = new List<InputData>();
            list.AddRange(Data);
            return list;
        }
    }
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

    public string BSDataName;
    public string BSDataAmount;
    public string BSDataAmountMonthly;
    public string BSDataFrequency;
    public List<int> BSDataPaymentMonths;
    public int ID;
}
