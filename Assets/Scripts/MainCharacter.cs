using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private int _healthBar = 4; //Максимальное количество жизни у ГГ
    [SerializeField] private int _currentHealthBar = 4; //Конкретное количество жизни у ГГ
    [SerializeField] private float _speed = 3f; //Скорость персонажа
    [SerializeField] private float _jumpForce = 5f; //Сила прижка
    private bool isGrounded = true;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run() {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, _speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void FixedUpdate() {
        CheckGround();
    }

    private void Update() {
        if(Input.GetButton("Horizontal"))
            Run();
        if(isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Jump() {
        rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void DashandSlash() {

    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }
}