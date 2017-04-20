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
    private bool _pressedFinish, _levelCompleteRollDown = true;


    private void Awake()
    {
        EventManager.StartListening("LevelComplete", LevelComplete);
        EventManager.StartListening("Upgrade", Upgraded);
        EventManager.StartListening("LevelLost", LevelLost);
        EventManager.StartListening("SpawnFirstUnit", StartTime);
        
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

                Debug.Log("SlidedRightIn");

                if(StateManager.Instance.SelectedLevel == 1 && StateManager.Instance.NewLevelComplete == true)
                {
                    HintManager.Instance.CreateHint(7);
                }

                else if (StateManager.Instance.SelectedLevel == 2 && StateManager.Instance.NewLevelComplete == true)
                {
                    HintManager.Instance.CreateHint(11);
                }
                
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
        //Uncomment dette hvis du vil bruge tid i stedet for liv som årsag til stjerner
        /*
        var image = GetComponentsInChildren<Image>()[6];
        var timeDiff = _timeEnded - _timeSinceStart;
        if (timeDiff <= 30f)
        {
            StartCoroutine(stars(3, image));
            //image.sprite = Stars.A_Stars(3);
            return;
        }
        if (timeDiff <= 60)
        {
            StartCoroutine(stars(2, image));
            //image.sprite = Stars.A_Stars(2);
            return;
        }
        if (timeDiff <= 90)
        {
            image.sprite = Stars.A_Stars(1);
            GetComponentsInChildren<ParticleSystem>()[0].Play();
            return;
        }
        image.sprite = Stars.A_Stars(0);
        */

        //Livbaseret stjerneudregning
        var image = GetComponentsInChildren<Image>()[6];
        //var timeDiff = _timeEnded - _timeSinceStart;
        Base playerBase = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<Base>();
        float baseHpPercent = (float)playerBase.health / (float)playerBase.maxHealth;
        print(baseHpPercent);
        if (baseHpPercent == 1f)
        {
            StartCoroutine(stars(3, image));
            //image.sprite = Stars.A_Stars(3);
            return;
        }
        else if (baseHpPercent >= 0.5f)
        {
            StartCoroutine(stars(2, image));
            //image.sprite = Stars.A_Stars(2);
            return;
        }
        else if(baseHpPercent != 0f)
        {
            image.sprite = Stars.A_Stars(1);
            GetComponentsInChildren<ParticleSystem>()[0].Play();
            return;
        }
        image.sprite = Stars.A_Stars(0);
    }


    IEnumerator stars(int starsNum, Image starImage)
    {
        var particles = GetComponentsInChildren<ParticleSystem>();
        if (starsNum == 2)
        {
            starImage.sprite = Stars.A_Stars(1);
            particles[0].Play();
            while (particles[0].isEmitting)
            {
                yield return new WaitForFixedUpdate();
                if (!particles[0].isEmitting)
                {
                    starImage.sprite = Stars.A_Stars(2);
                    particles[1].Play();
                    break;
                }
            }
        }
        else
        {
            starImage.sprite = Stars.A_Stars(1);
            particles[0].Play();
            while (particles[0].isEmitting)
            {
                yield return new WaitForFixedUpdate();
                if (!particles[0].isEmitting)
                {
                    starImage.sprite = Stars.A_Stars(2);
                    particles[1].Play();
                    while (particles[1].isEmitting)
                    {
                        yield return new WaitForFixedUpdate();
                        if (!particles[1].isEmitting)
                        {
                            starImage.sprite = Stars.A_Stars(3);
                            particles[2].Play();
                            break;
                        }
                        }
                }
            }
        }
    }



    private void Upgraded()
    {
        GetComponentsInChildren<Text>()[0].text = StateManager.Instance.UpgradesAvailable.ToString();
    }



    private void LevelLost()
    {
        _timeEnded = Time.fixedTime;
        FinishMenu.SetActive(true);
        GetComponentsInChildren<Image>()[6].gameObject.SetActive(false);
        if (StateManager.Instance.LevelsAvailable == StateManager.Instance.SelectedLevel)
        {
            StateManager.Instance.UpgradesAvailable = 1;

        }
        UpdateFinishMenu();
        Debug.Log("LevelLost");

        StartCoroutine(WaitSecondsLost(Time.time));
    }



    IEnumerator WaitSecondsLost(float starttime)
    {

        Vector3 start = FinishMenu.transform.localPosition;
        Vector3 end = new Vector3(0f, 1083f, 0f);
        while (_levelCompleteRollDown)
        {
            yield return new WaitForFixedUpdate();
            var timeSinceStart = Time.time - starttime;
            var percentageComplete = timeSinceStart / 1f;

            FinishMenu.transform.localPosition = Vector3.Lerp(start, end, percentageComplete);

            if (percentageComplete >= 1f)
            {
                _levelCompleteRollDown = false;
                GetComponentsInChildren<Button>()[0].onClick.AddListener(() => FirstTimePress());

                break;
            }


        }

        Debug.Log("End of WaitSecondsLost");

    }



    IEnumerator WaitSeconds(float starttime)
    {

        Vector3 start = FinishMenu.transform.localPosition;
        Vector3 end = new Vector3 (0f, 1083f,0f);
        while (_levelCompleteRollDown)
        {
            yield return new WaitForFixedUpdate();
            var timeSinceStart = Time.time - starttime;
            var percentageComplete = timeSinceStart / 1.5f;

            FinishMenu.transform.localPosition = Vector3.Lerp(start, end, percentageComplete);

            if (percentageComplete >= 1f)
            {
                _levelCompleteRollDown = false;
                GetComponentsInChildren<Button>()[0].onClick.AddListener(() => FirstTimePress());

                //show stars if game won

                StarSystem();
                break;
            }
        }
        Debug.Log("End of WaitSeconds");

    }



    private void LevelComplete()
    {
        _timeEnded = Time.fixedTime;
        Debug.Log("LevelComplete");


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
        FinishMenu.SetActive(true);
        GetComponentsInChildren<Image>()[0].sprite = WonImage;
        UpdateFinishMenu();

        StartCoroutine(WaitSeconds(Time.time));

    }



    private void UpdateFinishMenu()
    {
        //show end level screen
        Debug.Log("UpdateFinishMenu");

        
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
