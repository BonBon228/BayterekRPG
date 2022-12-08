using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected float distance;
    protected Vector2 direction;
    protected SpriteRenderer _spriteRenderer;
    protected int currentHealth; //Текущее здоровье врага
    protected GameObject player;
    protected Transform playerTransform;
    protected Rigidbody2D enemyRb;
    protected Animator _enemyAnim;

    protected bool _isAttacked = false;

    protected bool _isFacingRight = false;

    private GameObject attackZone;

    public static Enemy Instance { get; set; }
    [SerializeField] private GameObject _dieExplosion;

    protected void Awake()
    {
        SetHealth(health);
    }

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        enemyRb = this.gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        _enemyAnim = this.gameObject.GetComponentInChildren<Animator>();
        attackZone = GameObject.FindGameObjectWithTag("AttackZone");
        if(Instance == null)
        {
            Instance = this;
        }
    }

    protected void Update()
    {
        PlayerPositionCalculate();
    }

    public void GetDamage() 
    {
        if(!_isAttacked)
        {
            StartCoroutine(Attacked());
            StartCoroutine(ChangeSpriteColor());
        }
    }

    protected void Flip() {
        if(_isFacingRight && transform.position.x > playerTransform.position.x || !_isFacingRight && playerTransform.position.x > transform.position.x) {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Attacked()
    {
        _isAttacked = true;
        health--;
        Debug.Log(health);
        if(health < 1) 
            Die();
        yield return new WaitForSeconds(1f);
        _isAttacked = false;
    }

    private IEnumerator ChangeSpriteColor() 
    {
        _spriteRenderer.color = new Color(0.9f, 0.4f, 0.4f, 1);
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    protected void Die()
    {
        GameObject particleGO = Instantiate(_dieExplosion, transform.position, Quaternion.identity);
        particleGO.SetActive(true);
        Destroy(this.gameObject);
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