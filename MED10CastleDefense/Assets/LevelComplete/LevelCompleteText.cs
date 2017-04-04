using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteText : MonoBehaviour {
    [SerializeField]
    private GameObject FinishMenu;


    private void Awake()
    {
        EventManager.StartListening("LevelComplete", LevelComplete);
        EventManager.StartListening("Upgrade", Upgraded);
        EventManager.StartListening("LevelLost", LevelLost);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("LevelComplete", LevelComplete);
        EventManager.StopListening("Upgrade", Upgraded);
        EventManager.StopListening("LevelLost", LevelLost);

    }


    private void Upgraded()
    {
        GetComponentsInChildren<Text>()[3].text = "Upgrades available: " + StateManager.Instance.UpgradesAvailable.ToString();
    }
    private void LevelLost()
    {
        StateManager.Instance.UpgradesAvailable = 1;
        UpdateFinishMenu();
    }
    private void LevelComplete()
    { 
        StateManager.Instance.UpgradesAvailable = 2;
        if (StateManager.Instance.MaxLevel == StateManager.Instance.SelectedLevel)
        {
            StateManager.Instance.YearlyExpense = int.Parse(PretendData.instance.Data[StateManager.Instance.SelectedLevel-1].BSDataAmount);

            StateManager.Instance.MaxLevel = 1;
        }

        UpdateFinishMenu();
    }
    private void UpdateFinishMenu()
    {

        FinishMenu.SetActive(true);//GetComponentsInChildren<RectTransform>()[1].gameObject.SetActive(true);
        var textFields = GetComponentsInChildren<Text>();
        // exp, level, upgrades 
        var instance = StateManager.Instance;
        textFields[1].text = "Experience: " + instance.Experience.ToString();
        textFields[2].text = "Player level: " + instance.PlayerLevel.ToString();
        textFields[3].text = "Upgrades available: " + instance.UpgradesAvailable.ToString(); ;
        textFields[4].text = "Available levels: " + instance.MaxLevel.ToString();
        GetComponentsInChildren<Button>()[3].onClick.AddListener(() => EventManager.TriggerEvent("EndLevel"));

    }
}
