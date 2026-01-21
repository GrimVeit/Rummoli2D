using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentationSystemModel
{
    private readonly ISoundProvider _soundProvider;

    public PlayerPresentationSystemModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void StartShow()
    {
        _soundProvider.PlayOneShot("ShowPlayer");
    }

    public void StartHide()
    {
        _soundProvider.PlayOneShot("HidePlayer");
    }
}
