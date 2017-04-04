using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChooseLevelManager : MonoBehaviour {

    private Button[] _levels;

    [SerializeField]
    private GameObject SelectedLevelIcon;

    
    private void Awake()
    {
        _levels = GetComponentsInChildren<Button>();
        InteractableLevels();

        Debug.Log("ran awake");
        foreach (var button in _levels)
        {
            if (button.IsInteractable())
            {
                button.onClick.AddListener(() => ButtonPress(button.name));

            }
        }
        EventManager.StartListening("LevelIncrease", CompletedALevel);
        
    }

    private void ButtonPress(string NameButton)
    {
        int number;
        int.TryParse(NameButton.Substring(NameButton.Length - 1, 1),out  number);

        var data = PretendData.instance.Data[number-1];
        var instance = StateManager.Instance;
        instance.LevelName = data.BSDataName;
        instance.SelectedLevel = number;
        EventManager.TriggerEvent("SelectedLevel");
        SelectedLevelIcon.transform.parent = _levels[number - 1].transform;
        SelectedLevelIcon.transform.localPosition = Vector3.zero;

        
    }
    

    private void InteractableLevels()
    {
        var MaxLevel = StateManager.Instance.MaxLevel;
        if (StateManager.Instance.MaxLevel >= _levels.Length) StateManager.Instance.MaxLevel = _levels.Length;

        for (int i = 0; i < StateManager.Instance.MaxLevel; i++)
        {
            _levels[i].interactable = true;
        }
    }


    private void CompletedALevel()
    {


    }





}
