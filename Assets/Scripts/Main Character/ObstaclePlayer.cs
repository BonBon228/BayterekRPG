using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Enemy" && FindObjectOfType<MainCharacter>()._isAttacking) {
            col.gameObject.GetComponent<Enemy>().GetDamage();
        } 
    }
}
