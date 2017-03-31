using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int health = 10;
    public int maxHealth = 10;
    public int damage = 10;
    public float speed = 3f;
    public SpriteRenderer sprite;
    public E_UnitTypes unitType;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private string _resourceImagePath = "Sprites/";


    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }



    private void Update()
    {
        _rb.MovePosition(transform.position + Vector3.right * Time.deltaTime * speed);
    }



    public void AssignStatValues(E_UnitTypes type)
    {
        unitType = type;
        string imagePath = _resourceImagePath;

        switch (type)
        {
            case E_UnitTypes.COIN:
                maxHealth = CoinStats.Health;
                damage = CoinStats.Damage;
                speed = CoinStats.Speed;
                imagePath += "Coin";
                break;
            case E_UnitTypes.PIGGY:
                maxHealth = PiggyStats.Health;
                damage = PiggyStats.Damage;
                speed = PiggyStats.Speed;
                imagePath += "Piggy";
                break;
            case E_UnitTypes.SAFE:
                maxHealth = SafeStats.Health;
                damage = SafeStats.Damage;
                speed = SafeStats.Speed;
                imagePath += "Safe";
                break;
            default:
                maxHealth = CoinStats.Health;
                damage = CoinStats.Damage;
                speed = CoinStats.Speed;
                imagePath += "Coin";
                Debug.LogWarning("UnitType returned default for " + transform.name);
                break;
        }

        health = maxHealth;
        transform.name = type.ToString();

        if (Resources.Load<Sprite>(imagePath) != null)
        {
            sprite.sprite = Resources.Load<Sprite>(imagePath);
        }
        else
        {
            Debug.LogWarning(transform.name + " could not find image at path: " + imagePath);
            sprite.sprite = Resources.Load<Sprite>(_resourceImagePath + "Default");
        }

    }



    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Death();
        }
    }



    private void Death()
    {
        //TODO: Play animation

        //Destroy gameObject
        Destroy(gameObject);
    }



    private void Attack(GameObject enemy)
    {
        //TODO: Play explosion animation

        //TODO: Play explosion sound

        //TODO: Deal damage to an enemy 
        Debug.Log("Explosion hit " + enemy.name);


        Destroy(gameObject);      
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBase")
        {
            Attack(collision.gameObject);
        }
    }
}

public enum E_UnitTypes
{
    COIN,
    PIGGY,
    SAFE
}