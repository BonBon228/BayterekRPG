using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBijaiRun : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Player" && FindObjectOfType<EnemyBijai>()._isUlting) {
            Debug.Log("Touched Player!");
            GettingDamage(col);
        } 
    }

    private void GettingDamage(Collider2D col) {
        col.gameObject.GetComponent<MainCharacter>().GetDamage();
    }
}
