using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InLevelSettings : MonoBehaviour {
    [SerializeField]
    GameObject Settings;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => OnClick());
    }
    private void OnClick()
    {
        // Add function for pause game

        Time.timeScale = 0f;
        Settings.SetActive(true);
        
        foreach (var button in Settings.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() => ButtonInSettings(button.name));
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("Faked a complete lvl");
            EventManager.TriggerEvent("LevelComplete"); //EventManager.TriggerEvent("LevelLost");
        }
    }

    private void ButtonInSettings(string buttonNum)
    {
        switch (buttonNum)
        {
            case "Resume":
                Time.timeScale = 1f;
                //resume game
                Settings.SetActive(false);
                break;
            case "Restart":
                //Restart level
                Time.timeScale = 1f;
                EventManager.TriggerEvent("RestartLevel");
                break;
            case "Quit":
                //quit level
                Time.timeScale = 1f;
                StateManager.Instance.NewLevelComplete = false;
                EventManager.TriggerEvent("EndLevel");
                break;
            case "FakeFinish":
                Time.timeScale = 1f;
                Debug.Log("Faked a complete lvl");
                EventManager.TriggerEvent("LevelComplete"); //EventManager.TriggerEvent("LevelLost");
                break;

            default:
                Debug.LogWarning("None of the options were chosen");
                break;
        }
    }
    


}
