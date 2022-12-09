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
        Quaternion rotation = Quaternion.Euler(0,0,-90);
        _timer += Time.deltaTime;

        if(_timer >= _delay)
        {
            Instantiate(_bullet, _points[0].transform.position, rotation);
            Instantiate(_bullet, _points[1].transform.position, rotation);
            Instantiate(_bullet, _points[2].transform.position, rotation);
            _timer = 0f;
        }
    }
}
