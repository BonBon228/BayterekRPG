using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : SpawnerDots
{
    [SerializeField] private GameObject _jinEnemy;
    private float _timer;

    private void Update()
    {
        JinSpawn();
    }

    private void JinSpawn()
    {
        
        transform.position = new Vector2(_player.transform.position.x, transform.position.y);
        _timer += Time.deltaTime;

        if(_timer >= _delay)
        {
            Instantiate(_jinEnemy, _points[Random.Range(0, _points.Length)].transform.position, Quaternion.identity);
            _timer = 0f;
        }
    }
}
