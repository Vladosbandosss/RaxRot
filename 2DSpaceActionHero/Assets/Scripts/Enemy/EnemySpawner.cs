using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesShips;
    private int _enemyShipIndex;
    
    [SerializeField] private GameObject[] meteors;
    private int _meteorIndex;
    
    [SerializeField] private Transform[] pointsToSPawn;
    private int _pointIndex;

    [SerializeField] private float minTimeToSpawn = 2f, maxTimeToSpawn = 4f;
    private float _timeToSpawn;

    private GameObject _spawnedObject;

    private int _spawnedEnemiesCount;

    [SerializeField] private float increaseSpeedOfInstantiateEnemy = 0.1f;
    [SerializeField] private float maxSpawnTimeAfterAll = 1.5f;
    
    private void Start()
    { 
        SpawnObjects();
    }

    private IEnumerator _SpawnObjects()
    {
        _timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
        yield return new WaitForSeconds(_timeToSpawn);

        _pointIndex = Random.Range(0, pointsToSPawn.Length);
        _spawnedObject = null;

        if (Random.Range(0,10)>=3)//ship
        {
            _enemyShipIndex = Random.Range(0, enemiesShips.Length);
            _spawnedObject = Instantiate(enemiesShips[_enemyShipIndex]);

            _spawnedEnemiesCount++;
        }
        else//meteor
        {
            _meteorIndex = Random.Range(0, meteors.Length);
            _spawnedObject = Instantiate(meteors[_meteorIndex]);
        }

        _spawnedObject.transform.position = pointsToSPawn[_pointIndex].transform.position;

        if(_spawnedEnemiesCount>=3)
        {
            _spawnedEnemiesCount = 0;
            
            if (maxTimeToSpawn>maxSpawnTimeAfterAll)
            {
                minTimeToSpawn -= increaseSpeedOfInstantiateEnemy;
                maxTimeToSpawn -= increaseSpeedOfInstantiateEnemy;
            }
        }

        StartCoroutine(nameof(_SpawnObjects));
    }

    private void SpawnObjects()
    {
        StartCoroutine(nameof(_SpawnObjects));
    }

}
