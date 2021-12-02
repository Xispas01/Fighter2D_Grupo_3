using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float vel;
    public GameObject go;
    public Transform t;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fram
    //Para que el objeto se mueva
    void FixedUpdate()
    {
        rb.AddForce(transform.right*vel,ForceMode2D.Impulse);
    }
    void Update()
    {
        //Para que destruya el objeto tras pasar x distancia
        if (t.position.x >= 10) 
        {
            Destroy(go);
        }
    }
}
