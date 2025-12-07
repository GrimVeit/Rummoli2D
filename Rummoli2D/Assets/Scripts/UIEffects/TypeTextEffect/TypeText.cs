using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeText : UIEffect
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string textDescription;
    [SerializeField] private float timePause;

    public TextMeshProUGUI TextComponent => textComponent;
    public string TextDescription => textDescription;
    public float TimePause => timePause;

    public void SetTextDescription(string textDescription)
    {
        this.textDescription = textDescription;
    }
}
