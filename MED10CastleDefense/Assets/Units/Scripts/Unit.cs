using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public int health = 10;
    public int maxHealth = 10;
    public int damage = 10;
    public float speed = 3f;
    public float cooldown = 3f;
    public SpriteRenderer sprite;
    public E_UnitTypes unitType;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private string _resourceImagePath = "Sprites/";
    private GameObject _explosion;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        EventManager em = EventManager.Instance;    //Only to make sure no errors happen with the eventmanager


        EventManager.Damage += TakeDamage;

        EventManager.StartListening("LevelComplete", Death);
        EventManager.StartListening("LevelLost", Death);

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
                cooldown = CoinStats.Cooldown;
                speed = CoinStats.Speed;

                if (CoinStats.explosion == null)
                    CoinStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionCoins");
                _explosion = CoinStats.explosion;

                imagePath += "Coin";
                break;
            case E_UnitTypes.PIG:
                maxHealth = PigStats.Health;
                damage = PigStats.Damage;
                cooldown = PigStats.Cooldown;
                speed = PigStats.Speed;

                if (PigStats.explosion == null)
                    PigStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionBills");
                _explosion = PigStats.explosion;

                imagePath += "Piggy";
                break;
            case E_UnitTypes.SAFE:
                maxHealth = SafeStats.Health;
                damage = SafeStats.Damage;
                cooldown = SafeStats.Cooldown;
                speed = SafeStats.Speed;

                if (SafeStats.explosion == null)
                    SafeStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionBills");
                _explosion = SafeStats.explosion;

                imagePath += "Safe";
                break;
            default:
                maxHealth = CoinStats.Health;
                damage = CoinStats.Damage;
                cooldown = CoinStats.Cooldown;
                speed = CoinStats.Speed;

                if (CoinStats.explosion == null)
                    CoinStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionCoins");
                _explosion = CoinStats.explosion;

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


    public void AssignStatValues(string type)
    {
        string imagePath = _resourceImagePath + type;

        switch (type)
        {
            case "Coin":
                maxHealth = CoinStats.Health;
                damage = CoinStats.Damage;
                cooldown = CoinStats.Cooldown;
                speed = CoinStats.Speed;

                if (CoinStats.explosion == null)
                    CoinStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionCoins");
                _explosion = CoinStats.explosion;

                break;
            case "Pig":
                maxHealth = PigStats.Health;
                damage = PigStats.Damage;
                cooldown = PigStats.Cooldown;
                speed = PigStats.Speed;

                if (PigStats.explosion == null)
                    PigStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionBills");
                _explosion = PigStats.explosion;

                break;
            case "Safe":
                maxHealth = SafeStats.Health;
                damage = SafeStats.Damage;
                cooldown = SafeStats.Cooldown;
                speed = SafeStats.Speed;

                if (SafeStats.explosion == null)
                    SafeStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionBills");
                _explosion = SafeStats.explosion;

                break;
            default:
                maxHealth = CoinStats.Health;
                damage = CoinStats.Damage;
                cooldown = CoinStats.Cooldown;
                speed = CoinStats.Speed;

                if (CoinStats.explosion == null)
                    CoinStats.explosion = Resources.Load<GameObject>("Prefabs/ExplosionCoins");
                _explosion = CoinStats.explosion;

                Debug.LogWarning("UnitType returned default for " + transform.name);
                break;
        }

        health = maxHealth;
        transform.name = type;

        if (Resources.Load<Sprite>(imagePath) != null)
        {
            sprite.sprite = Resources.Load<Sprite>(imagePath);
        }
        else
        {
            Debug.LogWarning(transform.name + " could not find image at path: " + imagePath);
            sprite.sprite = Resources.Load<Sprite>(_resourceImagePath + "Coin");
        }

    }



    public void TakeDamage(GameObject dealer, List<GameObject> receiver)
    {
        if (receiver != null && gameObject != null)
        {
            if (receiver.Contains(gameObject))
            {
                if (dealer.tag == "EnemyBase")
                {
                    int amount = dealer.GetComponent<BaseAttack>().damage;
                    health -= amount;
                    //Debug.Log(transform.name + " took " + amount + " damage. Health left: " + health);
                    if (health <= 0)
                    {
                        Death();
                    }
                }
            }
        }

    }


    //public void TakeDamage(int amount)
    //{
    //    health -= amount;
    //    Debug.Log(transform.name + " took " + amount + " damage. Health left: " + health);
    //    if(health <= 0)
    //    {
    //        Death();
    //    }
    //}



    private void Death()
    {
        //TODO: Play animation

        //Spawn explosion
        Instantiate(_explosion, transform.position, Quaternion.Euler(-90, 0, 0));

        EventManager.Instance.UnitDead(gameObject);

        EventManager.Damage -= TakeDamage;

        //Destroy gameObject
        Destroy(gameObject, 0.1f);
    }



    private void Attack(GameObject enemy)
    {
        //TODO: Play explosion animation

        //TODO: Play explosion sound

        //Deal damage to an enemy 
        List<GameObject> hit = new List<GameObject>();
        hit.Add(enemy);
        EventManager.Instance.DealDamage(gameObject, hit);

        //Destroy gameObject after a bit
        sprite.enabled = false;
        Death();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBase")
        {
            Attack(collision.gameObject);
        }
    }
}

public enum E_UnitTypes
{
    COIN,
    PIG,
    SAFE
}