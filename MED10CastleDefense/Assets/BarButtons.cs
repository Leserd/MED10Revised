﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataVisualisation.Utilities;
using System.Linq;


public class BarButtons : MonoBehaviour {

    public BillLegendButton BillLegend;
    private BarChart _chart;
    private MediumLine _line;
    private List<InputData> _currentData = new List<InputData>();
    private List<InputData> _allData = new List<InputData>();
    private List<BillLegendButton> _legendButtons = new List<BillLegendButton>();
    private VerticalLayoutGroup vlg;


    public void InstantiateButtons(InputData[] data, BarChart chart, MediumLine mediumLine)
    {
        _line = mediumLine;
        _chart = chart;


        var colors = ColorGenerator.GetColorsGoldenRatio(data.Length);
        var sorted = sort(data);
        _allData.AddRange(sorted);
        _currentData.AddRange(sorted);

        vlg = GetComponent<VerticalLayoutGroup>();
        if (sorted.Count() > 11)
        {
            vlg.padding.top = 50;
            vlg.spacing = 62;
        }

        foreach (var bill in sorted)
        {
            var legendButtons = Instantiate(BillLegend, transform, false);
            legendButtons.name = bill.BSDataName;
            legendButtons.LegendBackground.color = colors[bill.ID];
            legendButtons.LegendForeground.color = colors[bill.ID];
            legendButtons.LegendText.text = bill.BSDataName;
           // legendButtons.LegendText.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width, legendButtons.LegendText.GetComponent<RectTransform>().rect.height);


             legendButtons.GetComponentInChildren<Button>().onClick.AddListener(() => ButtonPress(bill.ID, legendButtons.transform.GetSiblingIndex()));
            _legendButtons.Add(legendButtons);
        }
    }


    private void ButtonPress(int id, int buttonNum)
    {
        if (!_legendButtons[buttonNum].LegendActive)
        {
            var colors = ColorGenerator.GetColorsGoldenRatio(_allData.Count);

            var tempo = _currentData;
            var item = _allData.SingleOrDefault(x => x.ID == id);
            if (item != null) tempo.Add(item);
            _currentData = tempo;

            _legendButtons[buttonNum].LegendForeground.color = colors[id] ;
            _legendButtons[buttonNum].LegendActive = true;
            

        }
        else
        {
            var temp = _currentData;
            var item = temp.SingleOrDefault(x => x.ID == id);
            if (item != null) temp.Remove(item);
            _currentData = temp;

            _legendButtons[buttonNum].LegendForeground.color = Color.gray;
            _legendButtons[buttonNum].LegendActive = false;

            

        }
        _chart.DeleteCurrentGraph();
        _chart.SetMonthsData(_currentData.ToArray());
        _line.ReCenter(_currentData.ToArray());

    }


    private InputData[] sort(InputData[] unsorted)
    {
        InputData[] sorted1 = unsorted.OrderBy(c => -int.Parse(c.BSDataAmountMonthly)).ToArray();

        InputData[] sorted = sorted1.OrderBy(c => -c.BSDataPaymentMonths.Count).ToArray();
        return sorted;
    }


}
