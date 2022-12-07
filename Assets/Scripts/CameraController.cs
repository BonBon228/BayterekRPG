using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;

    private void Awake() {
        if(!player)
            player = FindObjectOfType<MainCharacter>().transform;
    }

    private void Update() {
        pos = player.position;
        if(pos.x < -5)
            pos.x = -5;
        if(pos.y < 0)
            pos.y = 0;
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime*4);
    }
}
