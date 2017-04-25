using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetInformation : MonoBehaviour {
    public GameObject InfoText;

	// Use this for initialization
	void Awake () {
        GetComponentInChildren<Button>().onClick.AddListener(() => InfoText.SetActive(!InfoText.activeSelf));
	}
	

}
