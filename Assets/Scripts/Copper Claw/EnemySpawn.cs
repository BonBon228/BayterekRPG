using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : SpawnerDots
{
    [SerializeField] private GameObject _jinEnemy;
    [SerializeField] private Animator anim;
    private float _timer;

    private void OnEnable() {
        //transform.position = new Vector2(_player.transform.position.x, transform.position.y);
        JinSpawn();
    }

    private void Update() {
        //transform.position = new Vector2(_player.transform.position.x, transform.position.y);

    }

    public void JinSpawn()
    {
        StartCoroutine(ChangeState());
        Instantiate(_jinEnemy, _points[Random.Range(0, _points.Length)].transform.position, Quaternion.identity);
    }

    private IEnumerator ChangeState() {
        State = States.spawn;
        yield return new WaitForSeconds(1f);
        State = States.idle;
    }

    private States State {
        get {return (States)anim.GetInteger("myState");}
        set {anim.SetInteger("myState", (int)value);}
    }

    private enum States {
        idle,
        shoot,
        dash,
        spawn
    }
}
