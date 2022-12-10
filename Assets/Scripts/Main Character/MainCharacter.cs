using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private int _healthBar; //Максимальное количество жизни у ГГ
    [SerializeField] private int _currentHealthBar; //Конкретное количество жизни у ГГ
    [SerializeField] private float _speed = 3f; //Скорость персонажа
    [SerializeField] private float _jumpForce = 5f; //Сила прижка
    [SerializeField] private Image[] hearts;
    [SerializeField] private Image HeadHB;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    private bool _isInvincible = false;

    public bool _isAttacking = false;

    private bool _isGrounded = true;

    private float horizontal;
    private bool _isFacingRight = true;

    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField]private float _dashingPower = 24f;
    [SerializeField]private float _dashingTime = 0.5f;
    [SerializeField]private float _dashingCooldown = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField]private TrailRenderer tr;
    [SerializeField]private LayerMask Ground;
    [SerializeField]private BoxCollider2D col;

    public static MainCharacter Instance { get; set; }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col != null) {
            if(col.gameObject.tag == "CopperClaw") {
                MainCharacter.Instance.GetDamage();
            }
        }
    }

    private void Awake() {
        _healthBar = 4;
        _currentHealthBar = _healthBar;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col = GetComponentInChildren<BoxCollider2D>();
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Run() {
        if(_isGrounded && !_isAttacking) {
            State = States.run;
        }

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
        if(_currentHealthBar > _healthBar)
            _currentHealthBar = _healthBar;

        minusHeart();

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
        if(Input.GetKeyDown(KeyCode.D) && !_isAttacking)
            StartCoroutine(Attacking());

        Flip();
    }

    private void minusHeart() {
        for(int i=0;i<hearts.Length;i++) {
            if(i<_currentHealthBar)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if(i<_healthBar)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void Jump() {
        jumpSound.Play();
        rb.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); 
    }

    private void CheckGround() {
        bool contact = Physics2D.OverlapCircle(transform.position, 0.5f, Ground);
        _isGrounded = contact;

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
            minusHeart();
            HeadHB.color = new Color((float)0.17,(float)0.23,(float)0.415);
            State = States.death;
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        attackSound.Play();
        yield return new WaitForSeconds(0.75f);
        col.enabled = !col.enabled;
        _isAttacking = false;
        col.enabled = !col.enabled;
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
        _isInvincible = true;
        _canDash = false;
        _isDashing = true;
        _isAttacking = true;
        State = States.dash;
        dashSound.Play();
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        _isDashing = false;
        _isAttacking = false;
        rb.velocity = Vector2.zero;
        State = States.idle;
        _isInvincible = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}