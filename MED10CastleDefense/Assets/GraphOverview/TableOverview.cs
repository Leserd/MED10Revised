using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TableOverview : MonoBehaviour {

    public GameObject rowPrefab;
    private GameObject _topRow, _botRow;

    private List<TableOverviewRow> bills = new List<TableOverviewRow>();
    int testNum = 0;

    private void Start()
    {
        EventManager.StartListening("EnableBudgetOverview", CreateTableOverview);
    }


    private void CreateTableOverview()
    {
        InputData[] sortedList = sort(PretendData.Instance.Data);
        foreach (InputData bill in sortedList)
        {
            CreateNewRow(bill);
        }
    }


    private InputData[] sort(InputData[] unsorted)
    {
        InputData[] sorted1 = unsorted.OrderBy(c => -int.Parse(c.BSDataAmountMonthly)).ToArray();

        InputData[] sorted = sorted1.OrderBy(c => -c.BSDataPaymentMonths.Count).ToArray();
        return sorted;
    }



    private void Awake()
    {
        _topRow = transform.GetChild(0).gameObject;
        _botRow = transform.GetChild(1).gameObject;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            //foreach(InputData bill in PretendData.Instance.Data)
            //{
            //    CreateNewRow(bill);
            //}
            CreateTableOverview();
            //CreateNewRow(PretendData.Instance.Data[testNum]);

            //testNum++;
        }
    }





    public void CreateNewRow(InputData bill)
    {
        TableOverviewRow newBill = Instantiate(rowPrefab, transform).GetComponent<TableOverviewRow>();
        newBill.Fill(bill);
        bills.Add(newBill);

        //Make sure the total-row is last
        _botRow.transform.SetAsLastSibling();

        UpdateTotal();
    }



    private void UpdateTotal()
    {
        Text[] botRowTexts = _botRow.transform.GetComponentsInChildren<Text>();

        botRowTexts[0].text = "Total";

        for (int i = 0; i < 12; i++)
        {
            int totalMonth = 0;
            for (int k = 0; k < bills.Count; k++)
            {
                int monthNum = 0;
                if (bills[k].GetRowText(i + 1).text != string.Empty)
                {
                    monthNum = int.Parse(bills[k].GetRowText(i + 1).text);
                }
                totalMonth += monthNum;
            }
            botRowTexts[i + 1].text = totalMonth.ToString();
        }

        botRowTexts[13].text = Mathf.RoundToInt((StateManager.Instance.YearlyExpense / 12)).ToString();
        botRowTexts[14].text = StateManager.Instance.YearlyExpense.ToString();
    }
}
