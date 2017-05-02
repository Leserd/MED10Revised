using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadCSVtoInputdata : MonoBehaviour {

    private void Awake()
    {
        var fileLoad = Resources.LoadAll("ReadActiveCSV")[0] as TextAsset;
        var csvToSorted = CSVReader.Read(fileLoad);

        ReplaceFakeInput(csvToSorted);


    }

    private void ReplaceFakeInput(List<Dictionary<string, object>> newData)
    {
        List<InputData> data = new List<InputData>();
        for (int i = 0; i < newData.Count; i++)
        {
            InputData entry = new InputData();
            entry.BSDataName = newData[i]["Regningens navn"].ToString();
            entry.BSDataAmount = newData[i]["årligt beløb på regning"].ToString();
            entry.BSDataAmountMonthly = newData[i]["regningens beløb pr gang"].ToString();
            entry.BSDataFrequency = ReturnFrequency(newData[i]["hvor ofte der betales i tal  (månedlig = 12 kvartalt = 4 etc)"].ToString());
            entry.BSDataPaymentMonths = ReturnPaymentMonths(newData[i]["første betalingsmåned i tal"].ToString(), newData[i]["hvor ofte der betales i tal  (månedlig = 12 kvartalt = 4 etc)"].ToString());
            entry.ID = i;

            data.Add(entry);
        }
        PretendData.Instance.ReplaceData(data.ToArray());


        
    }
    private List<int> ReturnPaymentMonths(string firstOccurance, string numberOfOccurences)
    {
        var firstOccur = int.Parse(firstOccurance);
        var numberOccur = int.Parse(numberOfOccurences);
        switch (numberOccur)
        {
            case 12:
                return new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            case 4:
                var list = new List<int>();
                for (int i = 0; i < 4; i++)
                {
                    list.Add(firstOccur);
                    firstOccur =firstOccur + 3;
                }
                return list;
            case 2:

                return new List<int> { firstOccur, firstOccur + 6 };
            case 1:
                return new List<int> { firstOccur };

            default:
                return null;
        }
    }

    private string ReturnFrequency(string months)
    {
        switch (months)
        {
            case "12":
                return "Månedligt";
            case "4":
                return "Kvartalt";
            case "2":
                return "Halvårligt";
            case "1":
                return "Årligt";
            default:
                return "unknown";
        }
    }
}
