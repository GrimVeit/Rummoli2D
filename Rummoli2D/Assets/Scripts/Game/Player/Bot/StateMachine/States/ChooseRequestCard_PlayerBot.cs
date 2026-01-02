using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRequestCard_PlayerBot : IState
{
    private ICard _currentChooseCard = null;
    private CardData _currentCardData;

    private IEnumerator timer;

    public ChooseRequestCard_PlayerBot()
    {

    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        Pass();
    }

    public void SetCardData(CardData cardData)
    {
        _currentCardData = cardData;
    }

    #region Output

    public event Action<ICard> OnCardLaid;
    public event Action OnPass;

    private void Choose()
    {
        OnCardLaid?.Invoke(_currentChooseCard);
        Debug.Log("CHOOSE CARD!!!");
    }

    private void Pass()
    {
        OnPass?.Invoke();
        Debug.Log("PASS!!!");
    }

    #endregion
}
