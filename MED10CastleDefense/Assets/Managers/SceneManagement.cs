using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    private static SceneManagement _instance = null;
    
    public static SceneManagement Instance
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
                _instance = stateManagerObject.AddComponent<SceneManagement>();
                DontDestroyOnLoad(stateManagerObject);
            }
            return _instance;
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
        DontDestroyOnLoad(gameObject);
        EventManager.StartListening("StartLevel", LevelStart);
        EventManager.StartListening("RestartLevel", RestartLevel);
        EventManager.StartListening("EndLevel", EndLevel);
    }

    void LevelStart()
    {
        Debug.Log("Attempt Level start");
        SceneManager.LoadScene("LevelScene");
    }

    void RestartLevel()
    {
        Debug.Log("Attempt restart level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void EndLevel()
    {
        Debug.Log("Attempt end level");
        SceneManager.LoadScene("LevelOverviewScene");
    }

}
