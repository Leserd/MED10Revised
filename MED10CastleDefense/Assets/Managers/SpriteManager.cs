using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {

    [SerializeField]
    private Sprite _upgradeSprite = null, _nonInteractable = null, _unlockSprite = null, _lockedSprite = null;


    public void Upgrade()
    {
        GetComponent<Button>().image.sprite = _upgradeSprite;
    }
    public void NonInteractable()
    {
        var button = GetComponent<Button>();
            button.image.sprite = _nonInteractable;
        button.interactable = false;

    }
    public void Unlock()
    {
        try
        {
            GetComponent<Button>().image.sprite = _unlockSprite;
        }
        catch (System.Exception)
        {

            throw;
        } 

    }
    public void Locked()
    {
        try
        {
            var button = GetComponent<Button>();
            button.image.sprite = _lockedSprite;
            button.interactable = false;
        }
        catch (System.Exception)
        {

            throw;
        } 

    }


}
