using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderToPercentage : MonoBehaviour
{
    private int v;

    public void zPercentageValue(Slider slide){
        v = (int) (slide.value * 100);
    }

    public void zPercentageString(TextMeshProUGUI per){
        per.text = v + "%";
    }
}
