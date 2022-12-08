using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private int _healthBar = 4; //Максимальное количество жизни у ГГ
    [SerializeField] private int _currentHealthBar = 4; //Конкретное количество жизни у ГГ
    [SerializeField] private float _speed = 3f; //Скорость персонажа
    [SerializeField] private float _jumpForce = 5f; //Сила прижка

    private bool _isInvincible = false;

    public bool _isAttacking = false;

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

    public static MainCharacter Instance { get; set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Run() {
        if(_isGrounded && !_isAttacking) State = States.run;

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

        if(_isGrounded && !_isAttacking) State = States.idle;

        if(Input.GetButton("Horizontal"))
            Run();

        if(_isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if(Input.GetKeyDown(KeyCode.S) && _canDash)
            StartCoroutine(Dash());
        if(Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Attacking());

        Flip();
    }

    private void Jump() {
        rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f, 3);
        _isGrounded = collider.Length > 1;

        if(!_isGrounded && !_isAttacking) State = States.jump;
    }

    private void Flip() {
        if(_isFacingRight && horizontal < 0f || !_isFacingRight && horizontal > 0f) {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void GetDamage() {
        Debug.Log(_currentHealthBar);
        if(!_isInvincible) {
            StartCoroutine(Attacked());
        }
    }

    private IEnumerator Attacked() {
        _isInvincible = true;
        if(_currentHealthBar == 1) {
            _isDashing = true;
            State = States.death;
            yield return new WaitForSeconds(1.5f);
            Time.timeScale = 0;
        }
        _currentHealthBar -= 1;
        sprite.color = new Color(1,1,1,(float)0.5);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1,1,1,(float)0.5);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1,1,1,(float)0.5);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1,1,1,1);
        _isInvincible = false;
    }

    private IEnumerator Attacking() {
        _isAttacking = true;
        State = States.attack;
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
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
        attack,
        dash,
        death
    }

    private IEnumerator Dash() {
        _canDash = false;
        _isDashing = true;
        _isInvincible = true;
        _isAttacking = true;
        State = States.dash;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;
        _isInvincible = false;
        _isAttacking = false;
        rb.velocity = Vector2.zero;
        State = States.idle;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}