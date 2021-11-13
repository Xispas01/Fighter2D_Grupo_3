using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEdgeColider2D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EdgeCollider2D edge = gameObject.GetComponent<EdgeCollider2D>();
        if (edge == null)
        {
            edge = gameObject.AddComponent<EdgeCollider2D>();
        }

        PolygonCollider2D poly = gameObject.GetComponent<PolygonCollider2D>();
        if (poly == null)
        {
            poly = gameObject.AddComponent<PolygonCollider2D>();
        }

        Vector2[] puntos1 = poly.points;
        int i;
        int p1 = puntos1.Length;
        int p2 = puntos1.Length - 1;
        int p3 = puntos1.Length + 1;
        Vector2[] puntos2 = new Vector2[p3];
        for(i = 0; i <= p2; i++)
        {
            puntos2[i] = puntos1[i];
        }
        puntos2[p1] = puntos1[0];
        edge.points = puntos2;
        Destroy(poly);
    }
}
