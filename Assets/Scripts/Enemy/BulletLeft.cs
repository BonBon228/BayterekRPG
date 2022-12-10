using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeft : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _lifeTime = 4f;
    private Transform _copperClawTransform;
    private Transform playerTransform;
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _copperClawTransform = GameObject.FindGameObjectWithTag("CopperClaw").transform;
    }

    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            Debug.Log("Touched Player!");
            GettingDamage(col);
        } 
    }

    private void GettingDamage(Collider2D col) {
        col.gameObject.GetComponent<MainCharacter>().GetDamage();
    }

    void Update()
    {

        if(_copperClawTransform.position.x > playerTransform.position.x)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector3.down * -_speed * Time.deltaTime);
        }

        _lifeTime -= Time.deltaTime;

        if(_lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
