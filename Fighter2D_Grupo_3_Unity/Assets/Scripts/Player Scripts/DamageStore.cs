using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStore : MonoBehaviour
{

    [SerializeField] float storedDamage;
    public int storedDamageLimit;
    public float lightness; //(Ligereza) Lightness es un multiplicador para calcular cuanto te mueve un ataque
    [SerializeField] float lightnessBase; //El mínimo de este multiplicador: Teóricamente, el valor de Lightness para un 0% de dañoAcumulado
    [SerializeField] float storedDamageInfluenceOnLightness; //El multiplicador usado a la hora de calcular la Lightness del personaje con respecto a cuanto daño ha acumulado
    //¿Por que usamos getset y Serializefield en vez de ponerlas en public? Para asegurar que se ejecuta el código de los getset ANTES de obtener los valores de storedDamage y lightness

    public float StoredDamage //Siempre que se quiere obtener el daño almacenado por el jugador se devuelve normalizado para que no sobrepase el límite
    {
        get
        {
            if (storedDamage > storedDamageLimit)
            {
                storedDamage = storedDamageLimit;
            }
            return storedDamage;
        }
        set 
        {
            storedDamage = value;
            if (storedDamage > storedDamageLimit)
            {
                storedDamage = storedDamageLimit;
            }
        }
    }

    public float Lightness //Siempre que se quiere obtener la ligereza del jugador se devuelve normalizada para que no baje del límite
    {
        get 
        {
            UpdateLightnessToStoredDamage();
            if (lightness < lightnessBase)
            {
                lightness = lightnessBase;
            }
            return lightness;
        }
        set 
        {
            if (lightness < lightnessBase)
            {
                lightness = lightnessBase;
            }
            lightness = value; 
        }
    }

    public void UpdateLightnessToStoredDamage() //Actualiza la Lightness del jugador al daño acumulado que tiene
    {
        lightness = lightnessBase + storedDamage * storedDamageInfluenceOnLightness;
    }

    // Start is called before the first frame update
    void Start()
    {
        storedDamage = 0;
        lightness = lightnessBase;
    }

    // Update is called once per frame
    void Update()
    {

    }



}
