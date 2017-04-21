using UnityEngine;
using DataVisualisation.Utilities;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;


public class BarChart : MonoBehaviour
{



    public Bar barPrefab;

    private List<GameObject> _currentBars = new List<GameObject>();
    

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



    private static List<List<InputData>> GetSortedBillsEachMonth(InputData[] bills)
    {
        List<List<InputData>> monthsData = new List<List<InputData>>();
        InputData[] sorted1 = bills.OrderBy(c => -int.Parse(c.BSDataAmountMonthly)).ToArray();

        InputData[] sorted = sorted1.OrderBy(c => -c.BSDataPaymentMonths.Count).ToArray();


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

    private static  float GetMaxValue(List<List<InputData>> billsdata)
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

    public static float GetMaxValue (InputData[] bills)
    {
        var sort = GetSortedBillsEachMonth(bills);
        return GetMaxValue(sort);
         
    }

    private static float GetTotalValueMonth(List<InputData> billsdata)
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

        var colors = ColorGenerator.GetColorsGoldenRatio(PretendData.Instance.Data.Length);
        for (int i = 0; i < sortedBills.Count; i++)
        {
            Bar newBar = Instantiate(barPrefab) as Bar;
            newBar.transform.SetParent(transform);
            newBar.name = _months[i];
            newBar.label.text = _months[i] + "  ";

            float maxMonth = GetTotalValueMonth(sortedBills[i]);



            //bars height should be chartheight /12 * 0.95f
            // bars width should be ( totalMonthValue / maxMonth) * chartwidth,            float monthTotalWidth = (maxMonth / maxValue) * ChartWidth;
            float monthTotalWidth = (maxMonth / maxValue) * ChartWidth;

            newBar.Bars.sizeDelta = new Vector2(monthTotalWidth, ( ChartHeight / 12) * 0.65f);
            newBar.Bars.pivot = new Vector2(0f, 0.5f);
            newBar.Bars.localPosition = new Vector3(160f, 0f, 0f);
            newBar.barValue.text = maxMonth.ToString() + " kr";



            foreach (var bill in sortedBills[i])
            {
                var billbar = Instantiate(newBar.bar);
                billbar.name = bill.BSDataName;
                billbar.transform.SetParent(newBar.Bars);
                billbar.color = colors[bill.ID];
                var rectTrans = billbar.GetComponent<RectTransform>();
                rectTrans.sizeDelta =new Vector2( (float.Parse(bill.BSDataAmountMonthly)/maxMonth)*monthTotalWidth,newBar.Bars.sizeDelta.y);

            }
            _currentBars.Add(newBar.gameObject);




        }
    }

    public void DeleteCurrentGraph()
    {
        foreach (var item in _currentBars)
        {
            try
            {
                Destroy(item);

            }
            catch 
            {

                Debug.Log("Failed deletion");
            }
        }
    }
}
