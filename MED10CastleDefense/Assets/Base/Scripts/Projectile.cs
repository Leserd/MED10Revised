using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float _damage;
    private float _splashRadius;
    private E_AttackType _type;



    public void SetOwner(BaseAttack owner)
    {
        _damage = owner.damage;
        _type = owner.attackType;
        _splashRadius = owner.splashRadius;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Unit")
        {
            List<GameObject> targets = new List<GameObject>();
            if(_type == E_AttackType.SINGLE)
            {
                targets.Add(collision.gameObject);
            }
            else if(_type == E_AttackType.SPLASH)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _splashRadius);
                foreach(Collider2D hit in hits)
                {
                    targets.Add(hit.gameObject);
                }
            }

            foreach(GameObject target in targets)
            {
                //TODO: Send damager dealer and damage taker when triggering event
                //EventManager.TriggerEvent("DealDamage");
                print(transform.name + " hit: " + target.name + " for " + _damage + " damage.");
            }
        }
    }

}
