using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPointToPoint : SpawnerDots
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _copperClawTransform;
    private bool _isLeft;
    private Transform target;
    private float _step;

    private void Start()
    {
        _isLeft = true;
    }

    void Update()
    {
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
        yield return new WaitForSeconds(_delay);
        _isLeft = false;
    }

    private IEnumerator ToLeft()
    {
        yield return new WaitForSeconds(_delay);
        _isLeft = true;
    }
}
