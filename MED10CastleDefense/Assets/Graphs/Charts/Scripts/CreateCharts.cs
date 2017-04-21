using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateCharts : MonoBehaviour {

    public Vector3 ChooseStartLocation;
    public GameObject BarCanvas;
   	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var data = PretendData.Instance.Data;
            
            var barchart = Instantiate(BarCanvas);
            barchart.transform.SetParent(transform, false);

            var chart = barchart.GetComponentInChildren<BarChart>();

            var mediumLine = barchart.GetComponentInChildren<MediumLine>();
            mediumLine.InitializeAvg(1920f - 200f - 600f, data);

            var chartButtons = barchart.GetComponentInChildren<BarButtons>();
            chartButtons.InstantiateButtons(data, chart, mediumLine);

            chart.ChartWidth = 1920f - 200f - 600f; //canvasSpawnAtLoc.GetComponent<RectTransform>().rect.width;
            chart.ChartHeight = 1080f - 100f - 100f; //canvasSpawnAtLoc.GetComponent<RectTransform>().rect.height;

            chart.SetMonthsData(data);



        }
    }
}
