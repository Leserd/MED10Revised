using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpawner : MonoBehaviour {

    public static GameObject healthBarPrefab;

    private void Start()
    {
        if(healthBarPrefab == null)
        {
            healthBarPrefab = Resources.Load<GameObject>("Prefabs/HealthBar");
        }

        HealthBarScript bar = Instantiate(healthBarPrefab).GetComponent<HealthBarScript>();
        bar.SetTarget(gameObject);
        Destroy(this);
    }
}
