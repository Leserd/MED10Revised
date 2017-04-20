using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillOverview : MonoBehaviour {

    Text[] panelTexts;
    Transform monthsParent;
    Transform totalParent;
    public Sprite[] monthsToPay;
    public Sprite[] monthsToNotPay;
    


    void Awake()
    {
        panelTexts = GetComponentsInChildren<Text>();
        monthsParent = transform.GetChild(4);
        ///totalParent = transform.GetChild(5);
        EventManager.StartListening("SelectedLevel", SelectedLevel);
    }



    private void Start()
    {
        SelectedLevel();

        if(StateManager.Instance.LevelsAvailable == 2 && StateManager.Instance.NewLevelComplete == true)
        {
            HintManager.Instance.CreateHint(9, Vector3.zero);
        }
    }



    void SelectedLevel()
    {
        int lvl = StateManager.Instance.SelectedLevel - 1;
        panelTexts[0].text = PretendData.Instance.Data[lvl].BSDataName;
        panelTexts[1].text = PretendData.Instance.Data[lvl].BSDataAmountMonthly + " kr";
        panelTexts[2].text = PretendData.Instance.Data[lvl].BSDataFrequency;
        panelTexts[3].text = PretendData.Instance.Data[lvl].BSDataAmount + " kr";

        //Show payment months
        if (monthsToPay.Length != 0 && monthsToNotPay.Length != 0)
        {
            for (int i = 0; i < monthsParent.childCount; i++)
            {
                Sprite paymentSprite = monthsToNotPay[i];

                if (PretendData.Instance.Data[lvl].BSDataPaymentMonths.Contains(i))
                {
                    paymentSprite = monthsToPay[i];
                }

                monthsParent.GetChild(i).GetComponent<Image>().sprite = paymentSprite;
            }
        }
    }

   
}
