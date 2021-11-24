//Para añadir controles configurables 
/*usar codigo usual*/
/*DESTACAR  El script SettingsSaving existe con la unica finalidad de almacenar cualquier tipo de informacion configurable entre diversas escenas (para eso se utilizan variables static)*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    /*declarar diccionario para controles*/
    //Dictionary<x,y> z
    //Diccionario nombre "z" para valor de tipo "x" le corresponde 1 UNICO valor de tipo "y"
    //diferentes x pueden resultar en la misma y
    public Dictionary<string,KeyCode> inputs = new Dictionary<string, KeyCode>();    

    /*declarar identificador de jugador (1, 2, 3, etc)*/
    //configurar desde el inspector de unity
    public int player;
    //configurar desde el propio script si el parent ya tiene el otro script PlayerMovementSwapkey
    //Otra opcion es fusionar este codigo con el script PlayerMovementSwapkey esta opcion es mas recomendable

    void Start()
    {
        /*Asignacion de palabras clave a KeyCode segun jugador*/
        //Asignacion controles
        switch(player){                                                                                 //Asigna las letras configuradas para cada player a cada accion
            case 1:{
                /*añade la palabra "Left" con el KeyCode corespondiente a la palabra "LeftA" del script SettingsSaving

                se asigna "Left" en este nuevo diccionario en lugar de usar el diccionario del script SettingsSaving para generalizar. 
                Ya que para el jugador 1 se le añade A a la palabra comando (en este ejemplo "Left") y para el jugador 2 se le añade B*/

                //aplicable a cualquier palabra comando. revisar en el script ButtonSwap cual es la palabra que se utiliza en SettingsSaving
                inputs.Add("Left", SettingsSaving.keys["LeftA"]);
                break;
            }
            case 2:{
                inputs.Add("Left", SettingsSaving.keys["LeftB"]);
                break;
            }
        }
    }


    void Update()
    {
        /*usar como de normal
        if (Input.GetKeyDown(x)){y}
        en la posicion de "x" colocar
        NombreDiccionario["PalabraComando"]
        y en "y"
        las acciones a realizar
        */
        if(Input.GetKeyDown(inputs["Left"])){
            Debug.Log("Pulsado (Left)");
        }
    }
}