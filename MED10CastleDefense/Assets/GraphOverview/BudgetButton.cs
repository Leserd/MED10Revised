using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetButton : MonoBehaviour {

    private Text[] totalBudgetTexts;
    private GameObject buttonPanel, tableOverview, barOverview;
    private Button totalBudget, tableBtn, barChartBtn, sendBtn;
    private Vector3 _startPos, _endPos = new Vector3(0, 300, 0);
    private bool _moving = false;
    private float _slideSpeed = 2f;

    [SerializeField]
    private GameObject _endgameScreen;


    private void Awake()
    {
        
        totalBudget = transform.GetChild(0).GetComponent<Button>();
            buttonPanel = totalBudget.transform.GetChild(1).gameObject;
                tableBtn = buttonPanel.transform.GetChild(0).GetComponent<Button>();
                barChartBtn = buttonPanel.transform.GetChild(1).GetComponent<Button>();
                sendBtn = buttonPanel.transform.GetChild(2).GetComponent<Button>();
            totalBudgetTexts = totalBudget.transform.GetChild(0).GetComponentsInChildren<Text>();
        tableOverview = transform.GetChild(1).gameObject;
        barOverview = transform.GetChild(2).gameObject;

        tableBtn.onClick.AddListener(() => tableOverview.GetComponent<TableOverview>().ToggleDisplay());
        barChartBtn.onClick.AddListener(() => barOverview.GetComponent<CreateCharts>().ToggleDisplay());
        sendBtn.onClick.AddListener(() => _endgameScreen.SetActive(!_endgameScreen.activeSelf));


        totalBudget.enabled = false;

        _startPos = totalBudget.transform.localPosition;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            totalBudget.onClick.AddListener(() => ToggleDisplay());
            totalBudget.enabled = true;
        }
    }





    public void Start()
    {
        BudgetUpdate();
        EventManager.StartListening("SelectedLevel", BudgetUpdate);
        EventManager.StartListening("EnableBudgetOverview", EnableBudget);
        if (PretendData.Instance.Data.Length < StateManager.Instance.LevelsAvailable)
        {
            EnableBudget();
        }

        if (StateManager.Instance.LevelsAvailable == PretendData.Instance.Data.Length + 1 && StateManager.Instance.NewLevelComplete == true)
        {
            HintManager.Instance.CreateHint(12);
        }
    }



     void EnableBudget()
    {
        totalBudget.onClick.AddListener(() => ToggleDisplay());
        totalBudget.enabled = true;
    }



    public void BudgetUpdate()
    {
        totalBudgetTexts[0].text = Mathf.RoundToInt((float)StateManager.Instance.YearlyExpense / 12).ToString() + " kr";
        totalBudgetTexts[1].text = StateManager.Instance.YearlyExpense.ToString() + " kr";
    }


    public void ToggleDisplay()
    {
        //buttonPanel.SetActive(!buttonPanel.activeSelf);
        if (!_moving) StartCoroutine(Slide(Time.time));
    }




    IEnumerator Slide(float startTime)
    {
        Vector3 start = _startPos;
        Vector3 end = _endPos;
        if(totalBudget.transform.localPosition != _startPos)
        {
            start = _endPos;
            end = _startPos;
        }
        _moving = true;

        while (_moving)
        {
            yield return new WaitForFixedUpdate();
            var timeSinceStart = Time.time - startTime;
            var percentageComplete = timeSinceStart / 1f * _slideSpeed;

            totalBudget.transform.localPosition = Vector3.Lerp(start, end, percentageComplete);

            if (percentageComplete >= 1f)
            {
                totalBudget.transform.localPosition = end;
                _moving = false;

                break;
            }


        }
    }
}
