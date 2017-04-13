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
        switch (StateManager.Instance.LevelsAvailable)
        {
            case 1:
                _buttons[1].GetComponent<SpriteManager>().Locked();
                _buttons[2].GetComponent<SpriteManager>().Locked();

                break;
            case 2:
                _buttons[2].GetComponent<SpriteManager>().Locked();

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
                    else
                    {
                        _buttons[2].onClick.AddListener(() => OnUpgradeSafe());

                    }
                    break;
                }

                if (!SafeStats.Unlocked)
                {
                    SafeLocked();
                    _buttons[1].onClick.AddListener(() => OnUpgradePiggy());

                    break;
                }
                _buttons[2].onClick.AddListener(() => OnUpgradeSafe());

                _buttons[1].onClick.AddListener(() => OnUpgradePiggy());

                break;


        }


    }
    void PigLocked()
    {
        _buttons[1].GetComponent<SpriteManager>().Unlock();        
        _buttons[1].onClick.AddListener(() => UpgradeFirstTime(_buttons[1], "piggy"));

    }
    void SafeLocked()
    {

        _buttons[2].GetComponent<SpriteManager>().Unlock();
        _buttons[2].onClick.AddListener(() => UpgradeFirstTime(_buttons[2], "safe"));
    }
    
    void UpgradeFirstTime(Button button, string type)
    {
        if (type == "piggy")
        {
            if (StateManager.Instance.UpgradesAvailable > 0)
            {
                PigStats.Unlocked = true;
                button.GetComponent<SpriteManager>().Upgrade();

                StateManager.Instance.UpgradesAvailable = -1;
                EventManager.TriggerEvent("Upgrade");
                UpdateValues();
                button.onClick.RemoveAllListeners();
                _buttons[1].onClick.AddListener(() => OnUpgradePiggy());
                if (StateManager.Instance.UpgradesAvailable == 0) NoUpgrades();

            }
            return;
        }
        if (type == "safe")
        {
            if (StateManager.Instance.UpgradesAvailable > 0)
            {
                SafeStats.Unlocked = true;
                button.GetComponent<SpriteManager>().Upgrade();

                StateManager.Instance.UpgradesAvailable = -1;
                EventManager.TriggerEvent("Upgrade");
                UpdateValues();
                button.onClick.RemoveAllListeners();
                _buttons[2].onClick.AddListener(() => OnUpgradeSafe());
                if (StateManager.Instance.UpgradesAvailable == 0) NoUpgrades();

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
        _buttons[0].GetComponent<SpriteManager>().NonInteractable();
        if (!SafeStats.Unlocked)
        {
            _buttons[2].GetComponent<SpriteManager>().Locked();
        }
        else
        {
            _buttons[2].GetComponent<SpriteManager>().NonInteractable();
        }
        if (!PigStats.Unlocked)
        {
            _buttons[1].GetComponent<SpriteManager>().Locked();
        }
        else
        {
            _buttons[1].GetComponent<SpriteManager>().NonInteractable();
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
