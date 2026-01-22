using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeopleInputModel
{
    private readonly ISoundProvider _soundProvider;

    public PlayerPeopleInputModel(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Pass()
    {
        _soundProvider.PlayOneShot("Click");

        OnPass?.Invoke();
    }

    public void Choose()
    {
        _soundProvider.PlayOneShot("DealCard");

        OnChoose?.Invoke();
    }

    #region Output

    public event Action OnChoose;
    public event Action OnPass;

    #endregion
}
