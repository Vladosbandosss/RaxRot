using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    private Animator _anim;
    private PlayerController _playerController;
    
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth=5;

    [SerializeField] private GameObject playerHurtFX;
    [SerializeField] private GameObject deadFX;

    [SerializeField] private GameObject shield;
    private bool _shieldIsActive;
    [SerializeField] private float timeForShieldWork = 20f;

    [SerializeField] private GameObject shoot;
    [SerializeField] private float timeForNewPowerBetweenShoot = 0.15f;
    [SerializeField] private float timeForPowerWork = 20f;

    [SerializeField] private float warningTimeForStopPower = 3f;
    
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        currentHealth = maxHealth;
        
        UIManager.Instance.SetValuesForSliderHealth(maxHealth,currentHealth);
        
        shield.SetActive(false);
        shoot.SetActive(false);
    }

    public void HurtPlayer(int damage)
    {
        MusicManager.Instance.InjurePlayer();
        
        if (!_shieldIsActive)
        {
            currentHealth -= damage;
            
            GameManager.Instance.InjurePlayer();
            
            Instantiate(playerHurtFX, transform.position, Quaternion.identity);

            if (currentHealth<=0)
            {
                currentHealth = 0;
            
                Instantiate(deadFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                
                GameManager.Instance.GameOver();
            }
            
            UIManager.Instance.UpdateLifeFromSlider(currentHealth);
        }
    }

    public void ActivatePowerShoot()
    {
        StartCoroutine(nameof(_ActivatePowerShootCo));
    }

    private IEnumerator _ActivatePowerShootCo()
    {
        ActivatePower();
        
        yield return new WaitForSeconds(timeForPowerWork);
        
        _anim.Play(TagManager.PLAYER_SHOT_STOP_ANIM);
        
        yield return new WaitForSeconds(warningTimeForStopPower);
       
        DeactivatePower();
    }

    private void ActivatePower()
    {
        _playerController.SetNewTimeBetweenShoot(timeForNewPowerBetweenShoot);
        _anim.Play(TagManager.PLAYER_SHOOT_ANIM);
        shoot.SetActive(true);
    }
    
    private void DeactivatePower()
    {
        shoot.SetActive(false);
        _playerController.BackToNormalTimeBetweenShoot();
        _anim.Play(TagManager.PLAYER_IDLE_ANIM);
    }

    public void ActivateShield()
    {
        StartCoroutine(nameof(_ActivateShieldCo));
    }

    private IEnumerator _ActivateShieldCo()
    {
        shield.SetActive(true);
        _shieldIsActive = true;
        
        _anim.Play(TagManager.PLAYER_SHIELD_ANIM);
        
        yield return new WaitForSeconds(timeForShieldWork);
        
        _anim.Play(TagManager.PLAYER_SHIELD_STOP_ANIM);
        
        yield return new WaitForSeconds(warningTimeForStopPower);
        
        DeactivateShield();
    }
    
    private void DeactivateShield()
    {
        shield.SetActive(false);
        _shieldIsActive = false;
        _anim.Play(TagManager.PLAYER_IDLE_ANIM);
    }

    public void IncreaseHealth()
    {
        currentHealth++;
        UIManager.Instance.UpdateLifeFromSlider(currentHealth);
    }
}
