﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeManager : MonoBehaviour {

    private Text[] _values;
    private Button[] _buttons;
    private Sprite _safeLocked, _pigLocked, _pigUpgrade, _safeUpgrade;

 

    private void OnEnable()
    {

        _values = GetComponentsInChildren<Text>();
        _buttons = GetComponentsInChildren<Button>();
        UpdateValues();
        _buttons[0].onClick.AddListener(() => OnUpgradeCoin());
        switch (StateManager.Instance.LevelsAvailable)
        {
            case 1:
                _buttons[1].interactable = false;
                _buttons[1].image.color = new Color(0.35f, 0.35f, 0.35f);
                _buttons[1].image.sprite = _pigLocked;
                _buttons[2].interactable = false;
                _buttons[2].image.color = new Color(0.35f, 0.35f, 0.35f);
                _buttons[2].image.sprite = _safeLocked;

                break;
            case 2:

                _buttons[2].interactable = false;
                _buttons[2].image.color = new Color(0.35f, 0.35f, 0.35f);
                _buttons[2].image.sprite = _safeLocked;

                if (!PigStats.Unlocked)
                {
                    PigLocked();
                    break;
                }
                _buttons[1].onClick.AddListener(() => OnUpgradePiggy());

                break;
            default:
                if (!PigStats.Unlocked)
                {
                    PigLocked();
                    if (!SafeStats.Unlocked)
                    {
                        SafeLocked();
                    }
                    break;
                }

                if (!SafeStats.Unlocked)
                {
                    SafeLocked();
                    break;
                }
                _buttons[1].onClick.AddListener(() => OnUpgradePiggy());

                _buttons[2].onClick.AddListener(() => OnUpgradeSafe());
                break;


        }


    }
    void PigLocked()
    {
        _buttons[1].image.color = new Color(0.35f, 0.35f, 0.35f);
        var go = new GameObject("plusPig");
        go.transform.SetParent( _buttons[1].transform);
        var image = go.AddComponent<Image>();
        image.sprite= Resources.Load<Sprite>("PLUS");

        _buttons[1].onClick.AddListener(() => UpgradeFirstTime(_buttons[1], "piggy"));

    }
    void SafeLocked()
    {
        _buttons[2].image.color = new Color(0.35f, 0.35f, 0.35f);
        _buttons[2].onClick.AddListener(() => UpgradeFirstTime(_buttons[2], "safe"));
    }
    
    void UpgradeFirstTime(Button button, string type)
    {
        if (type == "piggy")
        {
            if (StateManager.Instance.UpgradesAvailable > 0)
            {
                PigStats.Unlocked = true;

                button.image.color = Color.white;

                StateManager.Instance.UpgradesAvailable = -1;
                EventManager.TriggerEvent("Upgrade");
                UpdateValues();
                button.onClick.RemoveAllListeners();
                _buttons[1].onClick.AddListener(() => OnUpgradePiggy());
            }
            return;
        }
        if (type == "safe")
        {
            if (StateManager.Instance.UpgradesAvailable > 0)
            {
                SafeStats.Unlocked = true;

                button.image.color = Color.white;

                StateManager.Instance.UpgradesAvailable = -1;
                EventManager.TriggerEvent("Upgrade");
                UpdateValues();
                button.onClick.RemoveAllListeners();
                _buttons[2].onClick.AddListener(() => OnUpgradeSafe());
            }
            return;
        }



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
            values[i].text = FormatValues(CoinStats.Values()[i]);
            if (CoinStats.UpgradedValues()[i] != "")
            {
                UpgradeValues[i].text =CoinStats.UpgradedValues()[i] ;
            }
            else
            {
                UpgradeValues[i].text = "";
            }
        }
    }
    private string FormatValues(string upgradeValue)
    {
        return float.Parse(upgradeValue).ToString("F1");
    }

    private void UpdatePiggyValues()
    {

            Text[] values = new Text[3];
            Array.Copy(_values, 6, values, 0, 3);
            Text[] UpgradeValues = new Text[3];
            Array.Copy(_values, 9, UpgradeValues, 0, 3);
            for (int i = 0; i < 3; i++)
            {
                values[i].text = FormatValues(PigStats.Values()[i]);
            if (PigStats.Unlocked)
            {
                if (PigStats.UpgradedValues()[i] != "")
                {
                    UpgradeValues[i].text = PigStats.UpgradedValues()[i];

                }
                else
                {
                    UpgradeValues[i].text = "";
                }

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
                values[i].text = FormatValues(SafeStats.Values()[i]);
            if (SafeStats.Unlocked)
            {
                if (SafeStats.UpgradedValues()[i] != "")
                {
                    UpgradeValues[i].text = SafeStats.UpgradedValues()[i];

                }
                else
                {
                    UpgradeValues[i].text = "";
                }
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
            PigStats.Upgrade();
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
