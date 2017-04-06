using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestLevelOverview : MonoBehaviour, IPointerClickHandler {



    public void OnPointerClick(PointerEventData data)
    {
        var instance = StateManager.Instance;
        if (data.pointerId == -1)
        {
            Debug.Log("left click");
            instance.LevelsAvailable =1;
            EventManager.TriggerEvent("LevelIncrease");
        }
        if (data.pointerId == -2)
        {
            Debug.Log("Right Click");
            instance.LevelsAvailable = -1;
        }
    }



}
