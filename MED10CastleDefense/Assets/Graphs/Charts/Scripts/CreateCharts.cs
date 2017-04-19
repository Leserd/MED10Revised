using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateCharts : MonoBehaviour {

    public Vector3 ChooseStartLocation;
    public GameObject Bars;
   	
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {

            var canvasSpawnAtLoc = new GameObject();

            canvasSpawnAtLoc.transform.SetParent(transform, false);
            canvasSpawnAtLoc.name = "Barchart";
            canvasSpawnAtLoc.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            canvasSpawnAtLoc.AddComponent<CanvasScaler>();
            canvasSpawnAtLoc.AddComponent<ChartTester>().barChartPrefab = Bars;
            canvasSpawnAtLoc.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
            canvasSpawnAtLoc.GetComponent<RectTransform>().localPosition = ChooseStartLocation;

        }
    }
}
