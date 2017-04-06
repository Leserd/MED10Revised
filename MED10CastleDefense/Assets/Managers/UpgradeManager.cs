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
        Text[] values = new Text[3];
        Array.Copy(_values, 0, values, 0, 3);
        Text[] UpgradeValues = new Text[3];
        Array.Copy(_values, 3, UpgradeValues, 0, 3);
        for (int i = 0; i < 3; i++)
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
        Text[] values = new Text[3];
        Array.Copy(_values, 6, values, 0, 3);
        Text[] UpgradeValues = new Text[3];
        Array.Copy(_values, 9, UpgradeValues, 0, 3);
        for (int i = 0; i < 3; i++)
        {
            values[i].text = PiggyStats.Values()[i];
            if (PiggyStats.UpgradedValues()[i] != "")
            {
                UpgradeValues[i].text = "+" + PiggyStats.UpgradedValues()[i];

            }
            else
            {
                UpgradeValues[i].text = "";
            }
        }
    }

    private void UpdateSafeValues()
    {
        Text[] values = new Text[3];
        Array.Copy(_values, 12, values, 0, 3);
        Text[] UpgradeValues = new Text[3];
        Array.Copy(_values, 15, UpgradeValues, 0, 3);
        for (int i = 0; i < 3; i++)
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
            if (StateManager.Instance.UpgradesAvailable == 0) NoUpgrades();

        }
        return;
    }

    void NoUpgrades()
    {
        foreach (var button in _buttons)
        {
            button.interactable = false;
        }
    }
    private void OnUpgradePiggy()
    {
        if (StateManager.Instance.UpgradesAvailable != 0)
        {
            PiggyStats.Upgrade();
            StateManager.Instance.UpgradesAvailable = -1;
            EventManager.TriggerEvent("Upgrade");
            UpdatePiggyValues();
            if (StateManager.Instance.UpgradesAvailable == 0) NoUpgrades();

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
            if (StateManager.Instance.UpgradesAvailable == 0) NoUpgrades();

        }
        return;
    }



}
