using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipGroup", menuName = "Game/Chip/New Group")]
public class ChipGroup : ScriptableObject, IChipGroupStore, IChipGroupBet
{
    public List<Chips> Chips = new List<Chips>();

    public Chips GetChipsById(int id)
    {
        return Chips.FirstOrDefault(cc => cc.ID == id);
    }

    public Chip GetChipBiId(int id)
    {
        return GetChipsById(id).Chip;
    }

    public int GetNominalChipByID(int id)
    {
        return GetChipsById(id).Chip.Nominal;
    }

    public int GetCountChipsByID(int id)
    {
        return GetChipsById(id).ChipData.ChipsCount;
    }

    public bool HasElementByID(int id)
    {
        return Chips.Any(cc => cc.ID == id);
    }

    public bool CanHaveCountChipsByOneId(int id, int countChip)
    {
        return GetChipsById(id).ChipData.ChipsCount >= countChip;
    }

    public bool CanHaveCountChipsByManyId(Dictionary<int, int> chipsGroups)
    {
        foreach (var chipGroup in chipsGroups)
        {
            if(!CanHaveCountChipsByOneId(chipGroup.Key, chipGroup.Value))
            {
                return false;
            }
        }

        return true;
    }

    public Chip GetChipById(int id)
    {
        return GetChipsById(id).Chip;
    }

    public int HowNeedChipsById(int id, int countChip)
    {
        var count = GetChipsById(id).ChipData.ChipsCount;


        return countChip - count;
    }
}

public interface IChipGroupStore
{
    public int GetNominalChipByID(int id);

    public int GetCountChipsByID(int id);
    public bool HasElementByID(int id);
}

public interface IChipGroupBet
{
    public int HowNeedChipsById(int id, int countChip);    
    public bool CanHaveCountChipsByOneId(int id, int countChip);
    public bool CanHaveCountChipsByManyId(Dictionary<int, int> countChips);
}
