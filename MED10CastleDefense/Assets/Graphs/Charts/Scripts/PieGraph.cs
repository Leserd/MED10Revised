using System;
using UnityEngine;
using System.Collections.Generic;
using DataVisualisation.Utilities;


public class PieGraph : MonoBehaviour
{
    public Pie PiePrefab;

    public void SetData(InputData[] dataCollection)
    {


        var total = 0f;
        var zRotation = 0f;

        for (var i = 0; i < dataCollection.Length; i++)
        {
            total += int.Parse(dataCollection[i].BSDataAmountMonthly);
        }

        var colors = ColorGenerator.GetColorsGoldenRatio(dataCollection.Length);
        for (var i = 0; i < dataCollection.Length; i++)
        {
            var entry = dataCollection[i];
            
            //Create the pie
            Pie newWedge = Instantiate(PiePrefab) as Pie;
            newWedge.transform.SetParent(transform);
            newWedge.pie.transform.SetParent(transform, false);
            newWedge.pie.GetComponent<RectTransform>().sizeDelta = transform.root.GetComponent<RectTransform>().sizeDelta;
            newWedge.pie.color = colors[i];
            newWedge.pie.fillAmount =int.Parse( entry.BSDataAmountMonthly) / total;
            newWedge.pie.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
            zRotation -= newWedge.pie.fillAmount * 360f;

            //Create the correct labels
            newWedge.label.text = entry.BSDataName;
            newWedge.label.transform.localEulerAngles = new Vector3(0, 0, -newWedge.pie.GetComponent<RectTransform>().localEulerAngles.z);

            //Create the correct percentage values
            newWedge.pieValue.text = (Math.Round(int.Parse(entry.BSDataAmountMonthly) / total,3)).ToString();
            newWedge.pieValue.transform.localEulerAngles = new Vector3 (0,0,-newWedge.pie.GetComponent<RectTransform>().localEulerAngles.z);
        }
        
    }
}
