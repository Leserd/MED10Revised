﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieGraphTest : MonoBehaviour {

    private float[] values;
    public Color[] wedgeColors;
    public Image wedgePrefab;

    private float total;
    private PretendData data;

    private void Awake()
    {
        data = PretendData.Instance;
        values = new float[data.Data.Length];
        for (int i = 0; i < data.Data.Length; i++)
        {
            values[i] = int.Parse(data.Data[i].BSDataAmount);
        }
    }

    void Start () {
        MakeGraph(values);
	}
    void MakeGraph(float[] dataValues)
    {
        float zRotation = 0f;
        foreach (var value in dataValues)
        {
            total += value;
        }
        for (int i = 0; i < dataValues.Length; i++)
        {
            var newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = wedgeColors[i];
            newWedge.fillAmount = dataValues[i] / total;
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= newWedge.fillAmount * 360f;

        }
    }
	

}
