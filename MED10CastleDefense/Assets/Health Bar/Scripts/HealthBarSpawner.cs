using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpawner : MonoBehaviour {
    public GameObject healthBarPrefab;
    private HealthBarScript bar;

    private void Start()
    {
        if(healthBarPrefab == null)
        {
            healthBarPrefab = Resources.Load<GameObject>("Prefabs/HealthBar");
        }
        
        StartCoroutine(SetHealth());

    }

    IEnumerator SetHealth()
    {
        yield return new WaitForSeconds(0.1f);
        bar = Instantiate(healthBarPrefab).GetComponent<HealthBarScript>();
        bar.SetTarget(gameObject);
        Destroy(this);
    }
}
