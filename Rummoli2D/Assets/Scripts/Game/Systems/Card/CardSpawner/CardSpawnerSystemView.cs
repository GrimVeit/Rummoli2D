using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawnerSystemView : View
{
    [SerializeField] private CardSpawnerMove cardSpawnerMovePrefab;
    [SerializeField] private Transform transformSpawnParent;
    [SerializeField] private Transform transformFrom;
    [SerializeField] private CardSpawnerTargets cardSpawnerTargets;

    public void SpawnCard(int playerIndex, ICard card)
    {
        var chip = Instantiate(cardSpawnerMovePrefab, transformSpawnParent);
        chip.SetData(playerIndex, card);
        chip.OnEndMove += DestroyCard;
        chip.MoveTo(transformFrom, cardSpawnerTargets.GetTransformMove(playerIndex), 0.3f);
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

    public Transform GetTransformMove(int playerIndex)
    {
        return targets.Find(t => t.PlayerId == playerIndex).TransformMove;
    }
}

[System.Serializable]
public class CardSpawnerTarget
{
    [SerializeField] private int playerId;
    [SerializeField] private Transform transformMove;

    public int PlayerId => playerId;
    public Transform TransformMove => transformMove;
}
