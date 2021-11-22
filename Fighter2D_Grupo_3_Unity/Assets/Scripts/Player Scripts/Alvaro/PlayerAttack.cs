using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D[] attackHitboxes;

    public GameObject shield;
    public GameObject hurtBox;

    public SpriteRenderer shieldSpriteRenderer;
    public SpriteRenderer playerSpriteRenderer;

    public float attackDamage; //Da�o que a�ade el ataque al jugador cuando lo golpea
    public float attackPushForceBase; //Empuje base que tiene el ataque

    public float storedDamage;
    public float pushForce;

    public GameObject objective; //Objetivo del ataque

    public Rigidbody2D objetiveRigidbody;

    private AttackProperties attackProperties;

    private DamageStore damageStore;

    private Vector2 attackDirectionPreset;

    public bool canAttack;


    // Update is called once per frame
    private void Start()
    {
        shieldSpriteRenderer = shield.GetComponent<SpriteRenderer>();
        playerSpriteRenderer = transform.parent.parent.gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        while (canAttack == true)
        {
            //detalle condicionar todo a la variable pausa y si puede controlar su personaje Xispas01
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("PULSANDO DEBIL");
                if (playerSpriteRenderer.flipX == false)
                { attackDirectionPreset = new Vector2(1.0f, 0.0f); LaunchAttack(attackHitboxes[0]); }
                else
                { attackDirectionPreset = new Vector2(-1.0f, 0.0f); LaunchAttack(attackHitboxes[1]); }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("PULSANDO FUERTE");
                if (playerSpriteRenderer.flipX == false)
                { attackDirectionPreset = new Vector2(1.0f, 0.0f); LaunchAttack(attackHitboxes[2]); }
                else
                { attackDirectionPreset = new Vector2(-1.0f, 0.0f); LaunchAttack(attackHitboxes[3]); }
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

    }
    private void LaunchAttack(Collider2D col/*, int direccion*/)
    {
        canAttack = false;
        Debug.Log("Implementando Launchattack");
        Collider2D[] cols = Physics2D.OverlapBoxAll(col.bounds.center, col.bounds.size, 0.0f, LayerMask.GetMask("Hurtbox"));
        foreach(Collider2D c in cols)
        {
            Debug.Log("Collider contactado");
            objective = c.transform.parent.parent.gameObject;
            Debug.Log(objective.ToString());

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
            /*Vector2 hitAngle = GetHitAngle(col, objetiveRigidbody);//Se podrian fijar angulos para los ataques asi no hace falta calcularlos Xispas01*/
            ApplyPushForce(objetiveRigidbody, pushForce, attackDirectionPreset);
            

        }
        canAttack = true;
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

    /*private Vector2 GetHitAngle(Collider2D attackCol, Rigidbody2D objectiveRigidbody)
    {
        Vector2 attackCenter = attackCol.transform.position;
        Vector2 objectiveCenter = objectiveRigidbody.transform.position;
        float hitAngleDegree = Vector2.SignedAngle(attackCenter, objectiveCenter);
        Vector2 result = new Vector2(Mathf.Cos(hitAngleDegree * Mathf.Deg2Rad), Mathf.Sin(hitAngleDegree * Mathf.Deg2Rad));
        Debug.Log(result.ToString());
        return result;
    }*/

    private void ApplyPushForce(Rigidbody2D ObjectiveRigidbody, float pushForce, Vector2 attackDirectionPreset)
    {
        ObjectiveRigidbody.AddForce(attackDirectionPreset * pushForce, ForceMode2D.Impulse);
        /*ObjectiveRigidbody.transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;*/
    }
}