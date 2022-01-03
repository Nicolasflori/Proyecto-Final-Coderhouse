using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject enemyPrefab;

    void Spawn()
    {
        Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

}
