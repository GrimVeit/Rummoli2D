using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesVisualModel
{
    private readonly ISoundProvider _soundProvider;

    public RulesVisualModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void ClickLeftRight()
    {
        _soundProvider.PlayOneShot("Click");
    }
}
