using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSwapkey : MonoBehaviour
{
    //variables                                                                                 
    public float jump = 400.0f;                                                                         //Fuerza de salto
    public float speed = 2.0f;                                                                          //Fuerza de movimiento
    public float speedLimit = 5.0f;                                                                     //Limite de velocidad
    public float Wj = 300.0f;                                                                           //Fuerza lateral de WallJump

    public Transform pies;                                                                              //Ubicacion Pies
    public Transform ladoD;                                                                             //Ubicacion lado Derecho
    public Transform ladoI;                                                                             //Ubicacion lado Izquierdo
    public int jumpsMax = 1;                                                                            //Maximos AirJumps
    int jumpsN;                                                                                         //AirJumps actuales


    public Vector2 limSuelo = new Vector2(0.5f, 0f);                                                    //Tamaños de boxcast Techo/suelo
    public Vector2 limLado = new Vector2(0f, 0.5f);                                                     //Tamaños de boxcast Lados

    private int ground;                                                                                 //Mascaras para raycast
    private int wall;                                                                                   //Mascaras para raycast
    private int wallJump;                                                                               //Mascaras para raycast

    private bool canControl = true;                                                                     //Cooldown Control WallJump (Sirve para ser lanzado sin control)

    private Vector2 aux2D;                                                                              //Vector 2D auxiliar

    Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();                                //Diccionario para controles configurables

    public int player;

    private Rigidbody2D rb;                                                                             //CuerpoRigido (Fisicas basada en fuerza y gravedad)


    void Start()                                                                                        //Inicio Start()
    {                   
        rb = gameObject.GetComponent<Rigidbody2D>();                                                    //Asigna el CuerpoRigido del objeto

        //Asignacion controles default              
        switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
            case 1:{
                keys.Add("Left", ButtonSwap.keys["LeftA"]);
                keys.Add("Right", ButtonSwap.keys["RightA"]);                                             
                keys.Add("Jump", ButtonSwap.keys["JumpA"]);                                               
                break;
            }
            case 2:{
                keys.Add("Left", ButtonSwap.keys["LeftB"]);                                               
                keys.Add("Right", ButtonSwap.keys["RightB"]);                                             
                keys.Add("Jump", ButtonSwap.keys["JumpB"]);                                               
                break;
            }
        }

        ground = LayerMask.GetMask("Terrain","WallJump","Empujable");                                   //Asigna valor a las mascaras de RayCast
        wall = LayerMask.GetMask("Terrain","WallJump");                                                 //Asigna valor a las mascaras de RayCast
        wallJump = LayerMask.GetMask("WallJump");                                                       //Asigna valor a las mascaras de RayCast

        canControl = true;                                                                              //Inicializa a true
    }

    // Update is called once per frame
    void Update()                                                                                       //Inicio Update()
    {
        if (PauseMenu.IsPaused==false && canControl == true)                                            //Revision de pausa Y control
        {

            if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, ground) != null )                   //Revisa BoxCast al Suelo
            {
                jumpsN = 0;                                                                             //Reinicia AirJumps
                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    rb.AddForce(Vector3.up * jump);                                                     
                }
            }else if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                
                aux2D = rb.velocity;                                                                      
                if(aux2D.y <= -3f)                                                                      //Reinicia velocidad de caida a 3 si va a caer(Efecto deslizar pared)
                {
                rb.velocity = new Vector2(aux2D.x,-3f);                                                   
                }

                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    aux2D = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux2D.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.left * Wj);                                 //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            }else if(Physics2D.OverlapBox(ladoI.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                aux2D = rb.velocity;                                                                      
                if(aux2D.y <= -3f)                                                                      //Reinicia velocidad de caida a 3 si va a caer(Efecto deslizar pared)
                {
                    rb.velocity = new Vector2(aux2D.x,-3f);                                                   
                }

                if (Input.GetKeyDown(keys["Jump"]))                                                     
                {
                    aux2D = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux2D.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.right * Wj);                                //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            } else if (Input.GetKeyDown(keys["Jump"]) && jumpsN < jumpsMax)                             //Revisa limite de AirJumps
            {
                aux2D = rb.velocity;                                                                    //Reinicia velocidad vertical antes de saltar
                rb.velocity = new Vector2(aux2D.x,0f);                                                  //Reinicia velocidad vertical antes de saltar
                rb.AddForce(Vector3.up * jump);                                                         
                jumpsN++;
            }

            if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, wall) == null)                       //Evita aplicar fuerza contra un muro
            {                                                                                           
                if (Input.GetKey(keys["Right"]))                                                        //Movimiento Derecha
                {                                                                                       
                    rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);                            

                    if (rb.velocity.x >= speedLimit)                                                    //Limitacion de velocidad
                    {                                                                                   
                        aux2D = rb.velocity;                                                              
                        rb.velocity = new Vector2(speedLimit, aux2D.y);                                   
                    }                                                                                   
                }                                                                                       
            }else{
                aux2D = rb.velocity;                                                                    //Reinicio velocidad horizontal
                if(aux2D.x > 0){
                rb.velocity = new Vector2(0.0f, aux2D.y);
                }  
            }
            
            if (Physics2D.OverlapBox(ladoI.position, limLado, 0.0f, wall) == null)                      //Evita aplicar fuerza contra un muro
            {                                                                                           
                if (Input.GetKey(keys["Left"]))                                                         //Movimiento Izquierda
                {                                                                                       
                    rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);                             

                    if (rb.velocity.x <= speedLimit)                                                    //Limitacion de velocidad
                    {                                                                                   
                        aux2D = rb.velocity;                                                              
                        rb.velocity = new Vector2(-speedLimit, aux2D.y);                                  
                    }                                                                                   
                }                                                                                       
            }else{
                Vector2 aux2D = rb.velocity;                                                            //Reinicio velocidad horizontal
                if(aux2D.x < 0){
                rb.velocity = new Vector2(0.0f, aux2D.y);
                }  
            }                                                                                                                                                                                     

            //Reinicio velocidad eje X
            if (Input.GetKeyUp(keys["Right"]) || Input.GetKeyUp(keys["Left"])                           //Revision soltar teclas movimiento
            || (Input.GetKey(keys["Right"]) && Input.GetKey(keys["Left"])))                             //Revision pulsar simultaneas teclas movimiento
            {
                Vector2 aux2D = rb.velocity;                                                            //Reinicio velocidad horizontal
                rb.velocity = new Vector2(0.0f, aux2D.y);                                                 
            }
        }
    }

    IEnumerator WallJumpCD() {                                                                          //Cooldown control WallJump
        yield return new WaitForSeconds(0.2f);
        canControl = true;
    }
}
