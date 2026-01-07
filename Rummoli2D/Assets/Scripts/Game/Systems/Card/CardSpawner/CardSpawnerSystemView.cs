using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSpawnerSystemView : View
{
    [SerializeField] private CardSpawnerMove cardSpawnerMovePrefab;
    [SerializeField] private RectTransform transformSpawnParent;
    [SerializeField] private RectTransform transformFrom;
    [SerializeField] private CardSpawnerTargets cardSpawnerTargets;
    [SerializeField] private Canvas canvas;

    [SerializeField] private TextMeshProUGUI textCardsCount;

    public void SpawnCard(int playerIndex, ICard card)
    {
        var chip = Instantiate(cardSpawnerMovePrefab, transformSpawnParent);
        chip.SetData(playerIndex, card);
        chip.OnEndMove += DestroyCard;

        chip.MoveTo(
            transformFrom,
            cardSpawnerTargets.GetTransformMove(playerIndex),
            transformSpawnParent,
            canvas,
            0.3f
        );
    }

    public void SetCardCount(int cardCount)
    {
        textCardsCount.text = $"x{cardCount}";
    }

    private void DestroyCard(int playerIndex, ICard card, CardSpawnerMove cardMove)
    {
        cardMove.OnEndMove -= DestroyCard;

        OnSpawnCard?.Invoke(playerIndex, card);

        Destroy(cardMove.gameObject);
    }

    #region Output

    public event Action<int, ICard> OnSpawnCard;

    #endregion
}

[System.Serializable]
public class CardSpawnerTargets
{
    [SerializeField] private List<CardSpawnerTarget> targets = new();

    public RectTransform GetTransformMove(int playerIndex)
    {
        return targets.Find(t => t.PlayerId == playerIndex).TransformMove;
    }
}

[System.Serializable]
public class CardSpawnerTarget
{
    [SerializeField] private int playerId;
    [SerializeField] private RectTransform transformMove;

    public int PlayerId => playerId;
    public RectTransform TransformMove => transformMove;
}
