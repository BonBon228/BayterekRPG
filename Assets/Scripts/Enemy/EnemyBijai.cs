using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBijai : Enemy
{
    private int _permSpeed;
    public bool _canAttack = true;
    public bool _canUlt = true;
    public bool _isAttacking = false;
    public bool _isUlting = false;

    void Start()
    {
        base.Start();
        _permSpeed = speed;
    }

    void Update()
    {
        base.Update();
        if(_isUlting)
        {
            return;
        }
        if(_isAttacking)
        {
            return;
        }
        MoveToPlayer();
    }

    void FixedUpdate()
    {
        if(_isUlting)
        {
            return;
        }
        RunToPlayer();
    }

    private void RunToPlayer()
    {
        if(distance > 8f && distance < 15f)
        {
            if(_canUlt == true)
            {
                StartCoroutine(DashPlayer());
            }
        }
    }

    private void MoveToPlayer()
    {
        Flip();
        if(distance > 2f && _isUlting == false)
        {
            speed = _permSpeed;

            State = States.walk;
            
            if(transform.position.x < playerTransform.position.x)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(-transform.right * speed * Time.deltaTime);
            }
        }
        else if(distance < 2f)
        {
            if(_canAttack == true)
            {
                StartCoroutine(AttackDelay());
            }
            else
            {
                State = States.idle;
            }
        }
    }

    private void DashToPlayer()
    {

    }

    private IEnumerator DashPlayer()
    {
        _canUlt = false;
        _isUlting = true;
        if(transform.position.x < playerTransform.position.x)
        {
            enemyRb.AddForce(transform.right * speed * 7000 * Time.deltaTime);
        }
        else
        {
            enemyRb.AddForce(-transform.right * speed * 7000 * Time.deltaTime);
        }
        State = States.dash;
        yield return new WaitForSeconds(2f);
        enemyRb.velocity = Vector2.zero;
        _isUlting = false;
        speed = 20;
        yield return new WaitForSeconds(Random.Range(5, 10));
        _canUlt = true;
    }

    private IEnumerator AttackDelay()
    {
        _canAttack = false;
        _isAttacking = true;
        speed = 0;
        State = States.attack;
        yield return new WaitForSeconds(1f);
        _isAttacking = false;
        State = States.idle;
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }

    private States State {
        get {return (States)_enemyAnim.GetInteger("myState");}
        set {_enemyAnim.SetInteger("myState", (int)value);}
    }
    
    private enum States {
        idle,
        walk,
        attack,
        dash
    }
}
