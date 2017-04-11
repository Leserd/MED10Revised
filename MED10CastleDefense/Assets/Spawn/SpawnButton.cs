﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    public GameObject unit;
    public Transform spawnLoc;
    public Image cooldownImg;
    private Button _btn;
    private Coroutine _cdCoroutine;
    private bool _canSpawn = false;
    private static bool _firstSpawn = true;

    void Start()
    {
        _btn = GetComponent<Button>();

        _btn.onClick.AddListener(() => SpawnPress(transform.name));

        if (spawnLoc == null)
            spawnLoc = GameObject.Find("UnitSpawnLoc").transform;

        if (cooldownImg == null)
            cooldownImg = GetComponentInChildren<Image>();

        _canSpawn = true;

        EventManager.StartListening("LevelComplete", DisableButton);
        EventManager.StartListening("LevelLost", DisableButton);

        if(gameObject.name == "Pig")
        {
            if (!PigStats.Unlocked)
                _btn.interactable = false;
        }
        if (gameObject.name == "Safe")
        {
            if (!SafeStats.Unlocked)
                _btn.interactable = false;
        }

    }


    //This is purely to test faster
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.name == "Coin")
        {
            SpawnPress("Coin");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.name == "Pig")
        {
            SpawnPress("Pig");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.name == "Safe")
        {
            SpawnPress("Safe");
        }
    }


    private void SpawnPress(string type)
    {
        if (_canSpawn)
        {
            float cdTime = 0;
            switch (type)
            {
                case "Coin":
                    cdTime = CoinStats.Cooldown;
                    break;
                case "Pig":
                    cdTime = PigStats.Cooldown;
                    break;
                case "Safe":
                    cdTime = SafeStats.Cooldown;
                    break;
                default:
                    Debug.LogWarning("Something went wrong with spawn");
                    break;
            }

            if (_cdCoroutine != null)
                StopCoroutine(_cdCoroutine);

            if (_firstSpawn)
            {
                _firstSpawn = false;
                EventManager.TriggerEvent("SpawnFirstUnit");
            }
           


            _cdCoroutine = StartCoroutine(StartCooldown(cdTime));

            Unit newUnit = Instantiate(unit, spawnLoc.position, Quaternion.identity).GetComponent<Unit>();
            newUnit.AssignStatValues(type);
            EventManager.Instance.Spawn(newUnit.gameObject);
        }
    }



    private IEnumerator StartCooldown(float cdTime)
    {
        _btn.interactable = false;

        float startTime = Time.time;
        float endTime = startTime + cdTime;

        while (Time.time < endTime)
        {
            float elapsedTime = Time.time - startTime;
            cooldownImg.fillAmount = 1 - (elapsedTime / cdTime);
            yield return new WaitForFixedUpdate();

        }

        cooldownImg.fillAmount = 0;
        _btn.interactable = true;
        _cdCoroutine = null;

    }



    public void DisableButton()
    {
        _btn.interactable = false;
        if (_cdCoroutine != null)
            StopCoroutine(_cdCoroutine);
        cooldownImg.fillAmount = 0;

        _canSpawn = false;
    }

}
