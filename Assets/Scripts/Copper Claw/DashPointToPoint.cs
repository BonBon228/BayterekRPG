using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPointToPoint : SpawnerDots
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isLeft;
    private Transform target;
    private float _step;
    private bool _isDashing;

    private void Start()
    {
        _isLeft = true;
    }

    void Update()
    {
        transform.position = new Vector2(_player.transform.position.x, transform.position.y);
        _step =  _speed * Time.deltaTime;
        if(_isLeft == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[1].transform.position, _step);
            StartCoroutine(ToRight());
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[0].transform.position, _step);
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
