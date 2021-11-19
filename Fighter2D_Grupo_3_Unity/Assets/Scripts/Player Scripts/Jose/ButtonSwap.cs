using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSwap : MonoBehaviour
{
    public TextMeshProUGUI JumpP1, LeftP1, RightP1;
    public TextMeshProUGUI JumpP2, LeftP2, RightP2;

    private GameObject currentKey;


    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);


    // Use this for initialization
    void Start () {
        if (SettingsSaving.isFirstRun){
            SettingsSaving.keys.Add("JumpA", KeyCode.W);
            SettingsSaving.keys.Add("LeftA", KeyCode.A);
            SettingsSaving.keys.Add("RightA", KeyCode.D);

            SettingsSaving.keys.Add("JumpB", KeyCode.UpArrow);
            SettingsSaving.keys.Add("LeftB", KeyCode.LeftArrow);
            SettingsSaving.keys.Add("RightB", KeyCode.RightArrow);
        }

        JumpP1.text = SettingsSaving.keys["JumpA"].ToString();
        LeftP1.text = SettingsSaving.keys["LeftA"].ToString();
        RightP1.text = SettingsSaving.keys["RightA"].ToString();

        JumpP2.text = SettingsSaving.keys["JumpB"].ToString();
        LeftP2.text = SettingsSaving.keys["LeftB"].ToString();
        RightP2.text = SettingsSaving.keys["RightB"].ToString();
        
    }
    
    
    public void defaultButton()
    {

        SettingsSaving.keys["JumpA"] = KeyCode.W;
        SettingsSaving.keys["LeftA"] = KeyCode.A;
        SettingsSaving.keys["RightA"] = KeyCode.D;
        
        SettingsSaving.keys["JumpB"] = KeyCode.UpArrow;
        SettingsSaving.keys["LeftB"] = KeyCode.LeftArrow;
        SettingsSaving.keys["RightB"] = KeyCode.RightArrow;


        JumpP1.text = SettingsSaving.keys["JumpA"].ToString();
        LeftP1.text = SettingsSaving.keys["LeftA"].ToString();
        RightP1.text = SettingsSaving.keys["RightA"].ToString();
        
        JumpP2.text = SettingsSaving.keys["JumpB"].ToString();
        LeftP2.text = SettingsSaving.keys["LeftB"].ToString();
        RightP2.text = SettingsSaving.keys["RightB"].ToString();

    }

    void OnGUI(){
        if(currentKey != null){
            Event e = Event.current;
            if(e.isKey){
                bool used = false;
                foreach (var k in SettingsSaving.keys.Values){//Revision repeticion de input
                    if(k == e.keyCode){
                        used = true;
                    }
                }
                if (used == false){//Sistema anti repeticion de input
                    SettingsSaving.keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                    currentKey.GetComponent<Image>().color = normal;
                    currentKey = null;
                }else{
                    SettingsSaving.keys[currentKey.name] = SettingsSaving.keys[currentKey.name];
                    currentKey.GetComponent<Image>().color = normal;
                    currentKey = null;
                }
            }
        }
    }

    public void ChangeKey(GameObject clicked){
        if(currentKey != null){
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }
}