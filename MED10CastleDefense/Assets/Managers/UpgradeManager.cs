using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeManager : MonoBehaviour {

    private Text[] _values;
    private Button[] _buttons;

    private void OnEnable()
    {
        _values = GetComponentsInChildren<Text>();
        _buttons = GetComponentsInChildren<Button>();
        UpdateValues();
        _buttons[0].onClick.AddListener(() => OnUpgradeCoin());
        _buttons[1].onClick.AddListener(() => OnUpgradePiggy());
        _buttons[2].onClick.AddListener(() => OnUpgradeSafe());
    }

    private void UpdateValues()
    {
        UpdatePiggyValues();
        UpdateCoinValues();
        UpdateSafeValues();
    }
    private void UpdateCoinValues()
    {
        Text[] values = new Text[4];
        Array.Copy(_values, 0, values, 0, 4);
        Text[] UpgradeValues = new Text[4];
        Array.Copy(_values, 4, UpgradeValues, 0, 4);
        for (int i = 0; i < 4; i++)
        {
            values[i].text = CoinStats.Values()[i];
            if (CoinStats.UpgradedValues()[i] != "")
            {
                UpgradeValues[i].text = "+" + CoinStats.UpgradedValues()[i];

            }
            else
            {
                UpgradeValues[i].text = "";
            }
        }
    }

    private void UpdatePiggyValues()
    {
        Text[] values = new Text[4];
        Array.Copy(_values, 8, values, 0, 4);
        Text[] UpgradeValues = new Text[4];
        Array.Copy(_values, 12, UpgradeValues, 0, 4);
        for (int i = 0; i < 4; i++)
        {
            values[i].text = PigStats.Values()[i];
            if (PigStats.UpgradedValues()[i] != "")
            {
                UpgradeValues[i].text = "+" + PigStats.UpgradedValues()[i];

            }
            else
            {
                UpgradeValues[i].text = "";
            }
        }
    }

    private void UpdateSafeValues()
    {
        Text[] values = new Text[4];
        Array.Copy(_values, 16, values, 0, 4);
        Text[] UpgradeValues = new Text[4];
        Array.Copy(_values, 20, UpgradeValues, 0, 4);
        for (int i = 0; i < 4; i++)
        {
            values[i].text = SafeStats.Values()[i];
            if (SafeStats.UpgradedValues()[i] != "")
            {
                UpgradeValues[i].text = "+" + SafeStats.UpgradedValues()[i];

            }
            else
            {
                UpgradeValues[i].text = "";
            }
        }
    }

    private void OnUpgradeCoin()
    {
        if (StateManager.Instance.UpgradesAvailable != 0)
        {
            CoinStats.Upgrade();
            StateManager.Instance.UpgradesAvailable = -1;
            EventManager.TriggerEvent("Upgrade");
            UpdateCoinValues();
        }
        return;
    }
    private void OnUpgradePiggy()
    {
        if (StateManager.Instance.UpgradesAvailable != 0)
        {
            PigStats.Upgrade();
            StateManager.Instance.UpgradesAvailable = -1;
            EventManager.TriggerEvent("Upgrade");
            UpdatePiggyValues();
        }
        return;
    }
    private void OnUpgradeSafe()
    {
        if (StateManager.Instance.UpgradesAvailable != 0)
        {
            SafeStats.Upgrade();
            StateManager.Instance.UpgradesAvailable = -1;
            EventManager.TriggerEvent("Upgrade");
            UpdateSafeValues();
        }
        return;
    }



}
