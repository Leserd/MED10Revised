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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //EventManager.DealDamage += TakeDamage;
    }



    public void TakeDamage(GameObject target, int amount)
    {
        if(target == gameObject)
        {
            health -= amount;

            ChangeHealthStateSprite();

            if (health <= 0)
            {
                Death();
            }
        }
    }



    private void Death()
    {
        //TODO: Tell eventManager this base died
        //EventManager.instance.BaseDied(this);

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
