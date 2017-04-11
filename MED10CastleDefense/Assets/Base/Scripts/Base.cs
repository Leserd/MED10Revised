using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    public string baseName;
    public int health;
    public int maxHealth;
    public Sprite[] baseHealthStates;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _particle;
    private int _curSpriteIndex;

    private void Start()
    {
        EventManager em = EventManager.Instance;    //Only to make sure no errors happen with the eventmanager

        _curSpriteIndex = baseHealthStates.Length - 1;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _particle = transform.GetChild(2).GetComponent<ParticleSystem>();
        if (_particle == null)
            Debug.LogWarning("No dust particle system was found in " + transform.name + "'s GetChild(2)");

        EventManager.Damage += TakeDamage;

        SetMaxHealth();
        SetName();
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
                amount = Mathf.CeilToInt((float)maxHealth / 100f * (float)amount);
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
            GetComponent<BaseAttack>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            //TODO: Instantiate white flag and play surrender animation
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }
        else if(gameObject.tag == "PlayerBase")
        {
            EventManager.TriggerEvent("LevelLost");
            transform.GetChild(1).GetComponent<Rigidbody2D>().simulated = true;
            //TODO: Animate the king falling
        }

        EventManager.Damage -= TakeDamage;
    }



    //Change sprite of base based on health and number of states
    private void ChangeHealthStateSprite()
    {
        if(baseHealthStates.Length > 0)
        {

            int spriteIndex = Mathf.CeilToInt((float)health / (float)maxHealth * (baseHealthStates.Length-1));
            if(spriteIndex != _curSpriteIndex)
            {
                _curSpriteIndex = spriteIndex;
                if (spriteIndex < 0)
                {
                    spriteIndex = 0;
                    _curSpriteIndex = 0;
                }

                if (_particle != null)
                    _particle.Play();
            }
            
            _spriteRenderer.sprite = baseHealthStates[spriteIndex];
        }
    }



    public void SetName()
    {
        if(gameObject.tag == "EnemyBase")
        {
            baseName = PretendData.Instance.Data[StateManager.Instance.SelectedLevel - 1].BSDataName;
            transform.name = baseName;
        }
        else if(gameObject.tag == "PlayerBase")
        {
            baseName = "You";
            transform.name = baseName;
        }
    }



    public void SetMaxHealth()
    {
        if (gameObject.tag == "EnemyBase")
        {
            maxHealth = int.Parse(PretendData.Instance.Data[StateManager.Instance.SelectedLevel - 1].BSDataAmount);
            health = maxHealth;
        }
        else if (gameObject.tag == "PlayerBase")
        {
            maxHealth = 10 + 6* StateManager.Instance.SelectedLevel - 1;
            health = maxHealth;
        }
    }
}
