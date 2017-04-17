using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    private static int _selectedLevel = 1,
                        _maxLevel = 1,
                        _experience = 0,
                        _upgradeAvailable = 0,
                        _yearlyExpence = 0;
    private static bool _completedNewLevel = false;

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

    public bool NewLevelComplete
    {
        get
        {
            return _completedNewLevel;
        }
        set
        {
            _completedNewLevel = value;
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
            return PretendData.Instance.Data[0].BSDataName;
        }
        set
        {
            _levelName = value;
        }
    }
    public int YearlyExpense
    {
        get
        {
            return _yearlyExpence;
        }
        set
        {
            if (_selectedLevel == _maxLevel)
            {
                _yearlyExpence += value;
            }
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
    public int LevelsAvailable
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
