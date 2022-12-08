using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUppedSpawn : SpawnerDots
{
    [SerializeField] private GameObject _bullet;
    private float _timer;

    private void Update() 
    {
        BulletSpawn();    
    }

    private void BulletSpawn()
    {
        transform.position = new Vector2(_player.transform.position.x, transform.position.y);
        _timer += Time.deltaTime;

        if(_timer >= _delay)
        {
            Instantiate(_bullet, _points[Random.Range(0, _points.Length)].transform.position, Quaternion.identity);
            _timer = 0f;
        }
    }
}
