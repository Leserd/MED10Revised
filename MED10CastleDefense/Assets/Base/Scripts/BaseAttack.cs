using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour {

    public int damage = 2;
    public float attackSpeed = 1f;
    public float splashRadius = 1f;
    public float attackRange;
    public float projectileSpeed = 8f;
    public GameObject projectilePrefab;
    public E_AttackType attackType;
    public bool canAttack = false;  //
    private List<GameObject> _availableTargets = new List<GameObject>();
    private GameObject _target;
    private Coroutine _attackCoroutine;



    private void OnEnable()
    {
        //EventManager.instance.StartListening("SpawnUnit", AddTarget);
        EventManager.SpawnUnit += AddTarget;
        EventManager.UnitDies += RemoveTarget;

        _availableTargets.Add(GameObject.FindGameObjectWithTag("PlayerBase"));
    }


    private void OnDisable()
    {
        //EventManager.instance.StartListening("SpawnUnit", AddTarget);
        EventManager.SpawnUnit -= AddTarget;
        EventManager.UnitDies -= RemoveTarget;
    }



    public void AddTarget(GameObject newTarget)
    {
        _availableTargets.Add(newTarget);
        canAttack = true;
        if(_attackCoroutine == null)
        {
            StartAttacking();
        }
    }



    public void RemoveTarget(GameObject target)
    {
        _availableTargets.Remove(target);
        
        if(_availableTargets.Count <= 0)
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                
            }
            canAttack = false;
        }
        
    }



    public void StartAttacking()
    {
        if(_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
    }



    private IEnumerator Attack()
    {
        while (canAttack && _availableTargets.Count > 0)
        {
            _target = GetClosestTarget();
            if (_target == null)
            {
                StopCoroutine(_attackCoroutine);
                break;
            }
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Init(this, _target.transform);
            yield return new WaitForSeconds(attackSpeed);
        }
    }



    private GameObject GetClosestTarget()
    {
        GameObject closestTarget = _availableTargets[0];

        foreach(GameObject go in _availableTargets)
        {
            if (closestTarget == null || go == null)
                continue;
            else if(Vector2.Distance(go.transform.position, transform.position) < Vector2.Distance(closestTarget.transform.position, transform.position))
            {
                closestTarget = go;
            }
        }

        return closestTarget;
    }

}



public enum E_AttackType
{
    SINGLE,
    SPLASH
}