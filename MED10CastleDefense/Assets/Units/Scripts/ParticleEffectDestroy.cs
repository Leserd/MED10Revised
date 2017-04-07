using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectDestroy : MonoBehaviour {

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
