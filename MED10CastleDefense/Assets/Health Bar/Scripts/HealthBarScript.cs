using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    private GameObject _target;
    private Image barForeground;    //The health bar to change size of when dealt damage
    private Image barBackground;    //The background of the health bar
    private int _health, _maxHealth;
    private RectTransform _transform;
    private bool _ready = false;    //has target been set?

    private void Awake()
    {
        barBackground = transform.GetChild(0).GetComponent<Image>();
        if (barBackground == null)
            Debug.LogError("No background found for health bar!");
        barBackground.enabled = false;

        barForeground = transform.GetChild(1).GetComponent<Image>();
        if (barForeground == null)
            Debug.LogError("No foreground found for health bar!");
        barForeground.enabled = false;

        EventManager.Damage += UpdateHealth;

        _transform = GetComponent<RectTransform>();

        _transform.SetParent(GameObject.Find("HealthBarCanvas").transform);

        EventManager.StartListening("LevelComplete", RemoveHealthBar);
        EventManager.StartListening("LevelLost", RemoveHealthBar);
    }



    private void Update()
    {
        if (_ready)
        {
            if (_target)
            {
                PlaceHealthBar();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }



    private void PlaceHealthBar()
    {
        _transform.position = Camera.main.WorldToScreenPoint(_target.transform.position);
        int yOffset = (int)(_target.GetComponent<SpriteRenderer>().sprite.rect.height * _target.transform.localScale.y) / 2 + 30;
        _transform.position = new Vector2(_transform.position.x, _transform.position.y + yOffset);
    }



    public void SetTarget(GameObject target)
    {
        _target = target;

        if (_target.GetComponent<Base>())
        {
            _maxHealth = _target.GetComponent<Base>().maxHealth;
        }
        else if (_target.GetComponent<Unit>())
        {
            _maxHealth = _target.GetComponent<Unit>().maxHealth;
        }

        _health = _maxHealth;

        //Change width of health bars
        int barWidth = (int)(_target.GetComponent<SpriteRenderer>().sprite.rect.width * _target.transform.localScale.x);
        barForeground.rectTransform.sizeDelta = new Vector2(barWidth, barForeground.rectTransform.sizeDelta.y);
        barBackground.rectTransform.sizeDelta = new Vector2(barWidth, barBackground.rectTransform.sizeDelta.y);
        _ready = true;

        PlaceHealthBar();

        barForeground.enabled = true;
        barBackground.enabled = true;
    }



    public void UpdateHealth(GameObject dealer, List<GameObject> receivers)
    {
        if (_target)
        {
            if (receivers.Contains(_target))
            {

                int amount = 0;

                if (dealer.tag == "EnemyBase")
                    amount = dealer.GetComponent<BaseAttack>().damage;
                else if (dealer.tag == "Unit")
                {
                    amount = dealer.GetComponent<Unit>().damage;
                    amount = Mathf.CeilToInt((float)_maxHealth / 100f * (float)amount);
                }

                _health -= amount;
                
                float fill = (float)_health / (float)_maxHealth;
                //print("HP: " + _health + ", dmg: " + amount + ", fill: " + fill);
                barForeground.fillAmount = fill;
            }
        }
    }


    public void RemoveHealthBar()
    {
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        EventManager.Damage -= UpdateHealth;
        EventManager.StopListening("LevelComplete", RemoveHealthBar);
        EventManager.StopListening("LevelLost", RemoveHealthBar);
    }
}
