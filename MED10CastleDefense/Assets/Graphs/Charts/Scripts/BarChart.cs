using UnityEngine;
using DataVisualisation.Utilities;
using System.Collections.Generic;

public class BarChart : MonoBehaviour
{
    public Bar barPrefab;

    public float ChartWidth { get; set; }
    public float ChartHeight { get; set; }


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
            if (rt.sizeDelta.x < 30f)
            {
                newBar.barValue.rectTransform.pivot = new Vector2(0.5f, 0f);
                newBar.barValue.rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
}
