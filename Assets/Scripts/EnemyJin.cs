using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJin : Enemy
{   
    private bool _isHunting = false;
    private bool _isInRadius = false;

    private void Update() 
    {
        base.Update();
        
        CalculatePositionToAttack();
    }

    private void FixedUpdate() 
    {
        if(_isHunting == true)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
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
        }
    }

    private IEnumerator TimeToDash()
    {
        DashToPlayer();
        yield return new WaitForSeconds(0.5f);
        enemyRb.velocity = Vector3.zero;
        _isHunting = false;
        yield return new WaitForSeconds(1f);
        _isHunting = true;
    }
}
