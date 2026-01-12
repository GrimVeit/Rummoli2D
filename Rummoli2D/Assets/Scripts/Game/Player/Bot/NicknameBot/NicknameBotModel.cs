using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameBotModel
{
    public string Nickname => _nickname;

    private string _nickname = "Bot";

    public void SetNickname(string nickname)
    {
        _nickname = nickname;

        OnSetNickname?.Invoke(_nickname);
    }

    #region Output

    public event Action<string> OnSetNickname;

    #endregion
}
