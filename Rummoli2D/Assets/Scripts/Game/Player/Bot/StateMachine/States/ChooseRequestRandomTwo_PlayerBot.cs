using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ChooseRequestRandomTwo_PlayerBot : IState
{
    private readonly IStoreCardInfoProvider _storeCardInfoProvider;
    private readonly IStoreGameDifficultyInfoProvider _difficultyProvider;

    private const float DECISION_DELAY_MIN = 0.5f;
    private const float DECISION_DELAY_MAX = 1f;

    private IEnumerator timer;

    public ChooseRequestRandomTwo_PlayerBot(
        IStoreCardInfoProvider storeCardInfoProvider,
        IStoreGameDifficultyInfoProvider difficultyProvider)
    {
        _storeCardInfoProvider = storeCardInfoProvider;
        _difficultyProvider = difficultyProvider;
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

    private IEnumerator ChooseRoutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(DECISION_DELAY_MIN, DECISION_DELAY_MAX));

        var twoCards = _storeCardInfoProvider.Cards
            .Where(c => c.CardRank == CardRank.Two)
            .ToList();

        if (twoCards.Count == 0 || ShouldMakeMistake())
        {
            OnPass?.Invoke();
            Debug.Log("[Bot] No two found or passed intentionally");
            yield break;
        }

        var cardToPlay = twoCards[UnityEngine.Random.Range(0, twoCards.Count)];
        OnCardLaid?.Invoke(cardToPlay);
        Debug.Log($"[Bot] Laid random two: {cardToPlay.CardSuit} {cardToPlay.CardRank}");
    }

    private bool ShouldMakeMistake()
    {
        float mistakeChance = _difficultyProvider.GameDifficulty switch
        {
            GameDifficulty.Easy => 0.30f,
            GameDifficulty.Medium => 0.15f,
            GameDifficulty.Hard => 0f,
            _ => 0f
        };

        return UnityEngine.Random.value < mistakeChance;
    }
}
