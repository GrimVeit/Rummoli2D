using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankGameView : View
{
    [SerializeField] private TextMeshProUGUI textMoney;

    public void SetMoney(int count)
    {
        textMoney.text = $"You have earned {count} coins";
    }
}
