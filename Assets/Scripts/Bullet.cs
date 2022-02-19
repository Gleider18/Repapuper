using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    private void Start()
    {
        rb.AddForce(transform.forward * 1000);
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Player>())
        {
            Player player = other.gameObject.GetComponentInParent<Player>();
            player.health -= 10;
            // Debug.Log(player.health);
            Destroy(gameObject);
        }
    }
}
