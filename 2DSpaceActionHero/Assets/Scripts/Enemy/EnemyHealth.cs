using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
   [SerializeField] private int minHealth = 1, maxHealth = 4;
   private int _currentHealth;

   [SerializeField] private int damageOfCollisionToPlayer = 1;
   [SerializeField] private int damageToCollision = 10;

   [SerializeField] private GameObject[] enemyImpactFX;
   private int _indexImpactFXToSpawn;

   [SerializeField] private int minScoreForDestroyEnemy = 2, maxScoreForDestroyEnemy = 4;
   private int _scoreForDestroyEnemy;
   
   [SerializeField] private GameObject powerPickUpObject;

   private void Start()
   {
      SetValues();
   }

   private void SetValues()
   {
      _currentHealth = Random.Range(minHealth, maxHealth);
      
      _indexImpactFXToSpawn = Random.Range(0, enemyImpactFX.Length);

      _scoreForDestroyEnemy = Random.Range(minScoreForDestroyEnemy, maxScoreForDestroyEnemy + 1);
   }
   
   private void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
      {
         PlayerHealth.Instance.HurtPlayer(damageOfCollisionToPlayer);
         
         TakeDamage(damageToCollision);
      }
   }

   public void TakeDamage(int damage)
   {
      _currentHealth -= damage;
      
      if (_currentHealth<=0)
      {
         if (Random.Range(0,10)==9)
         {
            Instantiate(powerPickUpObject, transform.position, Quaternion.identity);
         }
         
         UIManager.Instance.UpdateScore(_scoreForDestroyEnemy);
         MusicManager.Instance.PlayEnemyDeadSFX();
         
         Instantiate(enemyImpactFX[_indexImpactFXToSpawn], transform.position, Quaternion.identity);
         Destroy(gameObject);
         
      }
   }
}
