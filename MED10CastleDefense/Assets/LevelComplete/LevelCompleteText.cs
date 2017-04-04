using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteText : MonoBehaviour {


    private void Awake()
    {
        EventManager.StartListening("LevelComplete", levelComplete);
        EventManager.StartListening("Upgrade", Upgraded);
    }
    private void OnDisable()
    {
        //EventManager.StopListening("LevelComplete", levelComplete);
    }


    private void Upgraded()
    {
        GetComponentsInChildren<Text>()[2].text = "Upgrades available: " + StateManager.Instance.UpgradesAvailable.ToString();
    }
    public void levelComplete()
    {
        transform.parent.gameObject.SetActive(true);
        var textFields = GetComponentsInChildren<Text>();
        // exp, level, upgrades 
        var instance = StateManager.Instance;
        textFields[0].text = "Experience: "+instance.Experience.ToString();
        textFields[1].text = "Player level: " +instance.PlayerLevel.ToString();
        textFields[2].text = "Upgrades available: " + instance.UpgradesAvailable.ToString(); ;
        textFields[3].text = "Available levels: " + instance.MaxLevel.ToString();
        
    }
}
