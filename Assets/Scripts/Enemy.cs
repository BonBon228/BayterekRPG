using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected float distance;
    [SerializeField] protected Vector2 direction;
    protected int currentHealth; //Текущее здоровье врага
    protected GameObject player;
    protected Transform playerTransform;
    protected Rigidbody2D enemyRb;

    protected void Awake()
    {
        SetHealth(health);
    }

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        enemyRb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        PlayerPositionCalculate();
    }

    protected void SetHealth(int value)
    {
        currentHealth = health;
    }

    protected void PlayerPositionCalculate()
    {
        Vector2 heading = transform.position - playerTransform.position;
        distance = heading.magnitude;
        direction = heading / distance;
    }
} 