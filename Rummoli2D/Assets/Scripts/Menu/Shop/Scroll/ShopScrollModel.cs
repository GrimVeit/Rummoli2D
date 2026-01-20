using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScrollModel
{
    private readonly ISoundProvider _soundProvider;

    public ShopScrollModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void ClickLeftRight()
    {
        _soundProvider.PlayOneShot("Click");
    }
}
