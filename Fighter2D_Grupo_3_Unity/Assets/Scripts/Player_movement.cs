using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    //variables                                                                                 
    public float jump = 400.0f;                                                                         //Fuerza de salto
    public float speed = 2.0f;                                                                          //Fuerza de movimiento
    public float speedLimit = 5.0f;                                                                     //Limite de velocidad
    public float Wj = 200.0f;                                                                           //Fuerza lateral de WallJump

    public Transform pies;                                                                              //Ubicacion Pies
    public Transform ladoD;                                                                             //Ubicacion lado Derecho
    public Transform ladoI;                                                                             //Ubicacion lado Izquierdo
    public int jumpsMax = 1;                                                                            //Maximos AirJumps
    int jumpsN;                                                                                         //AirJumps actuales

    public Vector2 limSuelo = new Vector2(0.5f, 0f);                                                    //Tamaños de boxcast Techo/suelo
    public Vector2 limLado = new Vector2(0f, 0.5f);                                                     //Tamaños de boxcast Lados

    private int terrain;                                                                                //Mascaras para raycast
    private int wallJump;                                                                               //Mascaras para raycast

    private bool canControl = true;                                                                     //Cooldown Control WallJump

    private bool BoxCastWallJumpD;                                                                      //Resultados de BoxCast
    private bool BoxCastWallJumpI;                                                                      //Resultados de BoxCast
    private bool BoxCastWallD;                                                                          //Resultados de BoxCast
    private bool BoxCastWallI;                                                                          //Resultados de BoxCast
    private bool BoxCastGround;                                                                         //Resultados de BoxCast

    private Vector2 aux;                                                                                //Vector 2D auxiliar


    Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();                                //Diccionario para controles configurables

    private Rigidbody2D rb;                                                                             //CuerpoRigido (Fisicas basada en fuerza y gravedad)


    void Start()                                                                                        //Inicio Start()
    {                   
        rb = gameObject.GetComponent<Rigidbody2D>();                                                    //Asigna el CuerpoRigido del objeto

        //Asignacion controles default                  
        keys.Add("Left", KeyCode.A);                                                                    //Asigna a la palabra "Left" la tecla (KeyCode) A
        keys.Add("Right", KeyCode.D);                                                                   //Asigna a la palabra "Right" la tecla (KeyCode) D
        keys.Add("Jump", KeyCode.Space);                                                                //Asigna a la palabra "Jump" la tecla (KeyCode) Space

        terrain = LayerMask.GetMask("Terrain","WallJump");                                              //Asigna valor a las mascaras de RayCast
        wallJump = LayerMask.GetMask("WallJump");                                                       //Asigna valor a las mascaras de RayCast

        canControl = true;                                                                              //Inicializa a true
    }

    // Update is called once per frame
    void Update()                                                                                       //Inicio Update()
    {
        if (PauseMenu.IsPaused==false && canControl == true)                                            //Revision de pausa Y control
        {
            if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, terrain) != null )                  //Revisa BoxCast al Suelo
            {
                jumpsN = 0;                                                                             //Reinicia AirJumps
                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    rb.AddForce(Vector3.up * jump);                                                     
                }
            }else if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                aux = rb.velocity;                                                                      //Reinicia velocidad de caida a 3 (Efecto deslizar pared)
                rb.velocity = new Vector2(aux.x,-3f);                                                   //Reinicia velocidad de caida a 3 (Efecto deslizar pared)

                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    aux = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.left * Wj);                                 //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            }else if(Physics2D.OverlapBox(ladoI.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                aux = rb.velocity;                                                                      //Reinicia velocidad de caida a 3 (Efecto deslizar pared)
                rb.velocity = new Vector2(aux.x,-3f);                                                   //Reinicia velocidad de caida a 3 (Efecto deslizar pared)

                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    aux = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.right * Wj);                                //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            } else if (Input.GetKeyDown(keys["Jump"]) && jumpsN < jumpsMax)                             //Revisa limite de AirJumps
            {
                aux = rb.velocity;                                                                      //Reinicia velocidad vertical antes de saltar
                rb.velocity = new Vector2(aux.x,0f);                                                    //Reinicia velocidad vertical antes de saltar
                rb.AddForce(Vector3.up * jump);                                                         
                jumpsN++;
                
            }

            if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, terrain) == null)                    //Evita aplicar fuerza contra un muro
            {                                                                                           
                if (Input.GetKey(keys["Right"]))                                                        //Movimiento Derecha
                {                                                                                       
                    rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);                            

                    if (rb.velocity.x >= speedLimit)                                                    //Limitacion de velocidad
                    {                                                                                   
                        aux = rb.velocity;                                                              
                        rb.velocity = new Vector2(speedLimit, aux.y);                                   
                    }                                                                                   
                }                                                                                       
            } 
            
            if (Physics2D.OverlapBox(ladoI.position, limLado, 0.0f, terrain) == null)                   //Evita aplicar fuerza contra un muro
            {                                                                                           
                if (Input.GetKey(keys["Left"]))                                                         //Movimiento Izquierda
                {                                                                                       
                    rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);                             

                    if (rb.velocity.x <= speedLimit)                                                    //Limitacion de velocidad
                    {                                                                                   
                        aux = rb.velocity;                                                              
                        rb.velocity = new Vector2(-speedLimit, aux.y);                                  
                    }                                                                                   
                }                                                                                       
            }                                                                                                                                                                                     

            //Reinicio velocidad eje X
            if (Input.GetKeyUp(keys["Right"]) || Input.GetKeyUp(keys["Left"])                           //Revision soltar teclas movimiento
            || (Input.GetKey(keys["Right"]) && Input.GetKey(keys["Left"])))                             //Revision pulsar simultaneas teclas movimiento
            {
                Vector2 aux = rb.velocity;                                                              //Reinicio velocidad horizontal
                rb.velocity = new Vector2(0.0f, aux.y);                                                 
            }
        }
    }

    IEnumerator WallJumpCD() {                                                                          //Cooldown control WallJump
        yield return new WaitForSeconds(0.2f);
        canControl = true;
    }
}
