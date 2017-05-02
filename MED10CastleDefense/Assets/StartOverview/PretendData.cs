using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PretendData : MonoBehaviour {

    [SerializeField]
    private InputData[] _data;

    private static PretendData _instance;


    public InputData[] Data
    {
        get
        {
            return _data.OrderBy(c => int.Parse(c.BSDataAmount)).ToArray();
            // return sort(_data);
        }
    }

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
                    var go = Resources.LoadAll("InputDataActive", typeof(GameObject))[0] as GameObject; 
                    DontDestroyOnLoad(go);
                    _instance = go.GetComponent<PretendData>();
                }
            }

            return _instance;
        }
    }

    private InputData[] sort(InputData[] unsorted)
    {
        InputData[] sorted = unsorted.OrderBy(c => -int.Parse(c.BSDataAmount)).ToArray();
        return sorted;
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
