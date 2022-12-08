using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWaySpawn : SpawnerDots
{
    [SerializeField] private GameObject _bullet;
    private float _timer;

    private void Update() 
    {
        BulletSpawn();    
    }

    private void BulletSpawn()
    {
        _timer += Time.deltaTime;

        if(_timer >= _delay)
        {
            Instantiate(_bullet, _points[Random.Range(0, _points.Length)].transform.position, Quaternion.identity);
            _timer = 0f;
        }
    }
}
