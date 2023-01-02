using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    private enum Power
    {
        Shield,
        Health,
        FastShoot
    }
    private Power _power;
    private int _indexToChosePower;

    [SerializeField] private GameObject powerFX;

    private void Start()
    {
       SetValues();
    }

    private void SetValues()
    {
        _indexToChosePower = Random.Range(0, 3);
        
        switch (_indexToChosePower)
        {
            case 0:
                _power = Power.Health;
                break;
            
            case 1:
                _power = Power.Shield;
                break;
            
            case 2:
                _power = Power.FastShoot;
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            Instantiate(powerFX, transform.position, Quaternion.identity);
            MusicManager.Instance.PlayPowerPickUpSFX();
            
            switch (_power)
            {
                case Power.Health:
                    
                    PlayerHealth.Instance.IncreaseHealth();
                    
                    break;
                case Power.Shield:
                    
                    PlayerHealth.Instance.ActivateShield();
                    
                    break;
                
                case Power.FastShoot:
                    
                    PlayerHealth.Instance.ActivatePowerShoot();
                    
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
