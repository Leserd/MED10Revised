using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelManager : MonoBehaviour {

    private Button[] _levels;


    private void Awake()
    {
        _levels = GetComponentsInChildren<Button>();
        EventManager.StartListening("LevelIncrease", CompletedALevel);
    }


    private void CompletedALevel()
    {
        var states = StateManager.Instance;
        var MaxLevel = states.MaxLevel;
        if (MaxLevel > _levels.Length) MaxLevel = _levels.Length;

        for (int i = 0; i < MaxLevel; i++)
        {
            _levels[i].interactable = true;
        }

    }





}
