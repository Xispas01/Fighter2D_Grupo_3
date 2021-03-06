//Por favor firmar las modificacions para saber a quien preguntar si se tienen dudas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSwapkey : MonoBehaviour
{
    //Xispas01
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

    //Otro
    public int Death_Count;                                                                             //Contador de muertes
    public Transform SpawnPoint;                                                                        //Coordenadas para Respawn
    /*Esto es para que el Death_Count cuente con cierto retraso**/
    private float LastRespawn = 0.0f;                                                                   
    private float NextRespawn = 0.1f;
    /*Necesidad de adaptar el código de animación del jugador a las necesidades del proyecto**/
    private Animator AnimatorLibro;
    private Animator AnimatorAlice;
    public GameObject libro;
    public GameObject alice;

    //Xispas01
    public Vector2 limSuelo = new Vector2(0.5f, 0f);                                                    //Tamaños de boxcast Techo/suelo
    public Vector2 limLado = new Vector2(0f, 0.5f);                                                     //Tamaños de boxcast Lados

    private int ground;                                                                                 //Mascaras para raycast
    private int wall;                                                                                   //Mascaras para raycast
    private int wallJump;                                                                               //Mascaras para raycast

    private bool canControl = true;                                                                     //Cooldown Control WallJump (Sirve para ser lanzado sin control)
    
    private SpriteRenderer sprite;  
    private SpriteRenderer sprite2;

    private Vector2 aux2D;                                                                              //Vector 2D auxiliar
    private Vector3 aux3D;                                                                              //Vector 3D auxiliar

    public Dictionary<string,KeyCode> inputs = new Dictionary<string, KeyCode>();                                //Diccionario para controles configurables

    public int player;

    private Rigidbody2D rb;                                                                             //CuerpoRigido (Fisicas basada en fuerza y gravedad)


    void Start()                                                                                        //Inicio Start()
    {                   
        rb = gameObject.GetComponent<Rigidbody2D>();           
        //Xispas01

        //Asignacion controles
        switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
            case 1:{
                sprite = alice.GetComponent<SpriteRenderer>(); 
                sprite2 = libro.GetComponent<SpriteRenderer>(); 
                AnimatorLibro = libro.GetComponent<Animator>();
                AnimatorAlice = alice.GetComponent<Animator>();

                inputs.Add("Left", SettingsSaving.keys["LeftA"]);
                inputs.Add("Right", SettingsSaving.keys["RightA"]);                                             
                inputs.Add("Jump", SettingsSaving.keys["JumpA"]);                                               
                break;
            }
            case 2:{
                inputs.Add("Left", SettingsSaving.keys["LeftB"]);                                               
                inputs.Add("Right", SettingsSaving.keys["RightB"]);                                             
                inputs.Add("Jump", SettingsSaving.keys["JumpB"]);                                               
                break;
            }
        }

        ground = LayerMask.GetMask("Terrain","WallJump","Empujable","Atravesable");                     //Asigna valor a las mascaras de RayCast
        wall = LayerMask.GetMask("Terrain","WallJump");                                                 //Asigna valor a las mascaras de RayCast
        wallJump = LayerMask.GetMask("WallJump");                                                       //Asigna valor a las mascaras de RayCast

        canControl = true;                                                                              //Inicializa a true
    }

    // Update is called once per frame
    void Update()                                                                                       //Inicio Update()
    {
        if (PauseMenu.IsPaused==false && canControl == true)                                            //Revision de pausa Y control
        {
            if(Input.GetKeyDown(KeyCode.Q)){
                switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
                    case 1:{                                            
                        break;
                    }
                    case 2:{                                           
                        break;
                    }
                }
                AnimatorAlice.SetBool("Heavy",true);
                AnimatorLibro.SetBool("heavy",true);
                StartCoroutine("cdHeavy");
            }
            if(Input.GetKeyDown(KeyCode.E)){
                switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
                    case 1:{                                            
                        break;
                    }
                    case 2:{                                           
                        break;
                    }
                }
                AnimatorAlice.SetBool("Normal",true);
                StartCoroutine("cdNormal");
            }

            if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, ground) == null )                   //Revisa BoxCast al Suelo
                    {
                        switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
                            case 1:{                                            
                                break;
                            }
                            case 2:{                                           
                                break;
                            }
                        }
                        AnimatorAlice.SetBool("Jump",true);
                    }
            if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, ground) != null )                   //Revisa BoxCast al Suelo
            {
                AnimatorAlice.SetBool("Jump",false);
                jumpsN = 0;                                                                             //Reinicia AirJumps
                if (Input.GetKeyDown(inputs["Jump"]))                                                     
                {
                    PlaySFX("JumpSFX");
                    rb.AddForce(Vector3.up * jump);                                                     
                }
            }else if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                if(!sprite.flipX){                                                                      //Flipea el personaje a la direccion contraria de la pared en la que se desliza
                    sprite.flipX = true;
                } 

                aux2D = rb.velocity;                                                                      
                if(aux2D.y <= -3f)                                                                      //Reinicia velocidad de caida a 3 si va a caer(Efecto deslizar pared)
                {
                rb.velocity = new Vector2(aux2D.x,-3f);                                                   
                }

                if (Input.GetKeyDown(inputs["Jump"]))                                                     
                {
                    PlaySFX("WallJumpSFX");
                    aux2D = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux2D.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.left * Wj);                                 //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            }else if(Physics2D.OverlapBox(ladoI.position, limLado, 0.0f, wallJump) != null)             //Revisa BoxCast a muro Walljump
            {
                if(sprite.flipX){                                                                       //Flipea el personaje a la direccion contraria de la pared en la que se desliza
                    sprite.flipX = false;
                } 

                aux2D = rb.velocity;                                                                      
                if(aux2D.y <= -3f)                                                                      //Reinicia velocidad de caida a 3 si va a caer(Efecto deslizar pared)
                {
                    rb.velocity = new Vector2(aux2D.x,-3f);                                                   
                }

                if (Input.GetKeyDown(inputs["Jump"]))                                                     
                {
                    PlaySFX("WallJumpSFX");
                    aux2D = rb.velocity;                                                                  
                    rb.velocity = new Vector2(aux2D.x,0f);                                                
                    rb.AddForce(Vector3.up * jump + Vector3.right * Wj);                                //Salta con fuerza lateral extra
                    canControl = false;                                                                
                    StartCoroutine("WallJumpCD");                                                       //Inicia cooldown de WallJump
                }
            } else if (Input.GetKeyDown(inputs["Jump"]) && jumpsN < jumpsMax)                             //Revisa limite de AirJumps
            {
                PlaySFX("AirJumpSFX");
                aux2D = rb.velocity;                                                                    //Reinicia velocidad vertical antes de saltar
                rb.velocity = new Vector2(aux2D.x,0f);                                                  //Reinicia velocidad vertical antes de saltar
                rb.AddForce(Vector3.up * jump);                                                         
                jumpsN++;
            }

            if(Physics2D.OverlapBox(ladoD.position, limLado, 0.0f, wall) == null)                       //Evita aplicar fuerza contra un muro
            {                                                                                           
                if (Input.GetKey(inputs["Right"]))                                                        //Movimiento Derecha
                {        
                    if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, ground) != null )                   //Revisa BoxCast al Suelo
                    {
                        AnimatorAlice.SetBool("Run",true);
                        AnimatorLibro.SetBool("run",true);
                    }                                                                         
                    rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
                    
                    if(sprite.flipX){                                                                   //Flipea el personaje a la direccion que va a moverse
                        sprite2.flipX = false;
                        sprite.flipX = false;
                    }

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
                if (Input.GetKey(inputs["Left"]))                                                         //Movimiento Izquierda
                {           
                    if (Physics2D.OverlapBox(pies.position, limSuelo, 0.0f, ground) != null )                   //Revisa BoxCast al Suelo
                    {
                        AnimatorAlice.SetBool("Run",true);
                        AnimatorLibro.SetBool("run",true);
                    }
                    rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);     

                    if(!sprite.flipX){                                                                  //Flipea el personaje a la direccion que va a moverse
                        sprite2.flipX = true;
                        sprite.flipX = true;
                    }                      

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
            if (Input.GetKeyUp(inputs["Right"]) || Input.GetKeyUp(inputs["Left"])                           //Revision soltar teclas movimiento
            || (Input.GetKey(inputs["Right"]) && Input.GetKey(inputs["Left"])))                             //Revision pulsar simultaneas teclas movimiento
            {
                
                AnimatorAlice.SetBool("Run",false);
                AnimatorLibro.SetBool("run",false);
                Vector2 aux2D = rb.velocity;                                                            //Reinicio velocidad horizontal
                rb.velocity = new Vector2(0.0f, aux2D.y);                                                 
            }
        }
    }

    private void PlaySFX(string name){
        float v = SettingsSaving.sfxV;
        FindObjectOfType<AudioManager>().Play(name, v);
    }

    IEnumerator WallJumpCD() {                                                                          //Cooldown control WallJump
        yield return new WaitForSeconds(0.2f);
        canControl = true;
    }

    IEnumerator cdHeavy() {                                                                          //Cooldown control WallJump
        yield return new WaitForSeconds(0.1f);
        AnimatorAlice.SetBool("Heavy",false);
        AnimatorLibro.SetBool("heavy",false);
    }
    IEnumerator cdNormal() {                                                                          //Cooldown control WallJump
        yield return new WaitForSeconds(0.1f);
        AnimatorAlice.SetBool("Normal",false);
    }

    //otro
    /**Esto requiere poner el tag "Respawn" en la capa donde esté las colisiones*/
    public void OnTriggerEnter2D(Collider2D collision) {                                                //Colisión con las paredes o el suelo
        if (collision.CompareTag("Respawn") && Time.time > LastRespawn) {
            LastRespawn = Time.time + NextRespawn;
            Death_Count++;
            transform.position = SpawnPoint.position ;
            switch(player){//especificacion de muertes en otro script para cada player
                case 1:{
                    SettingsSaving.deathsP1++;
                    if(SettingsSaving.deathsP1 == SettingsSaving.lives){
                       WinCondition.LivesMatchEnd(2);
                    }
                    break;
                }
                case 2:{
                    SettingsSaving.deathsP2++;
                    if(SettingsSaving.deathsP1 == SettingsSaving.lives){
                       WinCondition.LivesMatchEnd(1);
                    }
                    break;
                }
            }
        }
    }
   
}
