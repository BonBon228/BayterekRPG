using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : SpawnerDots
{
    [SerializeField] private GameObject _jinEnemy;
    private float _timer;

    private void OnEnable() {
        transform.position = new Vector2(_player.transform.position.x, transform.position.y);
        JinSpawn();
    }

    private void Update() {
        transform.position = new Vector2(_player.transform.position.x, transform.position.y);
    }

    public void JinSpawn()
    {
        Instantiate(_jinEnemy, _points[Random.Range(0, _points.Length)].transform.position, Quaternion.identity);
    }
}
