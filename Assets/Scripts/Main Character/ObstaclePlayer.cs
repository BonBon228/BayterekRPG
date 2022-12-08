using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePlayer : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col) {
        if(col != null) {
            if(col.gameObject == Enemy.Instance.gameObject && FindObjectOfType<MainCharacter>()._isAttacking) {
                Enemy.Instance.GetDamage();
            }
        }
    }
}
