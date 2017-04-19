using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateCharts : MonoBehaviour {

    public Vector3 ChooseStartLocation;
    public GameObject Bars;
   	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            var canvasSpawnAtLoc = new GameObject();

            canvasSpawnAtLoc.transform.SetParent(transform, false);
            canvasSpawnAtLoc.name = "Barchart";
            canvasSpawnAtLoc.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            canvasSpawnAtLoc.AddComponent<CanvasScaler>();
           // canvasSpawnAtLoc.AddComponent<ChartTester>().barChartPrefab = Bars;
            canvasSpawnAtLoc.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
            canvasSpawnAtLoc.GetComponent<RectTransform>().localPosition = ChooseStartLocation;


            var data = PretendData.Instance.Data;




            var barchart = Instantiate(Bars);
            barchart.transform.SetParent(canvasSpawnAtLoc.transform, false);

            var chart = barchart.GetComponent<BarChart>();

            chart.ChartWidth = canvasSpawnAtLoc.GetComponent<RectTransform>().rect.width;
            chart.ChartHeight = canvasSpawnAtLoc.GetComponent<RectTransform>().rect.height;

            //chart.SetData(data);
            chart.SetMonthsData(data);


        }
    }
}
