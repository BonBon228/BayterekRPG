using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJin : Enemy
{   
    private bool _isHunting = false;
    private bool _isInRadius = false;
    private bool _isDashing;

    private void Update() 
    {
        base.Update();
        if(_isDashing)
        {
            return;
        }
        CalculatePositionToAttack();
    }

    private void FixedUpdate() 
    {   
        if(_isDashing)
        {
            return;
        }
        if(_isHunting == true)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        _spriteRenderer.flipX = transform.position.x < playerTransform.position.x;
        StartCoroutine(TimeToDash()); 
    }

    private void DashToPlayer()
    {
        enemyRb.AddForce(-direction * speed * Time.deltaTime);
    }

    private void CalculatePositionToAttack()
    {
        if(distance <= 7.5f && !_isInRadius) 
        {
            _isHunting = true;
            _isInRadius = true;
            _spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    private IEnumerator TimeToDash()
    {
        _isDashing = true;
        DashToPlayer();
        State = States.djin_attack;
        yield return new WaitForSeconds(0.5f);
        enemyRb.velocity = Vector3.zero;
        State = States.djin_idle;
        _isHunting = false;
        yield return new WaitForSeconds(1f);
        _isDashing = false;
        _isHunting = true;
    }

    private States State {
        get {return (States)_enemyAnim.GetInteger("myState");}
        set {_enemyAnim.SetInteger("myState", (int)value);}
    }
    
    private enum States {
        djin_idle,
        djin_attack
    }
}
