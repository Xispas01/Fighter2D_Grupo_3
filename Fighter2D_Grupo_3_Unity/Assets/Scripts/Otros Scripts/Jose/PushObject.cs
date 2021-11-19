using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public Transform derecha;                                                                           //Ubicacion lado Derecho
    public Transform izquierda;                                                                         //Ubicacion lado Izquierdo
    public GameObject box;                                                                              //Objeto a empujar
    
    public Vector2 limite = new Vector2(0f, 0.5f);                                                      //Tamaños de boxcast Lados

    public Vector3 aux3D;                                                                               //vector auxiliar3D

    private int terrain;                                                                                //Mascaras para raycast
    private int player;                                                                                 //Mascaras para raycast
    private int empujable;                                                                              //Mascaras para raycast

    private Rigidbody2D rb;                                                                             //CuerpoRigido (Fisicas basada en fuerza y gravedad)
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();                                                    //Asigna el CuerpoRigido del objeto

        terrain = LayerMask.GetMask("Terrain","WallJump");                                              //Asigna valor a las mascaras de RayCast
        player = LayerMask.GetMask("Player");
        empujable = LayerMask.GetMask("Empujable");

        aux3D = box.transform.localScale;
        limite = new Vector2(0.1f , 0.5f * aux3D.y + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapBox(derecha.position, limite, 0.0f, terrain) != null
        && Physics2D.OverlapBox(derecha.position, limite, 0.0f, player) == null){
            gameObject.layer = 8;
        }else if(Physics2D.OverlapBox(derecha.position, limite, 0.0f, player) != null){
            gameObject.layer = 13;
        }
        
        if(Physics2D.OverlapBox(izquierda.position, limite, 0.0f, terrain) != null
        && Physics2D.OverlapBox(izquierda.position, limite, 0.0f, player) == null){
            gameObject.layer = 8;
        }else if(Physics2D.OverlapBox(izquierda.position, limite, 0.0f, player) != null){
            gameObject.layer = 13;
        }

        if(Physics2D.OverlapBox(derecha.position, limite, 0.0f, player) == null 
        && Physics2D.OverlapBox(izquierda.position, limite, 0.0f, player) == null){

            Vector2 aux = rb.velocity;                                                                  //Reinicio velocidad horizontal
            rb.velocity = new Vector2(0.0f, aux.y);
        }
    }
}
