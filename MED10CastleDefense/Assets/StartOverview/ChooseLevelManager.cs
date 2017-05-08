using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChooseLevelManager : MonoBehaviour {

    public RectTransform castleParent;

    private Button[] _levels;

    [SerializeField]
    private GameObject SelectedLevelIcon = null, BillObject = null, LevelCastle = null;
    [SerializeField]
    private Sprite FinishedLevel;
    private static bool _showIntroHint = true;
    private ScrollRect _scrollRect;
    private int basesToSpawn = 0;


    //private void Awake()
    //{
    //    _levels = GetComponentsInChildren<Button>();
    //    InteractableLevels();
    //    foreach (var button in _levels)
    //    {
    //        if (button.IsInteractable())
    //        {
    //            button.onClick.AddListener(() => ButtonPress(button.name));

    //        }
    //    }
    //    if (StateManager.Instance.SelectedLevel != 0)
    //    {
    //        try
    //        {
    //            SelectedLevelIcon.transform.SetParent(_levels[StateManager.Instance.LevelsAvailable -1].transform);
    //            StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable-1].BSDataName;
    //            EventManager.TriggerEvent("SelectedLevel");

    //            StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable;


    //        }
    //        catch
    //        {
    //            Debug.Log("catch");
    //            SelectedLevelIcon.transform.parent = _levels[(StateManager.Instance.LevelsAvailable)-2].transform;
    //            StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable -1;
    //            StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable - 2].BSDataName;


    //        }
    //        SelectedLevelIcon.transform.localPosition = Vector3.zero;
    //        SelectedLevelIcon.transform.localPosition = new Vector3(5f, 114f, 0f);

    //    }

    //    if (_showIntroHint)
    //    {
    //        HintManager.Instance.CreateHint(0);
    //        _showIntroHint = false;
    //    }



    //}

    private void Awake()
    {
        if (castleParent == null)
            castleParent = GameObject.Find("BaseParent").GetComponent<RectTransform>();

        _scrollRect = GetComponent<ScrollRect>();

        basesToSpawn = PretendData.Instance.Data.Length;

        FillCastleParent();
        
        _levels = castleParent.GetComponentsInChildren<Button>();
        InteractableLevels();
        AddListeners();
        PlaceSelectedLevelIcon();

        if (_showIntroHint)
        {
            HintManager.Instance.CreateHint(0);
            _showIntroHint = false;
        }

        MoveScrollRectPosition();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            FillCastleParent();

        }

        if (Input.GetKeyDown(KeyCode.H))
        {

            float x = castleParent.rect.width / 2 - 250 * StateManager.Instance.SelectedLevel;
            castleParent.anchoredPosition = new Vector3(x, 0, 0);
            Debug.Log("Pos: " + x);
        }


        
    }


    private void InitializeCastleParentPosition()
    {
        //Disable scroll rect and place castleparent properly if no more than 7 bills are active.
        if (basesToSpawn <= 7)
        {
            _scrollRect.enabled = false;
            castleParent.sizeDelta = new Vector2(250 * basesToSpawn, 275);
            castleParent.anchoredPosition = new Vector2(0, 0);
        } //535 + 150 * StateManager.Instance.SelectedLevel
        else if(basesToSpawn > 7)
        {
            _scrollRect.enabled = true;
        }
    }




    private void AddListeners()
    {
        foreach (var button in _levels)
        {
            if (button.IsInteractable())
            {
                button.onClick.AddListener(() => ButtonPress(button.name));

            }
        }
    }



    private void PlaceSelectedLevelIcon()
    {
        if (StateManager.Instance.SelectedLevel != 0)
        {
            try
            {
                SelectedLevelIcon.transform.SetParent(_levels[StateManager.Instance.LevelsAvailable - 1].transform);
                StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable - 1].BSDataName;
                EventManager.TriggerEvent("SelectedLevel");

                StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable;


            }
            catch
            {
                Debug.Log("catch");
                SelectedLevelIcon.transform.parent = _levels[(StateManager.Instance.LevelsAvailable) - 2].transform;
                StateManager.Instance.SelectedLevel = StateManager.Instance.LevelsAvailable - 1;
                StateManager.Instance.LevelName = PretendData.Instance.Data[StateManager.Instance.LevelsAvailable - 2].BSDataName;


            }
            SelectedLevelIcon.transform.localPosition = Vector3.zero;
            SelectedLevelIcon.transform.localPosition = new Vector3(5f, 114f, 0f);

            
        }
    }


    private void MoveScrollRectPosition()
    {
        if(_scrollRect.enabled == true)
        {
            //float firstBaseX = 0 + 250 * (basesToSpawn / 2);    //+ because x increases when you want to show earlier bases
            //float selBaseX = firstBaseX + 250 - 250 * StateManager.Instance.SelectedLevel;
            //Debug.Log("Base 1 x = " + GetBaseX(1));
             //Debug.Log("Base 14 x = " + GetBaseX(14));

            float x = 0;
            if (StateManager.Instance.SelectedLevel > 6)
            {
               
                if (basesToSpawn - StateManager.Instance.SelectedLevel >= 2)
                {
                    x = (StateManager.Instance.SelectedLevel - 6) * -250;
                    //print(distanceFrom6);
                    //x = GetBaseX(StateManager.Instance.SelectedLevel) + 475;
                }
                else
                {
                    x = (StateManager.Instance.SelectedLevel - 7) * -250;
                }

            }
            else
            {
                x = 0;
            }
            castleParent.anchoredPosition = new Vector3(x, 0, 0);


            //float maxLvlSelected = StateManager.Instance.SelectedLevel;

            //if (StateManager.Instance.SelectedLevel >= _levels.Length - 4)
            //{
            //    maxLvlSelected = _levels.Length - 4;
            //}
            
            
        }
       
    }



    private float GetBaseX(int baseNum)
    {
        float firstX = 0 + 250 * (basesToSpawn/2);
        float x = firstX + 250 - 250 * baseNum;

        if(basesToSpawn % 2 == 0)
        {
            x -= 125;
        }
     
        return x;
    }



    private void FillCastleParent()
    {
        InitializeCastleParentPosition();
        for (int i = 0; i < basesToSpawn; i++)
        {
            CreateNewLevelButton(i + 1);
        }
    }



    private void CreateNewLevelButton(int index)
    {
        GameObject newLvl = Instantiate(LevelCastle, castleParent, false);
        if(index >= StateManager.Instance.LevelsAvailable)
            newLvl.transform.GetChild(0).GetComponent<Text>().text = index.ToString();
        newLvl.name = "Level" + index.ToString();

        if(_scrollRect.enabled )
            castleParent.sizeDelta += new Vector2(newLvl.GetComponent<RectTransform>().rect.width + 90, 0);
    }



    private void ButtonPress(string NameButton)
    {
        int number;
        int.TryParse(NameButton.Substring(NameButton.Length - 1, 1),out  number);

        var data = PretendData.Instance.Data[number-1];
        var instance = StateManager.Instance;
        instance.LevelName = data.BSDataName;
        instance.SelectedLevel = number;
        EventManager.TriggerEvent("SelectedLevel");

        SelectedLevelIcon.transform.SetParent(_levels[number - 1].transform);
        SelectedLevelIcon.transform.localPosition = new Vector3(5f,114f,0f);

        
    }    



    private void InteractableLevels()
    {
        var MaxLevel = StateManager.Instance.LevelsAvailable;


        for (int i = 0; i < MaxLevel; i++)
        {
            if (i + 1 != MaxLevel)
            {
                _levels[i].image.sprite = FinishedLevel;


            }
            try
            {
                _levels[i].interactable = true;
                
                var billImage2 = Instantiate(BillObject, _levels[i].transform, false);
                billImage2.name = "BillObject";
                var bill = billImage2.GetComponentsInChildren<Text>();
                bill[0].text = PretendData.Instance.Data[i].BSDataName;
                if(bill[0].text.Length > 25)
                {
                    bill[0].text = bill[0].text.Substring(0, 25);
                }
                bill[1].text = PretendData.Instance.Data[i].BSDataAmount + " kr";

            }
            catch 
            {

                Debug.Log("failed completion of castle buttton " + i);
            }
        }
    }

}
