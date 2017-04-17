using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChooseLevelManager : MonoBehaviour {

    private Button[] _levels;

    [SerializeField]
    private GameObject SelectedLevelIcon, BillObject;
    [SerializeField]
    private Sprite FinishedLevel;
    private static bool _showIntroHint = true;
    

    
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
            try
            {
                SelectedLevelIcon.transform.parent = _levels[StateManager.Instance.LevelsAvailable -1].transform;
                StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable-1].BSDataName;
                EventManager.TriggerEvent("SelectedLevel");

                StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable;


            }
            catch
            {
                Debug.Log("catch");
                SelectedLevelIcon.transform.parent = _levels[(StateManager.Instance.LevelsAvailable)-2].transform;
                StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable -1;
                StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable - 2].BSDataName;


            }
            SelectedLevelIcon.transform.localPosition = Vector3.zero;
            SelectedLevelIcon.transform.localPosition = new Vector3(5f, 114f, 0f);

        }

        if (_showIntroHint)
        {
            HintManager.Instance.CreateHint(0, Vector3.zero);
            _showIntroHint = false;
        }

       

    }

    private void ButtonPress(string NameButton)
    {
        int number;
        int.TryParse(NameButton.Substring(NameButton.Length - 1, 1),out  number);

        var data = PretendData.Instance.Data[number-1];
        var instance = StateManager.Instance;
        instance.LevelName = data.BSDataName;
        EventManager.TriggerEvent("SelectedLevel");
        instance.SelectedLevel = number;
        SelectedLevelIcon.transform.parent = _levels[number - 1].transform;
        SelectedLevelIcon.transform.localPosition = new Vector3(5f,114f,0f);

        
    }    

    private void InteractableLevels()
    {
        var MaxLevel = StateManager.Instance.LevelsAvailable;


        for (int i = 0; i < MaxLevel; i++)
        {
            if (i + 1 != MaxLevel)
            {
                _levels[i].image.sprite = FinishedLevel;


            }
            try
            {
                _levels[i].interactable = true;
                
                var billImage2 = Instantiate(BillObject, _levels[i].transform, false);
                var bill = billImage2.GetComponentsInChildren<Text>();
                bill[0].text = PretendData.Instance.Data[i].BSDataName;
                bill[1].text = PretendData.Instance.Data[i].BSDataAmount + " kr";

            }
            catch 
            {

                Debug.Log("failed completion of castle buttton " + i);
            }
        }
    }

}
