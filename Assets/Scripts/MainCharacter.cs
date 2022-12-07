using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private int _healthBar = 4; //Максимальное количество жизни у ГГ
    [SerializeField] private int _currentHealthBar = 4; //Конкретное количество жизни у ГГ
    [SerializeField] private float _speed = 3f; //Скорость персонажа
    [SerializeField] private float _jumpForce = 5f; //Сила прижка

    private bool _isGrounded = true;

    private float horizontal;
    private bool _isFacingRight = true;

    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField]private float _dashingPower = 24f;
    [SerializeField]private float _dashingTime = 0.2f;
    [SerializeField]private float _dashingCooldown = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField]private TrailRenderer tr;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run() {
        if(_isGrounded) State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, _speed * Time.deltaTime);
    }

    private void FixedUpdate() {
        if(_isDashing) {
            return;
        }

        CheckGround();
    }

    private void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(_isDashing) {
            return;
        }

        if(_isGrounded) State = States.idle;

        if(Input.GetButton("Horizontal"))
            Run();
        if(_isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if(Input.GetKeyDown(KeyCode.D) && _canDash)
            StartCoroutine(Dash());

        Flip();
    }

    private void Jump() {
        rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        _isGrounded = collider.Length > 1;

        if(!_isGrounded) State = States.jump;
    }

    private void Flip() {
        if(_isFacingRight && horizontal < 0f || !_isFacingRight && horizontal > 0f) {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private States State {
        get {return (States)anim.GetInteger("myState");}
        set {anim.SetInteger("myState", (int)value);}
    }

    private enum States {
        idle_normal,
        idle,
        jump,
        run,
        dash
    }

    private IEnumerator Dash() {
        _canDash = false;
        _isDashing = true;
        State = States.dash;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;
        rb.velocity = Vector2.zero;
        State = States.idle;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}