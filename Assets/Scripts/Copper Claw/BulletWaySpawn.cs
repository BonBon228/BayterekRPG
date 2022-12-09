using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWaySpawn : SpawnerDots
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Animator anim;
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
            StartCoroutine(Shoot());
            _timer = 0f;
        }
    }

    private IEnumerator Shoot() {
        State = States.shoot;
        Quaternion rotation = Quaternion.Euler(0,0,-90);
        Instantiate(_bullet, _points[0].transform.position, rotation);
        Instantiate(_bullet, _points[1].transform.position, rotation);
        Instantiate(_bullet, _points[2].transform.position, rotation);
        yield return new WaitForSeconds(0.4f);
        State = States.idle;
        State = States.shoot;
        Instantiate(_bullet, _points[0].transform.position, rotation);
        Instantiate(_bullet, _points[1].transform.position, rotation);
        Instantiate(_bullet, _points[2].transform.position, rotation);
        yield return new WaitForSeconds(1f);
        State = States.idle;
        State = States.shoot;
        Instantiate(_bullet, _points[0].transform.position, rotation);
        Instantiate(_bullet, _points[1].transform.position, rotation);
        Instantiate(_bullet, _points[2].transform.position, rotation);
        yield return new WaitForSeconds(0.4f);
        State = States.idle;
        State = States.shoot;
        Instantiate(_bullet, _points[0].transform.position, rotation);
        Instantiate(_bullet, _points[1].transform.position, rotation);
        Instantiate(_bullet, _points[2].transform.position, rotation);
        State = States.idle;
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
