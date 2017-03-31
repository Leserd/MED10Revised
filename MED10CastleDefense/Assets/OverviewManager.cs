using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewManager : MonoBehaviour {

    Text[] panelTexts;

	// Use this for initialization
	void Awake () {
        panelTexts = GetComponentsInChildren<Text>();
        EventManager.StartListening("LevelIncrease", Test);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))
        {
          //  Test();
        }


    }
    void Test()
    {
        var instance = StateManager.Instance;
        panelTexts[1].text = instance.MaxLevel.ToString();
        panelTexts[3].text = "null";
        panelTexts[5].text = (instance.MaxLevel - 1).ToString();
    }
}
