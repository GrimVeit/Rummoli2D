using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundNumberVisualView : View
{
    [SerializeField] private TextMeshProUGUI textRoundOpen;
    [SerializeField] private TextMeshProUGUI textRoundCompleted;

    public void SetNameOpenRound(string name)
    {
        textRoundOpen.text = name;
    }

    public void SetNameCompletedRound(string name)
    {
        textRoundCompleted.text = name;
    }
}
