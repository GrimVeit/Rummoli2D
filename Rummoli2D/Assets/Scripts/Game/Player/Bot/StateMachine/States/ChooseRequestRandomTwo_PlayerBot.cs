using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseRequestRandomTwo_PlayerBot : IState
{
    private readonly IStoreCardInfoProvider _storeCardInfoProvider;

    private const float DECISION_DELAY_MIN = 0.5f;
    private const float DECISION_DELAY_MAX = 1f;

    private IEnumerator timer;

    public ChooseRequestRandomTwo_PlayerBot(IStoreCardInfoProvider storeCardInfoProvider)
    {
        _storeCardInfoProvider = storeCardInfoProvider;
    }

    public void EnterState()
    {
        if (timer != null) Coroutines.Stop(timer);

        timer = ChooseRoutine();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    #region Output Events

    public event Action<ICard> OnCardLaid;
    public event Action OnPass;

    #endregion

    #region Internal Logic

    private IEnumerator ChooseRoutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(DECISION_DELAY_MIN, DECISION_DELAY_MAX));

        var twoCards = _storeCardInfoProvider.Cards
            .Where(c => c.CardRank == CardRank.Two)
            .ToList();

        if (twoCards.Count > 0)
        {
            var cardToPlay = twoCards[UnityEngine.Random.Range(0, twoCards.Count)];
            OnCardLaid?.Invoke(cardToPlay);
            Debug.Log($"[Bot] Laid random two: {cardToPlay.CardSuit} {cardToPlay.CardRank}");
        }
        else
        {
            OnPass?.Invoke();
            Debug.Log("[Bot] No two found, passing");
        }
    }

    #endregion
}
