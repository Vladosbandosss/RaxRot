using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBatle : MonoBehaviour
{
    public static BossBatle Instance;
    
    [SerializeField] private GameObject boss;
    
    [SerializeField] private GameObject swingRotateObj;
    
    [SerializeField] private float minXToSpawn = 22, maxXToSpawn = 37;
    private float _xToSpawn;
    
    [SerializeField] private float posYtoSpawn = 7f;
    private Vector3 _spawnPos;

    [SerializeField] private float minTimeToSpawnRotateObg = 1f, maxTimeToSpawnRotateObg = 2f;
    private float _timeToSpawnRotateObg;

    [SerializeField] private int countSpawnedRotateObgToWin=10;
    [HideInInspector] public int spawnedRotateObg;

    private bool _isStartingFight;
    private bool _isFighting = true;

    [SerializeField] private GameObject bossDeadFx;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG) && !_isStartingFight)
        {
            _isStartingFight = true;
            
            boss.SetActive(true);
            StartFight();
        }
    }

    private void StartFight()
    {
        StartCoroutine(nameof(_StartFightCo));
    }

    private IEnumerator _StartFightCo()
    {
        while (_isFighting)
        {
            _timeToSpawnRotateObg = Random.Range(minTimeToSpawnRotateObg, maxTimeToSpawnRotateObg);
            yield return new WaitForSeconds(_timeToSpawnRotateObg);

            _xToSpawn = Random.Range(minXToSpawn, maxXToSpawn);
            _spawnPos = new Vector3(_xToSpawn, posYtoSpawn);

            Instantiate(swingRotateObj, _spawnPos, Quaternion.identity);

            if (spawnedRotateObg==countSpawnedRotateObgToWin)
            {
                _isFighting = false;
            }
        }
        yield return new WaitForSeconds(5f);
        
        BossDead();

        yield return new WaitForSeconds(3f);
        
        GameManager.Instance.WinGame();
    }

    private void BossDead()
    {
        Instantiate(bossDeadFx, boss.transform.position, Quaternion.identity);
        boss.SetActive(false);
    }
}
