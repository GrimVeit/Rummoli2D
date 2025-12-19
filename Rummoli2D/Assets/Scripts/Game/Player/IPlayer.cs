using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Id { get; }

    void ActivateApplyBet();
    void DeactivateApplyBet();


    event Action OnApplyBet;
}
