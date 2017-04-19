using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public GameObject healthObject;   //The parent of health icon and text - disabled if not a base

    private GameObject _target;
    private Image _barForeground;    //The health bar to change size of when dealt damage
    private Image _barBackground;    //The background of the health bar
    private Text _healthText;           
    private int _health, _maxHealth;
    private RectTransform _transform;
    private bool _ready = false;    //has target been set?

    private void Awake()
    {
        _barBackground = transform.GetChild(0).GetComponent<Image>();
        if (_barBackground == null)
            Debug.LogError("No background found for health bar!");
        _barBackground.enabled = false;

        _barForeground = transform.GetChild(1).GetComponent<Image>();
        if (_barForeground == null)
            Debug.LogError("No foreground found for health bar!");
        _barForeground.enabled = false;

        EventManager.Damage += UpdateHealth;

        _transform = GetComponent<RectTransform>();

        if (healthObject)
        {
            _healthText = healthObject.transform.GetChild(1).GetComponent<Text>();
            healthObject.SetActive(false);
        }

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
        int offsetAmount = 30;
        if (_target.tag == "EnemyBase")
            offsetAmount = -30;
        else if (_target.tag == "Unit")
            offsetAmount = -100;

        _transform.position = Camera.main.WorldToScreenPoint(_target.transform.position);
        int yOffset = (int)(_target.GetComponent<SpriteRenderer>().sprite.rect.height * _target.transform.localScale.y) / 2 + offsetAmount;
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
        if(_target.tag == "Unit")
        {
            barWidth /= 2;
        }

        _barForeground.rectTransform.sizeDelta = new Vector2(barWidth, _barForeground.rectTransform.sizeDelta.y);
        _barBackground.rectTransform.sizeDelta = new Vector2(barWidth, _barBackground.rectTransform.sizeDelta.y);
        _ready = true;

        if (_target.tag == "EnemyBase")
        {
            _healthText.text = _health.ToString() + " / " + _maxHealth.ToString();
            healthObject.SetActive(true);
        }

        PlaceHealthBar();

        _barForeground.enabled = true;
        _barBackground.enabled = true;
    }



    public void UpdateHealth(GameObject dealer, List<GameObject> receivers)
    {
        if (_target)
        {
            if (receivers.Contains(_target))
            {

                int amount = 0;

                if (dealer.tag == "EnemyBase")
                {
                    amount = dealer.GetComponent<BaseAttack>().damage;
                    
                }
                    
                else if (dealer.tag == "Unit")
                {
                    amount = dealer.GetComponent<Unit>().damage;
                    amount = Mathf.CeilToInt((float)_maxHealth / 100f * (float)amount);
                }

                _health -= amount;
                
                if(_target.tag == "EnemyBase")
                {
                    _healthText.text = _health.ToString() + " / " + _maxHealth.ToString();
                    healthObject.SetActive(true);
                }

                float fill = (float)_health / (float)_maxHealth;
                //print("HP: " + _health + ", dmg: " + amount + ", fill: " + fill);
                _barForeground.fillAmount = fill;
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
