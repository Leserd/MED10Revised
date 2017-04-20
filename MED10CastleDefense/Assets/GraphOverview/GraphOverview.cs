﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphOverview : MonoBehaviour {
    public Button next, last, exit;
    public Image graphImg;
    public Sprite[] graphs;
    private int _curIndex = 0;


    private void Awake()
    {
        if (graphImg == null)
            graphImg = transform.GetChild(0).GetComponent<Image>();
        if(next == null)
            next = transform.GetChild(1).GetComponent<Button>();
        if (last == null)
            last = transform.GetChild(2).GetComponent<Button>();
        if (exit == null)
            exit = transform.GetChild(3).GetComponent<Button>();

        next.onClick.AddListener(() => ShowNewImage(1));
        last.onClick.AddListener(() => ShowNewImage(-1));
        exit.onClick.AddListener(() => ToggleDisplay());

        if (graphs.Length == 0)
            this.enabled = false;
        else
            graphImg.sprite = graphs[0];

        last.interactable = false;
    }


    public void ShowNewImage(int direction)
    {
        _curIndex += direction;
        if (_curIndex <= 0)
        {
            _curIndex = 0;
            last.interactable = false;
            next.interactable = true;
            
        }
        else if (_curIndex >= graphs.Length - 1)
        {
            _curIndex = graphs.Length - 1;
            last.interactable = true;
            next.interactable = false;
        }
        else
        {
            last.interactable = true;
            next.interactable = true;
        }

        graphImg.sprite = graphs[_curIndex];
    }


    public void ToggleDisplay()
    {
        /// GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
        gameObject.SetActive(!gameObject.activeSelf);
    }

}



