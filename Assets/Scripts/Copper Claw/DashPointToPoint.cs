using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPointToPoint : SpawnerDots
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _copperClawTransform;
    [SerializeField] private Animator anim;
    private bool _isLeft;
    private Transform target;
    private float _step;
    private bool _isdashing = false;
    private Transform playerTransform;
    private bool _isFacingRight = false;

    private void OnEnable() {
        _isLeft = true;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDisable() {
        State = States.idle;
    }

    private void Update()
    {
        //if(_isdashing) return;
//
        //if(!_isdashing) State = States.idle;

        _step =  _speed * Time.deltaTime;
        
        if(_isLeft == true)
        {
            Flip();
            _copperClawTransform.position = Vector3.MoveTowards(_copperClawTransform.position, _points[1].transform.position, _step);
            StartCoroutine(ToRight());
        }
        else
        {
            Flip();
            _copperClawTransform.position = Vector3.MoveTowards(_copperClawTransform.position, _points[0].transform.position, _step);
            StartCoroutine(ToLeft());
        }
    }

    private void Flip() {
        if(_isFacingRight && _copperClawTransform.position.x > playerTransform.position.x || !_isFacingRight && playerTransform.position.x > _copperClawTransform.position.x) {
            Vector3 localScale = _copperClawTransform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            _copperClawTransform.localScale = localScale;
        }
    }

    private IEnumerator ToRight()
    {
        //_isdashing = true;
        State = States.dash;
        yield return new WaitForSeconds(0.3f);
        State = States.idle;
        yield return new WaitForSeconds(_delay);
        _isLeft = false;
        //_isdashing = false;
    }

    private IEnumerator ToLeft() {
        //_isdashing = true;
        State = States.dash;
        yield return new WaitForSeconds(0.3f);
        State = States.idle;
        yield return new WaitForSeconds(_delay);
        _isLeft = true;
        //_isdashing = false;
    }

    private States State {
        get {return (States)anim.GetInteger("myState");}
        set {anim.SetInteger("myState", (int)value);}
    }

    private enum States {
        idle,
        shoot,
        dash
    }
}
