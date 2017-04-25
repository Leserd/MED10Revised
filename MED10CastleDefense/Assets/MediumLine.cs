using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediumLine : MonoBehaviour {

    private Text _averageText;
    private float _widthChart;



    private void Awake()
    {
        _averageText = GetComponentInChildren<Text>();
    }

    public void InitializeAvg(float sizeLength, InputData[] data)
    {
        _widthChart = sizeLength;
        ReCenter(data);
    }

    public void ReCenter(InputData[] data)
    {
        _averageText.text = AverageMonthly(data).ToString("F0") + " kr. (gennemsnit pr måned)";
        transform.localPosition = Position(data);
    }

    private Vector3 Position(InputData[] data)
    {
        var yearMax = BarChart.GetMaxValue(data);
        var avgMonthly = AverageMonthly(data);

        var pos =  (yearMax >0) ?  (avgMonthly / yearMax) * (_widthChart) : 0f;//(avgMonthly >= yearMax/2f) ? (avgMonthly / yearMax) *( _widthChart) : -( avgMonthly / yearMax) *(_widthChart );
        return new Vector3( pos,-450,0f);

    }

    private float GetMaxValue(InputData[] billsdata)
    {
        float yearMax = 0f;
        foreach (var item in billsdata)
        {
            float monthlyMax = 0f;
             float.TryParse(item.BSDataAmount, out monthlyMax);
            monthlyMax /= 12;
            if (monthlyMax > yearMax)
            {
                yearMax = monthlyMax;
            }

        }

        return yearMax;
    }

    private float AverageMonthly(InputData[] data)
    {
        float total = 0f;
        foreach (var item in data)
        {
            total += float.Parse(item.BSDataAmount)/12f;
        }
        

        return total;
    }


}
