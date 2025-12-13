using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipBuyModel
{
    private readonly IChipGroupStore _chipGroupStore;
    private readonly IStoreChip _storeChip;
    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;

    public ChipBuyModel(IChipGroupStore chipGroupStore, IStoreChip storeChip, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _chipGroupStore = chipGroupStore;
        _storeChip = storeChip;
        _moneyProvider = moneyProvider;
        _soundProvider = soundProvider;
    }

    public void AddChip(int id)
    {
        if (!_chipGroupStore.HasElementByID(id))
        {
            Debug.LogWarning("Not found chip group by id - " + id);
            return;
        }

        int nominal = _chipGroupStore.GetNominalChipByID(id);

        if (_moneyProvider.CanAfford(nominal))
        {
            Debug.LogWarning(nominal);
            _storeChip.AddChip(id);
            _moneyProvider.SendMoney(-nominal);
            _soundProvider.PlayOneShot("Click");
        }
        else
        {
            var need = nominal - _moneyProvider.GetMoney();

            //_notificationProvider.SendMessage($"Need <color=#ffccd4>{need}</color> more coins to buy a <color=#ffccd4>{nominal} chip</color>", "<color=#ffccd4>Not Enough Coins!</color>", 0);
            _soundProvider.PlayOneShot("Error");
        }
    }

    public void RemoveChip(int id)
    {
        if (!_chipGroupStore.HasElementByID(id))
        {
            Debug.LogWarning("Not found chip group by id - " + id);
            return;
        }

        if(_chipGroupStore.GetCountChipsByID(id) == 0) return;

        int nominal = _chipGroupStore.GetNominalChipByID(id);

        _storeChip.RemoveChip(id);
        _moneyProvider.SendMoney(nominal);
        _soundProvider.PlayOneShot("Click");
    }
}
