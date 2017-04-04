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

        
        Settings.SetActive(true);
        
        foreach (var button in Settings.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() => ButtonInSettings(button.name));
        }

    }

    private void ButtonInSettings(string buttonNum)
    {
        switch (buttonNum)
        {
            case "Resume":
                //resume game
                Settings.SetActive(false);
                break;
            case "Restart":
                //Restart level
                Debug.Log("Restarting level");
                EventManager.TriggerEvent("RestartLevel");
                break;
            case "Quit":
                //quit level
                Debug.Log("Quitting level");
                EventManager.TriggerEvent("EndLevel");
                break;

            default:
                Debug.LogWarning("None of the options were chosen");
                break;
        }
    }
    


}
