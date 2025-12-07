using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusApplierModel
{
    public BonusType CurrentBonusType => _currentBonusType;

    private readonly IStoreDoorEventsProvider _storeDoorEventsProvider;
    private readonly IBonusVisualEventsProvider _bonusVisualEventsProvider;
    private readonly IDoorDesignProvider _doorDesignProvider;
    private readonly IStoreBonusProvider _storeBonusProvider;
    private readonly ISoundProvider _soundProvider;

    private BonusType _currentBonusType;
    private List<Door> _currentDoors;

    public BonusApplierModel(IStoreDoorEventsProvider storeDoorEventsProvider, IBonusVisualEventsProvider bonusVisualEventsProvider, IStoreBonusProvider storeBonusProvider, IDoorDesignProvider doorDesignProvider, ISoundProvider soundProvider)
    {
        _storeDoorEventsProvider = storeDoorEventsProvider;
        _bonusVisualEventsProvider = bonusVisualEventsProvider;
        _storeBonusProvider = storeBonusProvider;
        _doorDesignProvider = doorDesignProvider;
        _soundProvider = soundProvider;

        _storeDoorEventsProvider.OnDoorsCreated += SetDoors;
        _bonusVisualEventsProvider.OnChooseBonus_Value += SetBonus;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeDoorEventsProvider.OnDoorsCreated -= SetDoors;
        _bonusVisualEventsProvider.OnChooseBonus_Value -= SetBonus;
    }

    private void SetBonus(BonusType bonusType)
    {
        _currentBonusType = bonusType;
    }

    private void SetDoors(List<Door> doors)
    {
        _currentDoors = doors;
    }

    public void ApplyBonus()
    {
        _storeBonusProvider.RemoveBonus(_currentBonusType, 1);

        switch (_currentBonusType)
        {
            case BonusType.Oracle:

                bool isApply = false;

                for (int i = 0; i < _currentDoors.Count; i++)
                {
                    if (!_currentDoors[i].HasDanger)
                    {
                        _doorDesignProvider.ShowOracle(i);
                        isApply = true;
                    }
                }

                if(!isApply)
                    OnNotApplyBonus?.Invoke();

                break;

            case BonusType.EvilTongue:

                var dangerDoorsIndexes = Array.FindAll(Enumerable.Range(0, _currentDoors.Count).ToArray(), i => _currentDoors[i].HasDanger);

                if(dangerDoorsIndexes.Length == 0)
                {
                    OnNotApplyBonus?.Invoke();
                    return;
                }

                _doorDesignProvider.ShowEvilTongue(dangerDoorsIndexes[Random.Range(0, dangerDoorsIndexes.Length)]);

                break;
        }
    }

    public void ApplyBonus(int doorId)
    {
        _storeBonusProvider.RemoveBonus(_currentBonusType, 1);

        var door = _currentDoors[doorId];

        switch (_currentBonusType)
        {
            case BonusType.Amulet:
                door.HasDanger = false;
                break;
            case BonusType.NormalKey:
                if(door.HasLock && !door.IsGoldLock)
                {
                    door.HasLock = false;
                    _soundProvider.PlayOneShot("DoorUnlock");
                }
                break;
            case BonusType.GoldenKey:
                if(door.HasLock)
                    door.HasLock = false;

                _soundProvider.PlayOneShot("DoorUnlock");
                break;
        }
    }

    #region Output

    public event Action OnNotApplyBonus;

    #endregion
}
