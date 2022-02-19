using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleWeapon : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private ParticleSystem hitEffect = null;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            enemy.isColliding = true;
            enemy.DealDamage(damage);
            if (hitEffect != null) hitEffect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            enemy.isColliding = false;
        }
    }
}
