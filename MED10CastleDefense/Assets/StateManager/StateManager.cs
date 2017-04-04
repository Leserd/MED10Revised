﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    private static int _selectedLevel = 1,
                        _maxLevel = 1,
                        _experience = 0,
                        _upgradeAvailable = 2,
                        _playerLevel = 1;

    private static string _levelName;


    private static StateManager _instance = null;
    public static StateManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                GameObject stateManagerObject = new GameObject("StateManager");
                _instance = stateManagerObject.AddComponent<StateManager>();
                DontDestroyOnLoad(stateManagerObject);
            }
            return _instance;
        }
    }
    public string LevelName
    {
        get
        {
            if (_levelName != null)
            {
                return _levelName;

            }
            return "Could not find a level name";
        }
        set
        {
            _levelName = value;
        }
    }

    public int UpgradesAvailable
    {
        get
        {
            return _upgradeAvailable;
        }
        set
        {
            _upgradeAvailable += value;
        }
    }
    public int MaxLevel
    {
        ///TODO needs to be able to check for the max value that can be chosen.
        get
        {
            return _maxLevel;
        }
        set
        {
            if (value == 1 || value == -1)
            {
                _maxLevel += value;
                if(_maxLevel == 0)
                {
                    _maxLevel = 1;
                }
                
            }
        }
    }
    public int SelectedLevel
    {
        get
        {
            return _selectedLevel;
        }
        set
        {
            _selectedLevel = value;
        }
    }
    public int PlayerLevel
    {
        get
        {
            return _playerLevel;
        }
        set
        {
            _playerLevel = value;
        }
    }
    public int Experience
    {
        get
        {
            return _experience;
        }
        set
        {
            _experience = value;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
