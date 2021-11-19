using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStore : MonoBehaviour
{
    [SerializeField] float storedDamage;
    public int storedDamageLimit;
    public float lightness; //(Ligereza) Lightness es un multiplicador para calcular cuanto te mueve un ataque
    [SerializeField] float lightnessBase; //El m�nimo de este multiplicador: Te�ricamente, el valor de Lightness para un 0% de da�oAcumulado
    [SerializeField] float storedDamageInfluenceOnLightness; //El multiplicador usado a la hora de calcular la Lightness del personaje con respecto a cuanto da�o ha acumulado
    //�Por que usamos getset y Serializefield en vez de ponerlas en public? Para asegurar que se ejecuta el c�digo de los getset ANTES de obtener los valores de storedDamage y lightness

    public float StoredDamage //Siempre que se quiere obtener el da�o almacenado por el jugador se devuelve normalizado para que no sobrepase el l�mite
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

    public float Lightness //Siempre que se quiere obtener la ligereza del jugador se devuelve normalizada para que no baje del l�mite
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

    public void UpdateLightnessToStoredDamage() //Actualiza la Lightness del jugador al da�o acumulado que tiene
    {
        lightness = lightnessBase + storedDamage * storedDamageInfluenceOnLightness;
    }

    // Start is called before the first frame update
    void Start()
    {
        storedDamage = 0;
        lightness = lightnessBase;
    }


/*si no se usa se puede eliminar Xispas01

    // Update is called once per frame
    void Update()
    {

    }

*/


}
