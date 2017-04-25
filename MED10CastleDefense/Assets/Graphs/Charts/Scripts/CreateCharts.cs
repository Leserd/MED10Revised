using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreateCharts : MonoBehaviour {

    public GameObject BarCanvasPrefab;
    private Button _exit;
    private bool _isMade = false;
    private GameObject _barOverview;
    private bool _isShown = false;

    
    private void Awake()
    {
        _exit = transform.GetChild(0).GetComponent<Button>();
        _exit.onClick.AddListener(() => ToggleDisplay());
    }



    public void ToggleDisplay () {
        _isShown = !_isShown;

        if(_isMade == false)
        {
            CreateChart();
            _isMade = true;
        }

        _exit.gameObject.SetActive(_isShown);
        _barOverview.SetActive(_isShown);
    }



    private void CreateChart()
    {
        var data = PretendData.Instance.Data;

        _barOverview = Instantiate(BarCanvasPrefab);
        _barOverview.transform.SetParent(transform, false);

        var chart = _barOverview.GetComponentInChildren<BarChart>();

        var mediumLine = _barOverview.GetComponentInChildren<MediumLine>();
        mediumLine.InitializeAvg(1920f - 200f - 600f -10f , data);

        var chartButtons = _barOverview.GetComponentInChildren<BarButtons>();
        chartButtons.InstantiateButtons(data, chart, mediumLine);

        chart.ChartWidth = 1920f - 200f - 600f -10f ; //canvasSpawnAtLoc.GetComponent<RectTransform>().rect.width;
        chart.ChartHeight = 1080f - 100f - 100f -10f; //canvasSpawnAtLoc.GetComponent<RectTransform>().rect.height;

        chart.SetMonthsData(data);

        _barOverview.transform.SetAsFirstSibling();
    }
}
