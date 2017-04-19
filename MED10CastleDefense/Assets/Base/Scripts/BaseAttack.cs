using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseAttack : MonoBehaviour {

    public int damage = 1;
    public int upgDamage = 1;
    public int maxDamage = 5;
    public float attackSpeed = 2f;
    public float upgAttackSpeed = 0.15f;
    public float maxAttackSpeed = 0.65f;
    public float splashRadius = 1f;
    public float attackRange;
    public float projectileSpeed = 12f;
    public GameObject projectilePrefab;
    public E_AttackType attackType;
    public bool canAttack = false;  
    private List<GameObject> _availableTargets = new List<GameObject>();
    private GameObject _target;
    private Coroutine _attackCoroutine;
    private Transform _gun;
    private Animator _gunAnim;
    public GameObject fireParticleEffect;
    private ParticleSystem _particle;
    public GameObject damageStatsPrefab;
    private Transform damageStatsCurrent;

    private void Start()
    {
        EventManager em = EventManager.Instance;    //Only to make sure no errors happen with the eventmanager


        //EventManager.instance.StartListening("SpawnUnit", AddTarget);
        EventManager.SpawnUnit += AddTarget;
        EventManager.UnitDies += RemoveTarget;
        EventManager.StartListening("LevelComplete", StopAttacking);
        EventManager.StartListening("LevelLost", StopAttacking);
        EventManager.StartListening("SpawnFirstUnit", StartAttacking);


        _gun = transform.GetChild(0);
        _gunAnim = _gun.GetComponent<Animator>();
        _particle = _gun.GetChild(0).GetComponent<ParticleSystem>();
        _availableTargets.Add(GameObject.FindGameObjectWithTag("PlayerBase"));

        UpdateStats();
    }



    public void UpdateStats()
    {
        int lvl = StateManager.Instance.SelectedLevel - 1;
        damage = 1 + lvl * upgDamage;
        if (damage > maxDamage)
            damage = maxDamage;
        if(attackSpeed - lvl*upgAttackSpeed > maxAttackSpeed)
        {
            attackSpeed -= lvl * upgAttackSpeed;
        }
        DisplayAttackStats();
    }


    public void DisplayAttackStats()
    {
        if(damageStatsCurrent == null)
        {
            damageStatsCurrent = Instantiate(damageStatsPrefab, GameObject.Find("HealthBarCanvas").transform, false).transform;

        }

        damageStatsCurrent.GetChild(1).GetComponent<Text>().text = damage.ToString();
        damageStatsCurrent.GetChild(3).GetComponent<Text>().text = attackSpeed.ToString("F2");

        damageStatsCurrent.position = Camera.main.WorldToScreenPoint(transform.position);
        int yOffset = (int)(GetComponent<SpriteRenderer>().sprite.rect.height * transform.localScale.y) / 2 + 150;
        //int xOffset = (int)(GetComponent<SpriteRenderer>().sprite.rect.width * transform.localScale.x) / 2 + 30;
        damageStatsCurrent.position = new Vector2(damageStatsCurrent.position.x, damageStatsCurrent.position.y + yOffset);
        //damageStatsCurrent.position = new Vector2(damageStatsCurrent.position.x + xOffset, damageStatsCurrent.position.y);
    }


    public void RemoveAttackStats()
    {
        if(damageStatsCurrent != null)
        {
            Destroy(damageStatsCurrent.gameObject);
            damageStatsCurrent = null;
        }
    }


    private void OnDisable()
    {
        //EventManager.instance.StartListening("SpawnUnit", AddTarget);
        EventManager.SpawnUnit -= AddTarget;
        EventManager.UnitDies -= RemoveTarget;
    }



    private void OnDestroy()
    {
        StopAttacking();
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
            StopAttacking();
        }
        
    }



    public void StartAttacking()
    {
        if(_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
    }



    public void StopAttacking()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = null;
        canAttack = false;
        RemoveAttackStats();
    }



    private IEnumerator Attack()
    {
        while (canAttack && _availableTargets.Count > 0)
        {
            yield return new WaitForSeconds(attackSpeed);
            _target = GetClosestTarget();
            if (_target == null)
            {
                StopCoroutine(_attackCoroutine);
                break;
            }
            GameObject projectile = Instantiate(projectilePrefab, _gun.position, Quaternion.identity);

            _gun.rotation = Quaternion.LookRotation(Vector3.forward, _target.transform.position - _gun.transform.position);
            _gunAnim.CrossFade("CannonRecoil", 0f);

            if (_particle != null)
                _particle.Play();
                //Instantiate(fireParticleEffect, _gun.transform.position + _gun.up, _gun.rotation);

            projectile.GetComponent<Projectile>().Init(this, _target.transform);
            
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