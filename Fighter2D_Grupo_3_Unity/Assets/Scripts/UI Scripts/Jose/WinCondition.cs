using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public static string win1 = "Player 1 Wins", win2 = "Player 2 Wins";
    public void InstanReMatch(){
        SettingsSaving.deathsP1 = 0;
        SettingsSaving.deathsP2 = 0;
       //MenuControl.zSceneChange("SmashMap"); comentado hasta que se solucione
    }

    public void Temporizador(int actualSecond){
        if(actualSecond <= 0){
            TimeMatchEnd();
        }else{
            StartCoroutine("CountDown");
        }
    }

    IEnumerator CountDown(){
        yield return new WaitForSeconds(1f);
        SettingsSaving.actualTime--;
        Temporizador(SettingsSaving.actualTime);
    }

    public void LivesMatchEnd(int victor){
        SettingsSaving.winner = victor;
        //MenuControl.zSceneChange("MatchEnd");
    }

    public void TimeMatchEnd(){
        int a;
    }

}
