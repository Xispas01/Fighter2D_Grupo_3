using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    int d1;
    int d2;
    int live;
    bool time;
    // Start is called before the first frame update
    void Start()
    {
        time = false;
        live = SettingsSaving.lives;
        SettingsSaving.actualTime = SettingsSaving.gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        d1 = SettingsSaving.deathsP1;
        d2 = SettingsSaving.deathsP2;
        if(SettingsSaving.actualTime == 0){
            time = true;
        }
        if(d1 == live){
            SettingsSaving.winner = 2;
        }
        if(d2 == live){
            SettingsSaving.winner = 1;
        }
        if(time && (d1 != d2)){
            if(d1 < d2){
                SettingsSaving.winner = 1;
            }else{
                SettingsSaving.winner = 2;
            }
        }else if(time){
            //inicio ronda final
        }
    }
}
