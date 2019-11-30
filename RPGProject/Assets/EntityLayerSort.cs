using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLayerSort : MonoBehaviour
{

    public float playerOffset = 0.14f;

    SpriteRenderer[] entitySpriteRenderers;
   
    // Update is called once per frame
    void Update()
    {
        entitySpriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in entitySpriteRenderers) {
           if(sr.CompareTag("Entity")) sr.sortingOrder = (int) Mathf.Round(-sr.transform.position.y*10000);
           else if (sr.CompareTag("Player")) sr.sortingOrder = (int)Mathf.Round(-(sr.transform.position.y + playerOffset) * 10000)-1;
        }
    }
}
