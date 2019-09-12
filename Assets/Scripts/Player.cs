﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] GameObject laser;
    [SerializeField] float laserSpeed = 5f;
    [SerializeField] float laserFiringPeriod = 0.1f;
    [Header("Player")]
    [SerializeField] int health = 200;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - padding;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laserCopy = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            laserCopy.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (null != damageDealer)
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
