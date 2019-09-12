using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] float health = 500f;
    [SerializeField] float minTimeBetweenSots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.5f;

    float shotCounter;

    void Start()
    {
        ResetShotCounter();
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void ResetShotCounter()
    {
        shotCounter = Random.Range(minTimeBetweenSots, maxTimeBetweenShots);
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }

    private void Fire()
    {
        GameObject laserCopy = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        laserCopy.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }
}
