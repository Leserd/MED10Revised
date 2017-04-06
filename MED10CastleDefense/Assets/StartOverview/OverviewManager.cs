using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewManager : MonoBehaviour {

    Text[] panelTexts;

	// Use this for initialization
	void Awake () {
        panelTexts = GetComponentsInChildren<Text>();
        EventManager.StartListening("SelectedLevel", SelectedLevel);
        UpdateTextPanel();
	
	}
    void SelectedLevel()
    {
        panelTexts[1].text = StateManager.Instance.SelectedLevel.ToString();
    }
    void UpdateTextPanel()
    {
        var instance = StateManager.Instance;
        panelTexts[3].text = instance.YearlyExpense.ToString();
        panelTexts[5].text = (instance.LevelsAvailable - 1).ToString();
    }
}
