using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChooseLevelManager : MonoBehaviour {

    private Button[] _levels;

    [SerializeField]
    private GameObject SelectedLevelIcon, BillObject;
    

    
    private void Awake()
    {
        _levels = GetComponentsInChildren<Button>();
        InteractableLevels();
        foreach (var button in _levels)
        {
            if (button.IsInteractable())
            {
                button.onClick.AddListener(() => ButtonPress(button.name));

            }
        }
        if (StateManager.Instance.SelectedLevel != 0)
        {
            SelectedLevelIcon.transform.parent = _levels[StateManager.Instance.SelectedLevel-1].transform;
            SelectedLevelIcon.transform.localPosition = Vector3.zero;
            SelectedLevelIcon.transform.localPosition = new Vector3(5f, 114f, 0f);

        }

    }

    private void ButtonPress(string NameButton)
    {
        int number;
        int.TryParse(NameButton.Substring(NameButton.Length - 1, 1),out  number);

        var data = PretendData.Instance.Data[number-1];
        var instance = StateManager.Instance;
        instance.LevelName = data.BSDataName;
        instance.SelectedLevel = number;
        EventManager.TriggerEvent("SelectedLevel");
        SelectedLevelIcon.transform.parent = _levels[number - 1].transform;
        SelectedLevelIcon.transform.localPosition = new Vector3(5f,114f,0f);

        
    }
    

    private void InteractableLevels()
    {
        var MaxLevel = StateManager.Instance.LevelsAvailable;

        if (MaxLevel >= _levels.Length) MaxLevel = _levels.Length;

        for (int i = 0; i < MaxLevel; i++)
        {
            _levels[i].interactable = true;
            var billImage = Instantiate(BillObject, _levels[i].transform, false);
            if (i %2 ==1)
            {
                billImage.transform.localPosition += new Vector3(0f, 100f, 0f);
            }
            var bill = billImage.GetComponentsInChildren<Text>();
            bill[0].text = PretendData.Instance.Data[i].BSDataName;
            bill[1].text = PretendData.Instance.Data[i].BSDataAmount;
        }
    }

}
