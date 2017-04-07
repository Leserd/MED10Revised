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
        SceneManager.LoadScene("Level");
    }

    void RestartLevel()
    {
        EventManager.ResetEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void EndLevel()
    {
        EventManager.ResetEvents();
        SceneManager.LoadScene("LevelOverviewScene");
    }

}
