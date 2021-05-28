using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRaycast : MonoBehaviour
{
    public float distance = 0.1f;
    public GameObject mystery;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       Physics2D.queriesStartInColliders = false;
       RaycastHit2D hit =  Physics2D.Raycast(transform.position, Vector2.right * transform.parent.gameObject.transform.localScale.x, distance);

       if (hit.collider != null && hit.collider.gameObject.tag == "Mystery") {
           mystery.GetComponent<SpriteRenderer>().enabled = true;
       } else {
           mystery.GetComponent<SpriteRenderer>().enabled = false;
       }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.parent.gameObject.transform.localScale.x * distance);    
    }
}
