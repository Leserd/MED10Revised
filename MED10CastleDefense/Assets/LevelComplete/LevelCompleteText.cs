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
        GetComponentsInChildren<Text>()[0].text = "Upgrades available: " + StateManager.Instance.UpgradesAvailable.ToString();
    }
    private void LevelLost()
    {
        StartCoroutine(WaitSecondsLost(2f));
    }
    IEnumerator WaitSecondsLost(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (StateManager.Instance.LevelsAvailable == StateManager.Instance.SelectedLevel)
        {
            StateManager.Instance.UpgradesAvailable = 1;

        }
        UpdateFinishMenu();

    }
    private void LevelComplete()
    {
        StartCoroutine(WaitSeconds(2f));

    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (StateManager.Instance.LevelsAvailable == StateManager.Instance.SelectedLevel)
        {
            if (StateManager.Instance.LevelsAvailable <= PretendData.Instance.Data.Length)
            {
                StateManager.Instance.YearlyExpense = int.Parse(PretendData.Instance.Data[StateManager.Instance.SelectedLevel - 1].BSDataAmount);
                StateManager.Instance.UpgradesAvailable = 2;
                StateManager.Instance.LevelsAvailable = 1;
                StateManager.Instance.NewLevelComplete = true;
            }
            else
            {
                StateManager.Instance.UpgradesAvailable = 1;
                StateManager.Instance.NewLevelComplete = false;

            }
        }
        else
        {
            StateManager.Instance.UpgradesAvailable = 1;
            StateManager.Instance.NewLevelComplete = false;


        }

        UpdateFinishMenu();
    }
    private void UpdateFinishMenu()
    {

        FinishMenu.SetActive(true);
        var textFields = GetComponentsInChildren<Text>();
        // exp, level, upgrades 
        var instance = StateManager.Instance;
        textFields[0].text = "Upgrades available: " + instance.UpgradesAvailable.ToString();
        GetComponentsInChildren<Button>()[3].onClick.AddListener(() => EventManager.TriggerEvent("RestartLevel"));

        GetComponentsInChildren<Button>()[4].onClick.AddListener(() => EventManager.TriggerEvent("EndLevel"));

    }
}
