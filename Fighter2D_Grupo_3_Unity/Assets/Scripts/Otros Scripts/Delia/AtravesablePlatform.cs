using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtravesablePlatform : MonoBehaviour
{
    private GameObject[] player;
    private CircleCollider2D colliderPlayer1;
    private CircleCollider2D colliderPlayer2;
    private BoxCollider2D colliderPlatform;
    private Bounds colliderPlatformBounds;
    private float colliderPlayer1Radius;
    private float colliderPlayer2Radius;
    private float topPlatform, footPlayer1, footPlayer2;
    private bool isPlayer1;
    private bool isPlayer2;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        colliderPlayer1 = player[0].GetComponent<CircleCollider2D>();                                //Se supone que siempre hay únicamente 2 jugadores en game
        colliderPlayer2 = player[1].GetComponent<CircleCollider2D>();
        colliderPlatform = GetComponent<BoxCollider2D>();
        colliderPlatformBounds = colliderPlatform.bounds;
        colliderPlayer1Radius = colliderPlayer1.radius;
        colliderPlayer2Radius = colliderPlayer2.radius;
        topPlatform = colliderPlatformBounds.center.y + colliderPlatformBounds.extents.y;
        isPlayer1 = false;
        isPlayer2 = false;

    }

    // Update is called once per frame
    void Update()
    {
        footPlayer1 = player[0].transform.position.y - colliderPlayer1Radius; 
        footPlayer2 = player[1].transform.position.y - colliderPlayer2Radius;
        if (footPlayer1 >= topPlatform) {
            Physics2D.IgnoreCollision(player[0].GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), false);
        }
        if (footPlayer2 >= topPlatform)
        {
            Physics2D.IgnoreCollision(player[1].GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(), false);
        }
        if (footPlayer1 < topPlatform - 0.1f) {
            Physics2D.IgnoreCollision(player[0].GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(),true);    
        }
        if (footPlayer2 < topPlatform - 0.1f)
        {
            Physics2D.IgnoreCollision(player[1].GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>(),true);    
        }
    }
}
