using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedBar : MonoBehaviour {

    private float _oldFillamount, _newFillamount,_selectedLevel,_maxLevel;
    private float _timeStartLerp;
    private bool _resize;



    private void Awake()
    {
        _timeStartLerp = Time.time;
        _resize = true;
        _selectedLevel =(float) StateManager.Instance.SelectedLevel;
        _maxLevel = (float)StateManager.Instance.LevelsAvailable;
        CurrentBar();

    }

    void CurrentBar()
    {
        var image = GetComponentsInChildren<Image>()[1];
        image.fillAmount = (_maxLevel - 1) / (float)PretendData.Instance.Data.Length ;

        if (StateManager.Instance.NewLevelComplete)
        {

            StartCoroutine(SlideRightIn(image));

        }

    }

    IEnumerator SlideRightIn(Image slideImage)
    {
        float oldSize =( _selectedLevel - 1) / PretendData.Instance.Data.Length;
        float newSize = _selectedLevel / PretendData.Instance.Data.Length;

        while (_resize)
        {
            yield return new WaitForFixedUpdate();
            var timeSinceStart = Time.time - _timeStartLerp;
            var percentageComplete = timeSinceStart / 1f;

            slideImage.fillAmount = Mathf.Lerp(oldSize,newSize, percentageComplete);

            if (percentageComplete >= 1f)
            {
                _resize = false;
                break;
            }


        }
    }

}
