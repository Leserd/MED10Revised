using UnityEngine;
using UnityEngine.UI;


public class CreateSubCanvas : MonoBehaviour {
    public void CreateCharts(string whichChart, Vector3 worldPosition, GameObject bar){

        var canvasSpawnAtLoc = new GameObject();

        canvasSpawnAtLoc.transform.SetParent(transform, false);
        canvasSpawnAtLoc.name = whichChart;
        canvasSpawnAtLoc.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        canvasSpawnAtLoc.AddComponent<CanvasScaler>();
        canvasSpawnAtLoc.AddComponent<ChartTester>().barChartPrefab = bar;
        canvasSpawnAtLoc.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);
        canvasSpawnAtLoc.GetComponent<RectTransform>().localPosition = worldPosition;

    }
}
