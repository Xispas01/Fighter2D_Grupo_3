using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderToPercentage : MonoBehaviour
{
    private int v;

    public void PercentageValue(Slider slide){
        v = (int) (slide.value * 100);
    }

    public void PercentageString(TextMeshProUGUI per){
        per.text = v + "%";
    }
}
