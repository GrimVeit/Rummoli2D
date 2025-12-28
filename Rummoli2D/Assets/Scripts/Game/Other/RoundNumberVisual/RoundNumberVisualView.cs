using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundNumberVisualView : View
{
    [SerializeField] private TextMeshProUGUI textRound;

    public void SetName(string name)
    {
        textRound.name = name;
    }
}
