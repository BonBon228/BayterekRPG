using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField] private AudioSource bossFightMusic;
    [SerializeField] private AudioSource canvas;
    [SerializeField] private GameObject mobSpawn;
    [SerializeField] private GameObject dash;
    [SerializeField] private GameObject clawShoot;
    [SerializeField] private GameObject clawFall;
    [SerializeField] private GameObject bossWallOne;
    [SerializeField] private GameObject bossWallTwo;

    private bool activated = false;
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player" && !activated) {
            Debug.Log("Player entered!");
            canvas.Stop();
            bossFightMusic.Play();
            FindObjectOfType<CameraController>().changeBool();
            bossWallOne.SetActive(true);
            bossWallTwo.SetActive(true);
            activated = true;
            StartCoroutine(StartFight());
        }
    }

    private IEnumerator Test() {
        yield return new WaitForSeconds(3.5f);
        dash.SetActive(true);
        yield return new WaitForSeconds(2f);
        dash.SetActive(false);
        yield return new WaitForSeconds(1f);
        dash.SetActive(true);
    }

    private IEnumerator StartFight() {
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(3f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        //9sec
        yield return new WaitForSeconds(3f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(3f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        //22sec
        clawShoot.SetActive(true);
        yield return new WaitForSeconds(15f);
        clawShoot.SetActive(false);
        //35sec
        dash.SetActive(true);
        yield return new WaitForSeconds(11.5f);
        dash.SetActive(false);
        //47sec
        clawFall.SetActive(true);
        yield return new WaitForSeconds(10f);
        clawFall.SetActive(false);
        //56sec
        mobSpawn.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        mobSpawn.SetActive(false);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(3f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2f);
        //1:07sec
        clawShoot.SetActive(true);
        dash.SetActive(true);
        yield return new WaitForSeconds(13f);
        clawShoot.SetActive(false);
        dash.SetActive(false);
        yield return new WaitForSeconds(2f);
        //1:22sec
        mobSpawn.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        mobSpawn.SetActive(false);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(3f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(2.7f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        mobSpawn.SetActive(true);
        mobSpawn.SetActive(false);
        //1:31sec
        yield return new WaitForSeconds(3f);
        clawShoot.SetActive(true);
        yield return new WaitForSeconds(11f);
        clawFall.SetActive(true);
        yield return new WaitForSeconds(9f);
        clawShoot.SetActive(false);
        clawFall.SetActive(false);
    }
}
