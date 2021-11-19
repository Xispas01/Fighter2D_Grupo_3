using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    [System.Serializable]
public class SettingControl : MonoBehaviour
{

    public int j = 0;
    public bool contador = false;
    public int timeConfirm = 0;

    private List<int> widthList = new List<int>();
    private List<int> heightList = new List<int>();
    private List<int> refreshList = new List<int>();

    public TextMeshProUGUI timer;
    public TextMeshProUGUI resObj;


    public GameObject confirmOBJ;
    public GameObject optionsOBJ;


    public void zSetMusicV(Slider slide){
        SettingsSaving.musicV = slide.value;
    }

    public void zSetSfxV(Slider slide){
        SettingsSaving.sfxV = slide.value;
    }

    private void Start() {
        if (SettingsSaving.isFirstRun){
            Resolution[] resolutions = Screen.resolutions;

            // Print the resolutions
            foreach (var res in resolutions)
            {
                SettingsSaving.widthList.Add(res.width);
                SettingsSaving.heightList.Add(res.height);
                SettingsSaving.refreshList.Add(res.refreshRate);
            }

            SettingsSaving.i = SettingsSaving.widthList.Count - 1;

            SettingsSaving.width = SettingsSaving.widthList[SettingsSaving.i];
            SettingsSaving.height = SettingsSaving.heightList[SettingsSaving.i];

            resObj.text = SettingsSaving.widthList[SettingsSaving.i] + "x" + SettingsSaving.heightList[SettingsSaving.i];  
            Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen); 
        }
    }

    public void zSiguienteRes(){
        SettingsSaving.i++;
        if(SettingsSaving.i > SettingsSaving.widthList.Count - 1){
            SettingsSaving.i = 0;
        }
        resObj.text = SettingsSaving.widthList[SettingsSaving.i] + "x" + SettingsSaving.heightList[SettingsSaving.i];  
    }

    public void zAnteriorRes(){
        SettingsSaving.i--;
        if(SettingsSaving.i < 0){
            SettingsSaving.i = SettingsSaving.widthList.Count - 1;
        }
        resObj.text = SettingsSaving.widthList[SettingsSaving.i] + "x" + SettingsSaving.heightList[SettingsSaving.i];  
    }

    public void zApplyRes(int timeCon){
        Screen.SetResolution(SettingsSaving.widthList[SettingsSaving.i], SettingsSaving.heightList[SettingsSaving.i], SettingsSaving.fullscreen);
        timeConfirm = timeCon;
        contador = true;
        timer.text ="" + timeConfirm;   
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        StartCoroutine("ResCountDown");
    }

    private void Update() {
        if(contador == true){
            timer.text ="" + j;   
        }
    }

    private void ConfirmSpawn(GameObject target){
        target.SetActive(!target.activeSelf);
    }

    IEnumerator ResCountDown(){
        for(j = timeConfirm;j>=0;j--){
            yield return new WaitForSeconds(1f);
            if (j == 0){
                Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen);
                ConfirmSpawn(confirmOBJ);
                ConfirmSpawn(optionsOBJ);
                break;
            }
        }
    }

    public void zConfirmRes(){
        StopCoroutine("ResCountDown");        
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        contador = false;
        SettingsSaving.width = SettingsSaving.widthList[SettingsSaving.i];
        SettingsSaving.height = SettingsSaving.heightList[SettingsSaving.i];
    }

    public void zCancelRes(){
        StopCoroutine("ResCountDown");   
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        contador = false;
        Screen.SetResolution(SettingsSaving.width, SettingsSaving.height, SettingsSaving.fullscreen);
    }

    public void zSetToggle(Toggle t){
        if (SettingsSaving.fullscreen){
            t.isOn = true;
        }else{
            t.isOn = false;
        }
    }

    public void zSetFullScreen(Toggle t){
        if (t.isOn){
            SettingsSaving.fullscreen = true;
        }else{
            SettingsSaving.fullscreen = false;
        }
    }
}
