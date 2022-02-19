using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    public bool isColliding;
    private Transform player;
    private Bullet bullet;
    [SerializeField] private ParticleSystem destroyEffect;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float health = 30;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform[] muzzles;
    
    [Inject] public void Constuct(Player player, Bullet bullet)
    {
        this.player = player.GetComponentInChildren<Rigidbody>().transform;
        rigidbody.maxDepenetrationVelocity = 0.1f;
        this.bullet = bullet;
    }

    private void Awake()
    {
        transform.LookAt(player.transform.position + new Vector3(0f, 1f));
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        isColliding = false;
        transform.LookAt(player.transform.position + new Vector3(0f, 1f));
        Vector3 direction = transform.forward;
        direction.y = 0;
        if (Vector3.Distance(transform.position, player.position) > 15) rigidbody.AddForce(direction * speed);
        else rigidbody.AddForce(-rigidbody.velocity.normalized * speed);
        if(rigidbody.velocity.magnitude > maxSpeed) rigidbody.velocity = rigidbody. velocity. normalized * maxSpeed;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            int muzzleIndex = new Random().Next(0, 2);
            Vector3 pos = muzzles[muzzleIndex].transform.position;
            Instantiate(bullet, pos, transform.rotation, null);
            yield return new WaitForSeconds(1);
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (destroyEffect != null) destroyEffect.Play();
            transform.position = new Vector3(new Random().Next(-50, 50), 1.8f, new Random().Next(-50, 50));
            health = 30;
        }
    }
}
