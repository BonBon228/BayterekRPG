using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJin : Enemy
{   
    private bool _isHunting = false;
    private bool _isInRadius = false;
    private void FixedUpdate() 
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        if(distance <= 7.5f)
        {
            if(distance < 5) 
            {
                _isInRadius = true;
            }
            else if(distance >= 5 && _isHunting && _isInRadius)
            {
                enemyRb.velocity = Vector2.Lerp(transform.position, Vector2.zero, Time.deltaTime);
                _isHunting = false;
                _isInRadius = false;
            }
            else
            {
                enemyRb.AddForce(-direction * speed);
                _isHunting = true;
            }
        }
        else
        {
            _isHunting = false;
        }
    }
}
