using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChartTester : MonoBehaviour
{

    public GameObject barChartPrefab;
    private Rect _chartRect;


    private void Start()
    {
        _chartRect = GetComponent<RectTransform>().rect;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var data = PretendData.Instance.Data;




                var barchart = Instantiate(barChartPrefab);
                barchart.transform.SetParent(transform, false);

                var chart = barchart.GetComponent<BarChart>();
                chart.ChartWidth = _chartRect.width;
            chart.ChartHeight = _chartRect.height;

            chart.SetData(data);
            

            Debug.Log("pressed Space");
        }
    }    
}
