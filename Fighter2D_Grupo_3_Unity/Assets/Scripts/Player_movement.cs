using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float jump = 300.0f;
    public float speed = 2.0f;
    public float speedLimit = 5.0f;
    
    public Transform pies;
    public Transform ladoD;
    public Transform ladoI;
    public int jumpsMax = 1;
    public Vector2 limSuelo = new Vector2(0.5f, 0f);
    public Vector2 limLado = new Vector2(0f, 0.5f);


    private Vector2 aux;

    int jumpsN;

    Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Jump", KeyCode.Space);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.IsPaused==false){
            //Salto configurable con jumpsMax como limite de saltos aereos
            if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f) != null)
            {
                if (Input.GetKeyDown(keys["Jump"]))
                {
                    rb.AddForce(Vector3.up * jump);
                    jumpsN = 0;
                }
            } else if (Input.GetKeyDown(keys["Jump"]) && (jumpsN < jumpsMax))
            {
                aux = rb.velocity;
                aux.y = 0f;
                rb.velocity = aux;
                rb.AddForce(Vector3.up * jump);
                jumpsN++;
            }
            
            if (Physics2D.OverlapBox(ladoI.position, limLado, 0.0f) == null)
            {
                    //Control configurable
                if (Input.GetKey(keys["Left"]))
                {
                    rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
                    //Limite velocidad
                    if (rb.velocity.x <= speedLimit)
                    {
                        aux = rb.velocity;
                        rb.velocity = new Vector2(-speedLimit, aux.y);
                    }
                }
            }
    
            if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f) == null)
            {
                if (Input.GetKey(keys["Right"]))
                {
                    rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
                    //Limite velocidad
                    if (rb.velocity.x >= speedLimit)
                    {
                        aux = rb.velocity;
                        rb.velocity = new Vector2(speedLimit, aux.y);
                    }
                }
            }
    
            //Reinicio velocidad X
            if (Input.GetKeyUp(keys["Right"]) || Input.GetKeyUp(keys["Left"]) || (Input.GetKey(keys["Right"]) && Input.GetKey(keys["Left"])))
            {
                Vector2 aux = rb.velocity;
                rb.velocity = new Vector2(0.0f, aux.y);
                
            }
        }
    }
}
