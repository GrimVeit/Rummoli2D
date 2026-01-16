using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddCoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoins;

    public void SetCoins(int coins)
    {
        textCoins.text = $"+{coins}";
    }
}
