using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    private GameObject _target;
    private Image barForeground;    //The health bar to change size of when dealt damage
    private Image barBackground;    //The background of the health bar
    private int _health, _maxHealth;
    private RectTransform _transform;

    private void Awake()
    {
        barBackground = transform.GetChild(0).GetComponent<Image>();
        if (barBackground == null)
            Debug.LogError("No background found for health bar!");

        barForeground = transform.GetChild(1).GetComponent<Image>();
        if (barForeground == null)
            Debug.LogError("No foreground found for health bar!");

        EventManager.Damage += UpdateHealth;

        _transform = GetComponent<RectTransform>();
        
        _transform.SetParent(GameObject.Find("HealthBarCanvas").transform);
    }



    private void Update()
    {
        if (_target)
        {
            _transform.position = Camera.main.WorldToScreenPoint(_target.transform.position);
            int yOffset = (int)(_target.GetComponent<SpriteRenderer>().sprite.rect.height * _target.transform.localScale.y) / 2 - 20;
            _transform.position = new Vector2(_transform.position.x, _transform.position.y + yOffset);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void SetTarget(GameObject target)
    {
        _target = target;

        if(_target.GetComponent<Base>())
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
                    amount = dealer.GetComponent<Unit>().damage;

                _health -= amount;
                float fill = (float)_health / (float)_maxHealth;
                barForeground.fillAmount = fill;
            }
        }
    }



    private void OnDestroy()
    {
        EventManager.Damage -= UpdateHealth;

    }
}
