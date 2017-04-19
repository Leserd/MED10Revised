using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hint {

    private Sprite _sprite;
    private Button _hintObj;
    private static int _nextHintToShow = 0;
    private int _hintNumber = 0;



    public Hint(int num, Vector3 position)
    {
        //instantiate hint prefab
        GameObject newBtn = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Hint"), GameObject.Find("HintCanvas").transform);
        _hintObj = newBtn.GetComponent<Button>();

        //Assign sprite to hint
        _hintNumber = num;
        Sprite = Resources.Load<Sprite>("Sprites/Hints/Hint" + _hintNumber.ToString());
       
        //Debug.Log("Hint #" + _hintNumber + " created.");
       // _nextHintToShow++;

        //AddListener to button
        //Burde der ikke bare være en der lytter efter touch input her og laver destroy når der bliver trykket et eller andet sted?
        _hintObj.onClick.AddListener(() => DestroyHint());

        //Change position
        _hintObj.GetComponent<RectTransform>().localPosition = position;

        //_hintObj.transform.GetChild(0).GetComponent<Text>().text = _hintNumber.ToString();

        //Enable hint
        ShowHint();

        //Add to hintmanager activehints
       // HintManager.Instance.AddActiveHint(this);

    }



    public void ShowHint()
    {
        //TODO: Fancy måde hints popper frem på. 
        _hintObj.gameObject.SetActive(true);
    }



    public void DestroyHint()
    {
        //Debug.Log("Destroying hint #" + _hintNumber);

        //Hvis hintet skal vise ny hint når det lukkes, gør det her:
        if (_hintNumber == 0 || _hintNumber == 1 || _hintNumber == 3 || _hintNumber == 4 || _hintNumber == 5 || _hintNumber == 7)
        {
            Vector3 pos =  Vector3.zero;
            HintManager.Instance.CreateHint(_hintNumber+1, pos);
        }

        //TODO: Fancy måde hints fjernes på. 
        HintManager.Instance.RemoveHint(this);
        Object.Destroy(_hintObj.gameObject);
    }



    public Sprite Sprite
    {
        get
        {
            return _sprite;
        }

        set
        {
            _sprite = value;
            if (_sprite)
            {
                _hintObj.GetComponent<Image>().sprite = _sprite;
                //var recttrans = _hintObj.transform as RectTransform;
                //recttrans.sizeDelta = new Vector2(_sprite.textureRect.width, _sprite.textureRect.height);
            }
            else
                return;
        }
    }


    public Button HintObj
    {
        get
        {
            return _hintObj;
        }

        set
        {
            _hintObj = value;
        }
    }
}

