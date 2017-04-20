using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableOverview : MonoBehaviour {

    public GameObject rowPrefab;
    private GameObject _topRow, _botRow;

    private List<TableOverviewRow> bills = new List<TableOverviewRow>();
    int testNum = 0;

    private void Awake()
    {
        _topRow = transform.GetChild(0).gameObject;
        _botRow = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TableOverviewRow newBill = Instantiate(rowPrefab, transform).GetComponent<TableOverviewRow>();
            newBill.Fill(PretendData.Instance.Data[testNum]);
            bills.Add(newBill);
            testNum++;
            _botRow.transform.SetAsLastSibling();
            UpdateTotal();
        }
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
    }
}
