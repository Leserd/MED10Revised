using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteText : MonoBehaviour {
    [SerializeField]
    private GameObject FinishMenu;
    private float _timeSinceStart, _timeEnded;
    private bool _wonGame;


    private void Awake()
    {
        EventManager.StartListening("LevelComplete", LevelComplete);
        EventManager.StartListening("Upgrade", Upgraded);
        EventManager.StartListening("LevelLost", LevelLost);
        EventManager.StartListening("SpawnFirstUnit", StartTime);


        _wonGame = false;
    }
    private void StartTime()
    {
        _timeSinceStart = Time.fixedTime;
    }
    private void OnDestroy()
    {
        EventManager.StopListening("LevelComplete", LevelComplete);
        EventManager.StopListening("Upgrade", Upgraded);
        EventManager.StopListening("LevelLost", LevelLost);
        EventManager.StopListening("SpawnFirstUnit", StartTime);
    }

    private void StarSystem()
    {
        Debug.Log("Ran StarSystem");
        var image = GetComponentsInChildren<Image>()[6];
        var timeDiff = _timeEnded - _timeSinceStart;
        if (timeDiff <= 30f)
        {
            image.sprite = Stars.A_Stars(3);
            return;
        }
        if (timeDiff <=60)
        {
            image.sprite = Stars.A_Stars(2);
            return;
        }
        if (timeDiff <= 90)
        {
            image.sprite = Stars.A_Stars(1);
            return;
        }
        image.sprite = Stars.A_Stars(0);

    }
    private void Upgraded()
    {
        GetComponentsInChildren<Text>()[0].text = "Upgrades available: " + StateManager.Instance.UpgradesAvailable.ToString();
    }
    private void LevelLost()
    {
        _timeEnded = Time.fixedTime;

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

    private void LevelComplete()
    {
        _timeEnded = Time.fixedTime;
        _wonGame = true;
        StartCoroutine(WaitSeconds(2f));

    }
    private void UpdateFinishMenu()
    {
        //show end level screen
        FinishMenu.SetActive(true);
        //show stars if game won
        if(_wonGame)StarSystem();
        //update text fields on end level screen
        var textFields = GetComponentsInChildren<Text>();
        var instance = StateManager.Instance;
        textFields[0].text = "Upgrades available: " + instance.UpgradesAvailable.ToString();
        textFields[1].text = "Time taken: " + (_timeEnded - _timeSinceStart);

        //have scene change based on selection
        GetComponentsInChildren<Button>()[3].onClick.AddListener(() => EventManager.TriggerEvent("RestartLevel"));
        GetComponentsInChildren<Button>()[4].onClick.AddListener(() => EventManager.TriggerEvent("EndLevel"));

    }
}
