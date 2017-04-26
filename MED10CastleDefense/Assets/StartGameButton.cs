using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour {

    GameObject currentLevel;


	// Use this for initialization
	void Start () {
        EventManager.StartListening("SelectedLevel", MoveButton);
        MoveButton();
	}

    void MoveButton()
    {
        if (currentLevel != null)
            currentLevel.transform.FindChild("BillObject").gameObject.SetActive(true);

        
        Transform bases = GameObject.Find("BaseScrollRect").transform;
        int lvl = StateManager.Instance.SelectedLevel;
        currentLevel = bases.GetChild(0).GetChild(lvl - 1).gameObject;
        transform.SetParent(currentLevel.transform);
        transform.SetAsLastSibling();
        currentLevel.transform.FindChild("BillObject").gameObject.SetActive(false);
       
        transform.localPosition = new Vector2(0, 195);
    }
}
