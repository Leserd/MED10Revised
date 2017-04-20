using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintManager : MonoBehaviour {

    private List<Hint> _activeHints = new List<Hint>();
    private static HintManager _instance;
    private List<int> _shownHints = new List<int>();

    public static HintManager Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                GameObject hintManagerObject = new GameObject("HintManager");
                _instance = hintManagerObject.AddComponent<HintManager>();
                DontDestroyOnLoad(hintManagerObject);
            }
            return _instance;
        }
    }



    public void Start()
    {
        if(_activeHints != null)
            _activeHints.Clear();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CreateHint(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CreateHint(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CreateHint(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CreateHint(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CreateHint(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CreateHint(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CreateHint(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            CreateHint(7);
        }
    }

    public void CreateHint(int num)
    {
        if (!_shownHints.Contains(num))
        {
            Hint hint = new Hint(num);
            AddActiveHint(hint);
            _shownHints.Add(num);
        }
        
    }


    public void AddActiveHint(Hint hint)
    {
        _activeHints.Add(hint);
    }


    public void RemoveHint(Hint hint)
    {
        _activeHints.Remove(hint);
    }



    public void DestroyActiveHints()
    {
        foreach(Hint hint in _activeHints)
        {
            hint.DestroyHint();
        }

        _activeHints.Clear();
    }
}
