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

    private void OnStart() {
        _isLeft = true;
    }

    private void Update()
    {
        if(_isdashing) return;

        if(!_isdashing) State = States.idle;

        _step =  _speed * Time.deltaTime;
        if(_isLeft == true)
        {
            _copperClawTransform.position = Vector3.MoveTowards(_copperClawTransform.position, _points[1].transform.position, _step);
            StartCoroutine(ToRight());
        }
        else
        {
            _copperClawTransform.position = Vector3.MoveTowards(_copperClawTransform.position, _points[0].transform.position, _step);
            StartCoroutine(ToLeft());
        }
    }

    private IEnumerator ToRight()
    {
        _isdashing = true;
        State = States.dash;
        yield return new WaitForSeconds(_delay);
        Vector3 localScale = _copperClawTransform.localScale;
        localScale.x *= -1f;
        _copperClawTransform.localScale = localScale;
        State = States.idle;
        _isLeft = false;
        _isdashing = false;
    }

    private IEnumerator ToLeft() {
        _isdashing = true;
        State = States.dash;
        yield return new WaitForSeconds(_delay);
        Vector3 localScale = _copperClawTransform.localScale;
        localScale.x *= -1f;
        _copperClawTransform.localScale = localScale;
        State = States.idle;
        _isLeft = true;
        _isdashing = false;
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
