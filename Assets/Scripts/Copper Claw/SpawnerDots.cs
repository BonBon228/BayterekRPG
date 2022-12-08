using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDots : MonoBehaviour
{
    [SerializeField] protected GameObject[] _points;
    [SerializeField] protected float _delay;
    protected GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
