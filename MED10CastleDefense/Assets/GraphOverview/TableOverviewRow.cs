using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableOverviewRow : MonoBehaviour {

    private Text[] _rowTexts;   //0 = name, 1-12 = months, 13 = avg, 14 = total
    private Image _background;
    private Color _colorWhite = new Color(1, 1, 1, 0.7f);
    private Color _colorGrey = new Color(0.9f, 0.9f, 0.9f, 0.7f);

    private void Awake()
    {

        _rowTexts = transform.GetComponentsInChildren<Text>();
        print(_rowTexts.Length);
        _background = GetComponent<Image>();
        _background.color = transform.GetSiblingIndex() % 2 > 0 ? _colorWhite : _colorGrey;
    }


    public void Fill(InputData bill)
    {
        _rowTexts[0].text = bill.BSDataName;
        for (int i = 0; i < bill.BSDataPaymentMonths.Count; i++)
        {
            _rowTexts[bill.BSDataPaymentMonths[i] + 1].text = bill.BSDataAmountMonthly;
        }
        _rowTexts[13].text = Mathf.RoundToInt(int.Parse(bill.BSDataAmount) / 12).ToString();
        _rowTexts[14].text = Mathf.RoundToInt(int.Parse(bill.BSDataAmount)).ToString();
        
    }

    public Text GetRowText(int index)
    {
        return _rowTexts[index];
    }
}
