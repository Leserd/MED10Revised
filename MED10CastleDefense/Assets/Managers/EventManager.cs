using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!_instance)
                {
                    GameObject eventManager = new GameObject("EventManager");
                    _instance = eventManager.AddComponent<EventManager>();
                    DontDestroyOnLoad(eventManager);
                    _instance.Init();
                }
                else
                {
                    _instance.Init();
                }

            }
            return _instance;

        }
    }
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            gameObject.AddComponent<SceneManagement>();
        }
    }

    public static void StartListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
            //thisEvent += listener;
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            //thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (_instance == null) return;

        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName,out thisEvent))
        {
            thisEvent.RemoveListener(listener);
            //thisEvent -= listener;
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }


}
