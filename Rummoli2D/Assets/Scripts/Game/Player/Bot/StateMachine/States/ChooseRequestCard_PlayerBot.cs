using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChooseRequestCard_PlayerBot : IState
{
    private readonly IStoreCardInfoProvider _storeCardInfoProvider;
    
    private CardData _currentCardData;

    private const float DECISION_DELAY_MIN = 0.5f;
    private const float DECISION_DELAY_MAX = 1f;

    private IEnumerator timer;

    public ChooseRequestCard_PlayerBot(IStoreCardInfoProvider storeCardInfoProvider)
    {
        _storeCardInfoProvider = storeCardInfoProvider;
    }

    public void EnterState()
    {
        if(timer != null) Coroutines.Stop(timer);

        timer = ChooseRoutine();
        Coroutines.Start(timer);
    }

    public void ExitState()
    {
        if (timer != null) Coroutines.Stop(timer);
    }

    public void SetCardData(CardData cardData)
    {
        _currentCardData = cardData;
    }

    #region Output Events

    public event Action<ICard> OnCardLaid;
    public event Action OnPass;

    #endregion

    #region Internal Logic

    private IEnumerator ChooseRoutine()
    {
        yield return new WaitForSeconds(Random.Range(DECISION_DELAY_MIN, DECISION_DELAY_MAX));

        if (_currentCardData == null)
        {
            OnPass?.Invoke();
            Debug.Log("[Bot] No current card, passing");
            yield break;
        }


        var cardToPlay = _storeCardInfoProvider.Cards
            .FirstOrDefault(c => c.CardSuit == _currentCardData.Suit && c.CardRank == _currentCardData.Rank);

        if (cardToPlay != null)
        {
            OnCardLaid?.Invoke(cardToPlay);
            Debug.Log($"[Bot] Laid card: {_currentCardData.Suit} {_currentCardData.Rank}");
        }
        else
        {
            OnPass?.Invoke();
            Debug.Log($"[Bot] Does not have card {_currentCardData.Suit} {_currentCardData.Rank}, passing");
        }
    }

    #endregion
}
