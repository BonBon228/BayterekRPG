using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBijai : Enemy
{
    private int _permSpeed;
    private bool _canAttack = true;
    private bool _isAttacking;

    void Start()
    {
        base.Start();
        _permSpeed = speed;
    }

    void Update()
    {
        base.Update();
        if(_isAttacking)
        {
            return;
        }
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        _spriteRenderer.flipX = transform.position.x < playerTransform.position.x;
        if(distance > 2f)
        {
            speed = _permSpeed;

            State = States.walk;
            
            if(transform.position.x < playerTransform.position.x)
            {
                transform.Translate(speed/10 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-speed/10 * Time.deltaTime, 0, 0);
            }
        }
        else
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
        attack
    }
}
