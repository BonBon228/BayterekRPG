using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField] private AudioSource bossFightMusic;
    [SerializeField] private AudioSource canvas;
    [SerializeField] private GameObject mobSpawn;

    private bool activated = false;
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player" && !activated) {
            Debug.Log("Player entered!");
            canvas.Stop();
            bossFightMusic.Play();
            activated = true;
            StartCoroutine(StartFight());
        }
    }

    private IEnumerator StartFight() {
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
    }
}
