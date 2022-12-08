using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _lifeTime = 4f;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        _lifeTime -= Time.deltaTime;

        if(_lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
