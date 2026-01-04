using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SectorConditionBase
{
    public int SectorIndex { get; protected set; }   // индекс сектора (0-8)
    public SectorStatus Status { get; protected set; } = SectorStatus.Pending;

    // Проверка карты, скинутой игроком
    public abstract void CheckCard(int playerId, ICard card);
    public abstract void Complete();
    public abstract void Reset();
    public abstract (int playerId, ICard card) GetClosingInfo();
}

public enum SectorStatus
{
    Pending,    // ещё не готов
    Ready,      // готов к выдаче
    Claimed     // уже использован / забран
}
