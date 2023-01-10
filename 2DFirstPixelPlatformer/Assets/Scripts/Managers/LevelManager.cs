using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private PlayerController _player;
    [SerializeField] private float timeToRespawn=2f;
    [SerializeField] private float timeBetweenStartRespawn = 0.5f;

    [SerializeField] private Image fadeImage;
    [HideInInspector]public bool isFadingBlack,isFadingFromBlack;
    [SerializeField] private float fadeSpeed = 1.5f;

    [SerializeField] private GameObject playerDeadFX;

    private int _coinCount;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        MakeFadeOrNot();
    }

    public void RespawnPlayer()
    {
       Respawn();
    }

    private void Respawn()
    {
        MakeDeadPlayerFX();
        StartCoroutine(_MakeFadeOrNotCo());
        StartCoroutine(_RespawnPlayerCo());
    }

    private IEnumerator _RespawnPlayerCo()
    {
        SoundManager.Instance.PlayPlayerHurtSfx();
        _player.MakeKnock();
        _player.isPlayerDead = true;
        yield return new WaitForSeconds(timeBetweenStartRespawn);
        
       _player.gameObject.SetActive(false);
       
       yield return new WaitForSeconds(timeToRespawn);
       
       _player.transform.position = _player.RespawnPosition;
       _player.gameObject.SetActive(true);
       _player.isPlayerDead = false;
    }

    private IEnumerator _MakeFadeOrNotCo()
    {
        yield return new WaitForSeconds(timeBetweenStartRespawn);
        
        isFadingBlack = true;
        isFadingFromBlack = false;
        
        yield return new WaitForSeconds(timeToRespawn);

        isFadingBlack = false;
        isFadingFromBlack = true;
    }

    private void MakeFadeOrNot()
    {
        if (isFadingBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 1f, fadeSpeed * Time.deltaTime));
        }

        if (isFadingFromBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
    }

    private void MakeDeadPlayerFX()
    {
        Instantiate(playerDeadFX, _player.transform.position, Quaternion.identity);
    }

    public void AddCoin()
    {
        _coinCount++;
    }
    
}
