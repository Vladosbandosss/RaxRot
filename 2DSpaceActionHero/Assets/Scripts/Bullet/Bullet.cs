using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 7f;
    
    [SerializeField] private GameObject impactFX;

    [SerializeField] private int bulletDamage = 5;

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.position += new Vector3(shootSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(impactFX, transform.position, Quaternion.identity);
        
        if (col.CompareTag(TagManager.METEOR_TAG))
        {
            GameObject meteor;
            (meteor = col.gameObject).GetComponent<Meteor>().MeteorImpactFX();
            Destroy(meteor);
        }

        if (col.CompareTag(TagManager.ENEMY_TAG))
        {
            col.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        }

        if (col.CompareTag(TagManager.ENEMY_BULLET_TAG))
        {
            Destroy(col.gameObject);
        }
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
