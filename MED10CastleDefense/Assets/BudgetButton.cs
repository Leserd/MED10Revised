﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetButton : MonoBehaviour {

    Text[] totalBudgetTexts;
    GameObject graphButtonPanel, tablePanel, barChartPanel;

    Button open, table, barChart;



    private void Awake()
    {
        graphButtonPanel = transform.GetChild(0).gameObject;
        open = transform.GetChild(1).GetComponent<Button>();
        totalBudgetTexts = open.transform.GetComponentsInChildren<Text>();
        tablePanel = transform.GetChild(2).gameObject;
        table = graphButtonPanel.transform.GetChild(0).GetComponent<Button>();

        table.onClick.AddListener(() => tablePanel.GetComponent<TableOverview>().ToggleDisplay());


        open.enabled = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            open.onClick.AddListener(() => ToggleDisplay());
            open.enabled = true;
        }
    }



    public void Start()
    {
        BudgetUpdate();
        EventManager.StartListening("SelectedLevel", BudgetUpdate);
        EventManager.StartListening("EnableBudgetOverview", EnableBudget);

        if (StateManager.Instance.LevelsAvailable == PretendData.Instance.Data.Length + 1 && StateManager.Instance.NewLevelComplete == true)
        {
            HintManager.Instance.CreateHint(12);
        }
    }



    private void EnableBudget()
    {
        open.onClick.AddListener(() => ToggleDisplay());
        open.enabled = true;
    }



    void BudgetUpdate()
    {
        totalBudgetTexts[0].text = Mathf.RoundToInt(StateManager.Instance.YearlyExpense / 12).ToString() + " kr";
        totalBudgetTexts[1].text = StateManager.Instance.YearlyExpense.ToString() + " kr";
    }


    public void ToggleDisplay()
    {
        graphButtonPanel.SetActive(!graphButtonPanel.activeSelf);
    }

}
