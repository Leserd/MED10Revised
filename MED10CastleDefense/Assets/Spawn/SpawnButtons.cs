using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtons : MonoBehaviour {
    [SerializeField]
    private GameObject unit;


	// Use this for initialization
	void Awake () {
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() => SpawnPress(button.name));
        }
		
	}

    private void SpawnPress(string buttonName)
    {
        switch (buttonName)
        {
            case "Coin":
                //spawn coin
                Debug.Log("Spawn coin");
                var coin = Instantiate(unit,transform,false);
                coin.name = "CoinUnit";
                break;
            case "Pig":
                //spawn pig
                Debug.Log("Spawn pig");
                var pig = Instantiate(unit, transform, false);
                pig.name = "PigUnit";
                pig.GetComponent<Image>().color = Color.cyan;
                pig.transform.position += new Vector3(300f, 0f);
                break;
            case "Box":
                //spawn box
                Debug.Log("Spawn box");
                var box = Instantiate(unit, transform, false);
                box.name = "BoxUnit";
                box.GetComponent<Image>().color = Color.magenta;
                box.transform.position += new Vector3(150f, 0f);


                break;
            default:
                Debug.LogWarning("Something went wrong with spawn");
                break;
        }
    }

}
