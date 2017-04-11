using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteText : MonoBehaviour {
    [SerializeField]
    private GameObject FinishMenu;

    [SerializeField]
    private Sprite WonImage;
    private float _timeSinceStart, _timeEnded;
    private bool _wonGame,_pressedFinish;


    private void Awake()
    {
        EventManager.StartListening("LevelComplete", LevelComplete);
        EventManager.StartListening("Upgrade", Upgraded);
        EventManager.StartListening("LevelLost", LevelLost);
        EventManager.StartListening("SpawnFirstUnit", StartTime);
        _wonGame = false;
    }
    void FirstTimePress()
    {
        _pressedFinish = true; 
        StartCoroutine(SlideRightIn(Time.time));
        GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();

    }

    IEnumerator SlideRightIn(float startTime)
    {
        Vector3 start = FinishMenu.transform.localPosition;
        Vector3 end = Vector3.zero;
        while (_pressedFinish)
        {
            yield return new WaitForFixedUpdate();
            var timeSinceStart = Time.time - startTime;
            var percentageComplete = timeSinceStart / 1f;

            FinishMenu.transform.localPosition = Vector3.Lerp(start, end, percentageComplete);

            if (percentageComplete >= 1f)
            {
                _pressedFinish = false;
                break;
            }


        }
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
        GetComponentsInChildren<Image>()[0].sprite = WonImage;
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
        GetComponentsInChildren<Text>()[0].text = StateManager.Instance.UpgradesAvailable.ToString();
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
        GetComponentsInChildren<Button>()[0].onClick.AddListener(() => FirstTimePress());


        //show stars if game won
        if (_wonGame)StarSystem();
        else GetComponentsInChildren<Image>()[6].gameObject.SetActive(false);
        //update text fields on end level screen
        var textFields = GetComponentsInChildren<Text>();
        var instance = StateManager.Instance;
        textFields[0].text = instance.UpgradesAvailable.ToString();
        //textFields[1].text = "Time taken: " + (_timeEnded - _timeSinceStart);

        //have scene change based on selection
        GetComponentsInChildren<Button>()[4].onClick.AddListener(() => EventManager.TriggerEvent("RestartLevel"));
        GetComponentsInChildren<Button>()[5].onClick.AddListener(() => EventManager.TriggerEvent("EndLevel"));

    }
}
