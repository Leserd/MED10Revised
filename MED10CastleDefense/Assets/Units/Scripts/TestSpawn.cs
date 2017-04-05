using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour {

    public Transform spawnPos;
    public GameObject unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnUnit(E_UnitTypes.COIN);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnUnit(E_UnitTypes.PIG);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnUnit(E_UnitTypes.SAFE);
        }
    }


    private void SpawnUnit(E_UnitTypes type)
    {
        Unit newUnit = Instantiate(unit, spawnPos.position, Quaternion.identity).GetComponent<Unit>();
        newUnit.AssignStatValues(type);
        EventManager.Instance.Spawn(newUnit.gameObject);
    }
}
