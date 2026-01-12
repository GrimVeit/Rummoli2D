using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBotModel
{
    public void SetAvatarIndex(int index)
    {
        OnSetAvatarIndex?.Invoke(index);
    }

    public event Action<int> OnSetAvatarIndex;
}
