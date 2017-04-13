using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillOverview : MonoBehaviour {

    Text[] panelTexts;

    
    void Awake()
    {
        panelTexts = GetComponentsInChildren<Text>();
        EventManager.StartListening("SelectedLevel", SelectedLevel);
    }

    private void Start()
    {
        SelectedLevel();
    }

    void SelectedLevel()
    {
        int lvl = StateManager.Instance.SelectedLevel - 1;
        panelTexts[0].text = PretendData.Instance.Data[lvl].BSDataName;
        panelTexts[1].text = PretendData.Instance.Data[lvl].BSDataAmountMonthly + " kr";
        panelTexts[2].text = PretendData.Instance.Data[lvl].BSDataFrequency;
        panelTexts[3].text = PretendData.Instance.Data[lvl].BSDataAmount + " kr";
    }
}
