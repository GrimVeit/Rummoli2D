using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorCounterView : View
{
    [SerializeField] private TextMeshProUGUI textCount;

    public void SetCount(int count)
    {
        textCount.text = $"{count}<color=white>/99</color>";
    }
}
