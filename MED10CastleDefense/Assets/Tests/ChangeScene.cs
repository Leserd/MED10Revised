﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => EventManager.TriggerEvent("StartLevel"));
    }


}
