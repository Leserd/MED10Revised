using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour {

    public string LevelName;
    public int LevelNumber;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>UpdateState());

    }
    void UpdateState()
    {
        var instance = StateManager.Instance;
        instance.LevelName = LevelName;
        instance.SelectedLevel = LevelNumber;
        EventManager.TriggerEvent("SelectedLevel");
    }
}
