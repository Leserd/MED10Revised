using UnityEngine;
using DataVisualisation.Utilities;
using System.Collections.Generic;
using System;
using System.Linq;


public class BarChart : MonoBehaviour
{
    public Bar barPrefab;
    

    public float ChartWidth { get; set; }
    public float ChartHeight { get; set; }

    private string[] _months = new string[12]
    {
        "Januar",
        "Februar",
        "Marts",
        "April",
        "Maj",
        "Juni",
        "Juli",
        "August",
        "September",
        "Oktober",
        "November",
        "December"
    };


    public void SetData(InputData[] dataCollection)
    {
        
        float maxValue = 0;
        foreach (var entry in dataCollection)
        {
            if (int.Parse(entry.BSDataAmountMonthly) > maxValue)
                maxValue =int.Parse( entry.BSDataAmountMonthly);
        }

        var colors = ColorGenerator.GetColorsGoldenRatio(dataCollection.Length);
        for (int i = 0; i < dataCollection.Length; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);

            var data = dataCollection[i];
            float value = int.Parse(data.BSDataAmountMonthly);

            // set size of bar/s
            RectTransform rt = newBar.bar.GetComponent<RectTransform>();
            float normalizedValue = (value / maxValue) * 0.95f;
            rt.sizeDelta = new Vector2(ChartWidth * normalizedValue,ChartHeight/12*0.95f);
            newBar.bar.color = colors[i];

            newBar.label.text = data.BSDataName;

            //set value label
            newBar.barValue.text = value.ToString();
           /* if (rt.sizeDelta.x < 30f)
            {
                newBar.barValue.rectTransform.pivot = new Vector2(0.5f, 0f);
                newBar.barValue.rectTransform.anchoredPosition = Vector2.zero;
            }*/
        }
    }

    private List<List<InputData>> GetSortedBillsEachMonth(InputData[] bills)
    {
        List<List<InputData>> monthsData = new List<List<InputData>>();
        InputData[] sorted = bills.OrderBy(c => -int.Parse(c.BSDataAmountMonthly)).ToArray();

        for (int i = 0; i < 12; i++)
        {
            var currentMonthData = new List<InputData>();
            for (int j = 0; j < sorted.Length; j++)
            {
                if (sorted[j].BSDataPaymentMonths.Contains(i))
                {
                    currentMonthData.Add(sorted[j]);
                }
            }
            monthsData.Add(currentMonthData);
        }
        return monthsData;
    }

    private float GetMaxValue(List<List<InputData>> billsdata)
    {
        float yearMax = 0f;
        foreach (var item in billsdata)
        {
            float monthlyMax = GetTotalValueMonth(item);

            if (monthlyMax > yearMax)
            {
                yearMax = monthlyMax;
            }
            
        }

        return yearMax;
    }

    private float GetTotalValueMonth(List<InputData> billsdata)
    {
        float monthlyMax = 0f;
        foreach (var bill in billsdata)
        {
           
                monthlyMax += int.Parse(bill.BSDataAmountMonthly);
        }
        return monthlyMax;

    }

    public void SetMonthsData(InputData[] dataCollection)
    {

        var sortedBills = GetSortedBillsEachMonth(dataCollection);
        float maxValue = GetMaxValue(sortedBills);

        Debug.Log(sortedBills.Count);
        var colors = ColorGenerator.GetColorsGoldenRatio(dataCollection.Length);
        for (int i = 0; i < sortedBills.Count; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);
            newBar.name = _months[i];
            newBar.label.text = _months[i] + "  ";

            float maxMonth = GetTotalValueMonth(sortedBills[i]);

            float normalizedValue = (maxMonth / maxValue) * 0.95f;

            newBar.Bars.sizeDelta = new Vector2(ChartWidth * normalizedValue, ChartHeight / 12 * 0.95f);
            foreach (var bill in sortedBills[i])
            {
                var test = Instantiate(newBar.bar);
                test.transform.SetParent(newBar.Bars);
                var rectTrans = test.GetComponent<RectTransform>();
                rectTrans.sizeDelta =new Vector2( float.Parse(bill.BSDataAmountMonthly),newBar.Bars.sizeDelta.y);
                //test.
            }



            /// skal være i forhold til bill id 
            //newBar.bar.color = colors[i];


            //set value label
            //newBar.barValue.text = value.ToString();



        }












        
        /*
        for (int i = 0; i < dataCollection.Length; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);

            var data = dataCollection[i];
            float value = int.Parse(data.BSDataAmountMonthly);

            // set size of bar/s
            RectTransform rt = newBar.bar.GetComponent<RectTransform>();
            float normalizedValue = (value / maxValue) * 0.95f;
            rt.sizeDelta = new Vector2(ChartWidth * normalizedValue, ChartHeight / 12 * 0.95f);

            newBar.bar.color = colors[i];

            newBar.label.text = data.BSDataName;

            //set value label
            newBar.barValue.text = value.ToString();

            



        
            /* if (rt.sizeDelta.x < 30f)
             {
                 newBar.barValue.rectTransform.pivot = new Vector2(0.5f, 0f);
                 newBar.barValue.rectTransform.anchoredPosition = Vector2.zero;
             }
        }*/
    }


}
