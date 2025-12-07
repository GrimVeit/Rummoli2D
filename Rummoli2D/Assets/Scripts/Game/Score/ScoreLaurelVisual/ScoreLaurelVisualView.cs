using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLaurelVisualView : View
{
    [SerializeField] private TextMeshProUGUI textLaurel;

    public void SetLaurel(int laurel)
    {
        textLaurel.text = $"×{laurel}";
    }
}
