using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBijai : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject.tag == "Player" && FindObjectOfType<EnemyBijai>()._isAttacking) {
            Debug.Log("Touched Player!");
            StartCoroutine(GettingDamage(col));
        } 
    }

    private IEnumerator GettingDamage(Collider2D col) {
        yield return new WaitForSeconds(0.3f);
        col.gameObject.GetComponent<MainCharacter>().GetDamage();
    }
}
