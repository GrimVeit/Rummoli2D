using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NicknameBotView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private TextMeshProUGUI textNickname;

    public void SetNickname(string nickname)
    {
        textNickname.text = nickname;
    }
}
