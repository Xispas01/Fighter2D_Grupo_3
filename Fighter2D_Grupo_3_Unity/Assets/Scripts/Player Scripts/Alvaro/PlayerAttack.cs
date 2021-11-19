using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D[] attackHitboxes;

    public GameObject shield;
    public GameObject hurtBox;

    public SpriteRenderer shieldSpriteRenderer;

    public float attackDamage; //Da�o que a�ade el ataque al jugador cuando lo golpea
    public float attackPushForceBase; //Empuje base que tiene el ataque

    public float storedDamage;
    public float pushForce;

    public GameObject objective; //Objetivo del ataque

    public Rigidbody2D objetiveRigidbody;

    private AttackProperties attackProperties;

    private DamageStore damageStore;


    // Update is called once per frame
    private void Start()
    {
        shieldSpriteRenderer = shield.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //detalle condicionar todo a la variable pausa y si puede controlar su personaje Xispas01
        if(Input.GetKeyDown(KeyCode.F))
        {
            //puedes usar la variable booleana del sprite renderer del personage para decidir si usar la hitbox de la derecha o la izquierda Xispas01

            /*fuera de todos los metodos
                private SpriteRenderer sprite;
            */
            
            /*en el metodo start
                sprite = gameObject.GetComponent<SpriteRenderer>(); 
            */
            
            /*revision si esta flipeado haz x si no y
                if(sprite.flipX){
                    x;
                } else{
                    y;
                }
            */

            
            //como opcion para cargar aun menos puedes cambiar la posicion de las hitbox en si ya que si las incorporas en un objeto vacio que sea child del jugador la posicion es relativa al centro del parent Xispas01
            LaunchAttack(attackHitboxes[0]);
            LaunchAttack(attackHitboxes[1]);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LaunchAttack(attackHitboxes[2]);
            LaunchAttack(attackHitboxes[3]);
        }
        if (Input.GetKey(KeyCode.C))
        {
            LaunchDefense();
            Debug.Log("PULSANDO ESCUDO");

        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            hurtBox.layer = 6;
            shieldSpriteRenderer.enabled = false;
        }

    }
    private void LaunchAttack(Collider2D col/*, int direccion*/)  /*se le podria añadir un int para multiplicar el daño y decidir la direccion (1 si lanza a la derecha y -1 si lanza a la izquierda 
                                                                    asi solo hay que diseñar el lanzamiento a la derecha) Xispas01*/
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size, 0.0f, LayerMask.GetMask("Hurtbox"));
        foreach(Collider2D c in cols)
        {
            objective = c.transform.parent.parent.gameObject;

            if (objective == transform) //Si el padre del padre del collider es el propio jugador
            {
                continue; //Como un break pero evalua el siguiente c en el foreach
            }
            //Si no es parte del propio cuerpo
            Debug.Log("ATAQUE GOLPEA");
            
            //esto recoge las propiedades del ataque asignado al enemigo o me estoy colando yo? Xispas01
            attackProperties = col.gameObject.GetComponent<AttackProperties>();
            attackDamage = attackProperties.attackDamage;
            attackPushForceBase = attackProperties.attackPushForceBase;

            damageStore = objective.GetComponent<DamageStore>();
            damageStore.StoredDamage = damageStore.StoredDamage + attackDamage; //A�ade al da�o almacenado por el enemigo el da�o de nuestro ataque

            pushForce = CalculatePushForce( attackDamage, attackPushForceBase, damageStore); //Calcula el empuje que provoca nuestro ataque despu�s de a�adir el da�o
            objetiveRigidbody = objective.GetComponent<Rigidbody2D>();
            Vector2 hitAngle = GetHitAngle(col, objetiveRigidbody);//Se podrian fijar angulos para los ataques asi no hace falta calcularlos Xispas01
            ApplyPushForce(objetiveRigidbody, hitAngle, pushForce);

        }
    }
    private void LaunchDefense( )
    {
        hurtBox.layer = 16; //Shielded 
        shieldSpriteRenderer.enabled = true;
    }

    private float CalculatePushForce(float attackDamage, float attackPushForceBase, DamageStore damageStore)
    {
        float pushForce = attackPushForceBase + damageStore.Lightness;
        return pushForce;
    }

    private Vector2 GetHitAngle(Collider2D attackCol, Rigidbody2D objectiveRigidbody)
    {
        Vector2 attackCenter = attackCol.transform.position;
        Vector2 objectiveCenter = objectiveRigidbody.transform.position;
        float hitAngleDegree = Vector2.SignedAngle(attackCenter, objectiveCenter);
        return new Vector2(Mathf.Cos(hitAngleDegree * Mathf.Deg2Rad), Mathf.Sin(hitAngleDegree * Mathf.Deg2Rad));
    }

    private void ApplyPushForce(Rigidbody2D objetiveRigidbody, Vector2 hitAngle, float pushForce)
    {
        objetiveRigidbody.AddForce(hitAngle * pushForce, ForceMode2D.Impulse);
    }
}