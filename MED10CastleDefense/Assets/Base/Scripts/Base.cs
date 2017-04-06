using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    public string baseName;
    public int health;
    public int maxHealth;
    public Sprite[] baseHealthStates;
    private SpriteRenderer _spriteRenderer;



    private void Awake()
    {
        EventManager em = EventManager.Instance;    //Only to make sure no errors happen with the eventmanager


        _spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.Damage += TakeDamage;
    }



    public void TakeDamage(GameObject dealer, List<GameObject> receiver)
    {
        if (receiver.Contains(gameObject))
        {
            int amount = 0;
            if(dealer.tag == "EnemyBase")
            {
                amount = dealer.GetComponent<BaseAttack>().damage;
            }
            else if(dealer.tag == "Unit")
            {
                amount = dealer.GetComponent<Unit>().damage;
            }


            health -= amount;

            ChangeHealthStateSprite();

            if (health <= 0)
            {
                Death();
            }
        }
    }



    //public void TakeDamage(GameObject target, int amount)
    //{
    //    if(target == gameObject)
    //    {
    //        health -= amount;

    //        ChangeHealthStateSprite();

    //        if (health <= 0)
    //        {
    //            Death();
    //        }
    //    }
    //}



    private void Death()
    {
        //TODO: Tell eventManager this base died
        if(gameObject.tag == "EnemyBase")
        {
            EventManager.TriggerEvent("LevelComplete");
        }
        else if(gameObject.tag == "PlayerBase")
        {
            EventManager.TriggerEvent("LevelLost");

        }

        EventManager.Damage -= TakeDamage;

        //Destroy castle (TODO: Instead instantiate a fire on the base to show it is destroyed, while Victory screen is displayed)
        Destroy(gameObject);


    }



    //Change sprite of base based on health and number of states
    private void ChangeHealthStateSprite()
    {
        if(baseHealthStates.Length > 0)
        {
            int spriteIndex = Mathf.CeilToInt((float)health / (float)maxHealth * (baseHealthStates.Length-1));
            _spriteRenderer.sprite = baseHealthStates[spriteIndex];
        }
    }



    //TODO: Uncomment this when Bill has been implemented to the game
    //public void AssignBill(Bill bill)
    //{
    //    _name = bill.name;
    //    transform.name = _name;

    //    _maxHealth = bill.price;
    //    _health = maxHealth;
    //}



    public void SetName(string name)
    {
        baseName = name;
        transform.name = baseName;
    }



    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        health = amount;
    }

}
