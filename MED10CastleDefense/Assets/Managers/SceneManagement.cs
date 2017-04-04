using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    private void Awake()
    {
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
