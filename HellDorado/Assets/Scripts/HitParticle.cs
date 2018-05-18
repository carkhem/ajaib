using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {

    public GameObject hitParticlePrefab;
    private GameObject particle;
    private Transform enemy;
    private Vector3 spawn;

    private void Start()
    {
        spawn = new Vector3(0, 1, 0);
    }

    public void SpawnHitParticle(Transform t)
    {
        enemy = t;
        particle = Instantiate(hitParticlePrefab, (enemy.position + spawn), enemy.rotation);
    }

}
