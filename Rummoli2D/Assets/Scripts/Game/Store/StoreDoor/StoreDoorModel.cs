using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDoorModel
{
    private readonly IDoorCounterInfoProvider _counterInfoProvider;
    private readonly System.Random rnd = new();

    public StoreDoorModel(IDoorCounterInfoProvider counterInfoProvider)
    {
        _counterInfoProvider = counterInfoProvider;
    }

    /// Метод создания комнаты с 3 дверями
    public void GenerateDoors()
    {
        List<DoorType> available = GetAvailableTypes();
        List<Door> doors = CreateRandomDoorTypes(available);

        int ghosts = GetGhostCount();
        AssignGhosts(doors, ghosts);

        AssignBonuses(doors);

        DebugPrintDoors(doors); // для теста

        OnDoorsCreated?.Invoke(doors); // событие для UI
    }

    #region Генерация типов дверей

    private List<DoorType> GetAvailableTypes()
    {
        int d = _counterInfoProvider.GetCountDoor();

        if (d <= 20)
            return new() { DoorType.Normal, DoorType.Medium };

        if (d <= 50)
            return new() { DoorType.Normal, DoorType.Medium, DoorType.Dangerous, DoorType.Locked };

        return new() { DoorType.Medium, DoorType.Dangerous, DoorType.Spikes, DoorType.Locked, DoorType.Gold };
    }

    private List<Door> CreateRandomDoorTypes(List<DoorType> pool)
    {
        List<Door> result = new();

        for (int i = 0; i < 3; i++)
        {
            DoorType type = pool[rnd.Next(pool.Count)];
            result.Add(CreateDoorByType(type));
        }
        return result;
    }

    private Door CreateDoorByType(DoorType type)
    {
        Door door = new()
        {
            Type = type,
            HasDanger = false,
            DangerLevel = DangerLevel.None,
            HasBonus = false,
            BonusCount = 0
        };

        switch (type)
        {
            case DoorType.Locked:
                door.HasLock = true;
                door.IsGoldLock = false;
                break;

            case DoorType.Gold:
                door.HasLock = true;
                door.IsGoldLock = true;
                break;

            case DoorType.Spikes:
                door.HasDanger = true;
                door.DangerLevel = DangerLevel.Medium;
                break;
        }

        return door;
    }

    #endregion

    #region Опасности (призраки)

    private int GetGhostCount()
    {
        int d = _counterInfoProvider.GetCountDoor();

        if (d == 0)
            return 0;

        if (d <= 20)
            return 1;

        if (d <= 50)
            return rnd.NextDouble() < 0.3 ? 2 : 1;

        return rnd.NextDouble() < 0.5 ? 2 : 1;
    }

    private int GetDangerWeight(DoorType type)
    {
        return type switch
        {
            DoorType.Normal => 25,
            DoorType.Medium => 50,
            DoorType.Dangerous => 75,
            DoorType.Locked => 50,
            DoorType.Gold => 75,
            DoorType.Spikes => 100,
            _ => 0
        };
    }

    private void AssignGhosts(List<Door> doors, int ghostsLeft)
    {
        while (ghostsLeft > 0)
        {
            int sum = 0;
            foreach (var d in doors)
                if (!d.HasDanger)
                    sum += GetDangerWeight(d.Type);

            if (sum == 0) break;

            int roll = rnd.Next(sum);
            int current = 0;

            foreach (var d in doors)
            {
                if (d.HasDanger) continue;
                current += GetDangerWeight(d.Type);

                if (roll < current)
                {
                    d.HasDanger = true;
                    d.DangerLevel = GetDangerLevelByType(d.Type);
                    ghostsLeft--;
                    break;
                }
            }
        }
    }

    private DangerLevel GetDangerLevelByType(DoorType type)
    {
        return type switch
        {
            DoorType.Normal => DangerLevel.Low,
            DoorType.Medium => DangerLevel.Medium,
            DoorType.Dangerous => DangerLevel.High,
            DoorType.Spikes => DangerLevel.High,
            DoorType.Locked => DangerLevel.Medium,
            DoorType.Gold => DangerLevel.High,
            _ => DangerLevel.None
        };
    }

    #endregion

    #region Бонусы

    private void AssignBonuses(List<Door> doors)
    {
        foreach (var d in doors)
        {
            d.BonusCount = GetBonusAmount(d.Type);

            // С вероятностью 50% бонус будет
            if (rnd.NextDouble() > 0.5) continue;

            d.HasBonus = true;
        }
    }

    private int GetBonusAmount(DoorType type)
    {
        return type switch
        {
            DoorType.Normal => 1,
            DoorType.Medium => 2,
            DoorType.Locked => 2,
            DoorType.Dangerous => 3,
            DoorType.Gold => 3,
            DoorType.Spikes => 5,
            _ => 0
        };
    }

    #endregion

    #region Debug

    private void DebugPrintDoors(List<Door> doors)
    {
        for (int i = 0; i < doors.Count; i++)
        {
            var d = doors[i];
            string log = $"Door {i + 1}({_counterInfoProvider.GetCountDoor() + 1}): {d.Type} | " +
                         $"Locked: {d.HasLock} | GoldLock: {d.IsGoldLock} | " +
                         $"Danger: {d.HasDanger} ({d.DangerLevel}) | " +
                         $"Bonus: {d.HasBonus} | " +
                         $"BonusCount: {d.BonusCount}";

            Debug.Log(log);
        }
    }

    #endregion

    #region Output

    public event Action<List<Door>> OnDoorsCreated;

    #endregion
}

[System.Serializable]
public class Door
{
    public DoorType Type;

    // Замки
    public bool HasLock;
    public bool IsGoldLock;

    // Опасность
    public bool HasDanger;
    public DangerLevel DangerLevel;

    // Бонусы
    public bool HasBonus;
    public int BonusCount;
}

/// Тип двери — влияет на шансы опасности и количество бонусов
public enum DoorType
{
    Normal,
    Medium,
    Dangerous,
    Spikes,
    Locked,
    Gold
}

/// Уровень опасности — определяет урон (-1 / -2 / -3)
public enum DangerLevel
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3
}
