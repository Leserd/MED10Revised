using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataVisualisation.Utilities;
using System.Linq;


public class BarButtons : MonoBehaviour {

    public BillLegendButton BillLegend;
    private BarChart _chart;
    private List<InputData> _currentData = new List<InputData>();
    private List<InputData> _allData = new List<InputData>();
    private List<BillLegendButton> _legendButtons = new List<BillLegendButton>();



    public void InstantiateButtons(InputData[] data, BarChart chart)
    {
        _chart = chart;


        var colors = ColorGenerator.GetColorsGoldenRatio(data.Length);
        var sorted = sort(data);
        _allData.AddRange(sorted);
        _currentData = _allData;


        foreach (var bill in sorted)
        {
            var legendButtons = Instantiate(BillLegend, transform, false);
            legendButtons.name = bill.BSDataName;
            legendButtons.LegendBackground.color = colors[bill.ID];
            legendButtons.LegendForeground.color = colors[bill.ID];
            legendButtons.LegendText.text = bill.BSDataName;
            legendButtons.LegendText.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width, legendButtons.LegendText.GetComponent<RectTransform>().rect.height);


             legendButtons.GetComponentInChildren<Button>().onClick.AddListener(() => ButtonPress(bill.ID, legendButtons.transform.GetSiblingIndex()));
            _legendButtons.Add(legendButtons);
        }
    }




    private void ButtonPress(int id, int buttonNum)
    {
        Debug.Log(id + " was the id and the buttonNum pressed was " + buttonNum);
        if (!_legendButtons[buttonNum].LegendActive)
        {
            var colors = ColorGenerator.GetColorsGoldenRatio(_allData.Count);

            var temp2 = _currentData;
           // temp2.Add(_allData.SingleOrDefault(x => x.ID == id));
            _currentData = temp2;



            _legendButtons[buttonNum].LegendForeground.color = colors[id] ;

            _legendButtons[buttonNum].LegendActive = true;
            

        }
        else
        {
            var temp = _currentData;
            //var item = temp.SingleOrDefault(x => x.ID == id);
           // if (item != null) temp.Remove(item);
            _currentData = temp;
            _legendButtons[buttonNum].LegendForeground.color = Color.gray;
            _legendButtons[buttonNum].LegendActive = false;



        }

        _chart.DeleteCurrentGraph();
       // _chart.SetMonthsData(_currentData.ToArray());

    }

    private InputData[] sort(InputData[] unsorted)
    {
        InputData[] sorted1 = unsorted.OrderBy(c => -int.Parse(c.BSDataAmountMonthly)).ToArray();

        InputData[] sorted = sorted1.OrderBy(c => -c.BSDataPaymentMonths.Count).ToArray();
        return sorted;
    }


}
