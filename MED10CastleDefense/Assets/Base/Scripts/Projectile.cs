using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float _damage;
    private float _splashRadius;
    private E_AttackType _type;
    private BaseAttack _owner;
    private Transform _target;
    private Coroutine _moveCoroutine;
    private float _projectileSpeed = 8f;

    public void Init(BaseAttack owner, Transform target)
    {
        _owner = owner;
        _target = target;
        _damage = owner.damage;
        _type = owner.attackType;
        _splashRadius = owner.splashRadius;
        _projectileSpeed = owner.projectileSpeed;

        _moveCoroutine = StartCoroutine(MoveToTarget());
    }



    private IEnumerator MoveToTarget()
    {
        while (true)
        {
            if(_target != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, Time.deltaTime * _projectileSpeed);
            }
            else
            {
                _moveCoroutine = null;
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == _target)
        {
            List<GameObject> targets = new List<GameObject>();
            if (_type == E_AttackType.SINGLE)
            {
                targets.Add(collision.gameObject);
            }
            else if (_type == E_AttackType.SPLASH)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _splashRadius);
                foreach (Collider2D hit in hits)
                {
                    if (hit.tag == "PlayerBase" || hit.tag == "Unit")
                        targets.Add(hit.gameObject);
                }
            }

            //foreach(GameObject target in targets)
            //{
            //    //TODO: Send damager dealer and damage taker when triggering event
            //    //EventManager.TriggerEvent("DealDamage");
            //    //print(transform.name + " hit: " + target.name + " for " + _damage + " damage.");

            //}
            EventManager.Instance.DealDamage(_owner.gameObject, targets);

            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        //TODO: Stop listening to events
    }

}
