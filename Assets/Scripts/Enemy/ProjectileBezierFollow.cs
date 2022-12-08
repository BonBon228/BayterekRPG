using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBezierFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    public int routeToGo;
    private float tParam;
    private Vector2 _projectilePosition;
    public float speedModifier;
    public bool CoroutineAllowed { get; private set; }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col != null) {
            if(col.gameObject == MainCharacter.Instance.gameObject) {
                MainCharacter.Instance.GetDamage();
            }
        }
    }

    private void Start() 
    {
        routeToGo = 0;
        tParam = 0f;
        CoroutineAllowed = true;
    }

    private void Update() 
    {
        if(CoroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }    
    }

    public IEnumerator GoByTheRoute(int routeNumber)
    {
        CoroutineAllowed = false;
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            _projectilePosition = Mathf.Pow(1 - tParam, 3) * p0 +
            3 * Mathf.Pow(1 - tParam, 2) * tParam  * p1 +
            3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
            Mathf.Pow(tParam, 3) * p3;

            transform.position = _projectilePosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if(routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        CoroutineAllowed = true;
    }
}
