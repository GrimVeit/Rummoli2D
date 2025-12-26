using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerPoker : MonoBehaviour
{
    public PlayerPokerData Data => _data;

    [SerializeField] private Transform transformLine;
    [SerializeField] private TypeTextEffect textEffectNickname;
    [SerializeField] private TypeTextEffect textEffectHandRank;
    [SerializeField] private List<PlayerPokerCard> cards = new();

    private PlayerPokerData _data;

    private IEnumerator _timerSetData;
    private IEnumerator _timerShow;

    public void Initialize()
    {
        textEffectNickname.Initialize();
        textEffectHandRank.Initialize();
    }

    public void Dispose()
    {
        textEffectNickname.Dispose();
        textEffectHandRank.Dispose();
    }

    public void SetData(PlayerPokerData data)
    {
        _data = data;

        if (_timerSetData != null) Coroutines.Stop(_timerSetData);

        _timerSetData = TimerSetData(data);
        Coroutines.Start(_timerSetData);
    }

    private IEnumerator TimerSetData(PlayerPokerData data)
    {
        textEffectNickname.SetText(data.Nickname);
        textEffectHandRank.SetText(data.HandRankName);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetData(data.Cards[i].SpriteCover, data.Cards[i].SpriteFace);
        }

        textEffectNickname.ActivateEffect();

        for (int i = 0; i < cards.Count; i++)
        {
            yield return new WaitForSeconds(0.1f);

            cards[i].Show();
        }
    }

    public void Select()
    {
        transformLine.DOScaleX(1, 0.2f);
    }

    public void Show()
    {
        if (_timerShow != null) Coroutines.Stop(_timerShow);

        _timerShow = TimerShow();
        Coroutines.Start(_timerShow);
    }

    private IEnumerator TimerShow()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].FlipCard();

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.4f);

        textEffectHandRank.ActivateEffect();
    }
}
