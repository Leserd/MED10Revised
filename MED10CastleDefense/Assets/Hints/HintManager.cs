using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintManager : MonoBehaviour {

    private List<Hint> _activeHints = new List<Hint>();
    private static HintManager _instance;
    

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
            CreateHint(0, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CreateHint(1, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CreateHint(2, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CreateHint(3, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CreateHint(4, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CreateHint(5, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CreateHint(6, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            CreateHint(7, new Vector3(Random.Range(-900f, 900f), Random.Range(-500f, 500f), 0));
        }
    }

    public void CreateHint(int num, Vector3 position)
    {
        Hint hint = new Hint(num, position);
        AddActiveHint(hint);
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
