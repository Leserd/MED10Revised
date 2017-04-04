using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNameTest : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Text>().text = StateManager.Instance.LevelName;
    }


}
