using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    private bool _isShooting;
    private bool _canShoot;
    private Vector3 _normal;
    [SerializeField] private Transform[] controlPoints;
    [SerializeField] private GameObject _projectile;
    private Vector2 gizmozPosition;
    private Transform _playerTransform;
    
    
    private void Start() 
    {
        base.Start();
        _playerTransform = FindObjectOfType<MainCharacter>().GetComponent<Transform>();
    }

    private void Update() 
    {
        base.Update();
        controlPoints[3].position = new Vector2(_playerTransform.position.x, _playerTransform.position.y);
        controlPoints[2].position = new Vector2(_playerTransform.position.x, _playerTransform.position.y + distance / 2f);
        controlPoints[1].position = new Vector2(controlPoints[1].position.x, transform.position.y + distance / 2f);
        if(_isShooting)
        {
            return;
        }
        CalculatePositionToAttack();
        ShootPlayer();
        MoveCharacter();
    }

    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t+=0.05f)
        {
            gizmozPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
            3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 
            3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
            Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmozPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
        new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));

        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
        new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
    }
    
    private void MoveCharacter()
    {
        _spriteRenderer.flipX = transform.position.x < playerTransform.position.x;

        if(distance > 10 && _canShoot == true)
        {
            if(transform.position.x < playerTransform.position.x)
            {
                transform.Translate(speed/10 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-speed/10 * Time.deltaTime, 0, 0);
            }
        }
        if(distance < 7f)
        {
            if(transform.position.x < playerTransform.position.x)
            {
                transform.Translate(-speed/10 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(speed/10 * Time.deltaTime, 0, 0);
            }
        }
    }

    private void ShootPlayer()
    {
        if(_canShoot == true)
        {
            TimeToShoot();
        }
    }

    private void CalculatePositionToAttack()
    {
        if(distance <= 8f) 
        {
            _canShoot = true;
        }
    }

    private IEnumerator TimeToShoot()
    {
        _isShooting = true;
        Instantiate(_projectile, controlPoints[0].position, Quaternion.identity);
        State = States.attack;
        yield return new WaitForSeconds(1f);
        _isShooting = false;
        State = States.idle;
        
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
