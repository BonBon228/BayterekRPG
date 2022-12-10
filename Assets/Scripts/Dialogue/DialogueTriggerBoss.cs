using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBoss : MonoBehaviour
{
    public GameObject dialogueBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            other.GetComponentInChildren<Animator>().SetInteger("myState", 1);
            other.GetComponent<MainCharacter>().enabled = false;
        }
    }
}
