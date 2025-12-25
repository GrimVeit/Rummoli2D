using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Choose5CardsState_PlayerBot : IState
{
    private readonly IStoreCardInfoProvider _storeCardInfoProvider;

    private List<ICard> _cards = new();

    private IEnumerator timer;

    public Choose5CardsState_PlayerBot(IStoreCardInfoProvider storeCardInfoProvider)
    {
        _storeCardInfoProvider = storeCardInfoProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _cards = _storeCardInfoProvider.Cards.Take(5).ToList();
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(Random.Range(3f, 20f));

        Choose();
    }


    #region Output

    public event Action<List<ICard>> OnChooseCards;

    private void Choose()
    {
        if (_cards.Count != 5) return;

        OnChooseCards?.Invoke(new List<ICard>(_cards));
        Debug.Log("FIVE CARDS!!!");
    }

    #endregion
}
