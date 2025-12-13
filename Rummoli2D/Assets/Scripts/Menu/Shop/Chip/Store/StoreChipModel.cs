using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreChipModel
{
    public event Action<int, int> OnChangeChipCount;

    private readonly ChipGroup _chipGroup;

    private List<ChipData> chipDatas = new List<ChipData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Chips.json");

    public StoreChipModel(ChipGroup chipGroup)
    {
        _chipGroup = chipGroup;

        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            ChipDatas gameTypeDatas = JsonUtility.FromJson<ChipDatas>(loadedJson);

            Debug.Log("Load data");

            this.chipDatas = gameTypeDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            chipDatas = new List<ChipData>();

            for (int i = 0; i < chipGroup.Chips.Count; i++)
            {
                if(i == 0)
                {
                    chipDatas.Add(new ChipData(10));
                }
                else if(i == 1)
                {
                    chipDatas.Add(new ChipData(5));
                }
                else
                {
                    chipDatas.Add(new ChipData(0));
                }
            }
        }

        for (int i = 0; i < chipGroup.Chips.Count; i++)
        {
            chipGroup.Chips[i].SetData(chipDatas[i]);
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < _chipGroup.Chips.Count; i++)
        {
            OnChangeChipCount?.Invoke(_chipGroup.Chips[i].ID, _chipGroup.Chips[i].ChipData.ChipsCount);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new ChipDatas(chipDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void AddChip(int idGroup)
    {
        var group = _chipGroup.GetChipsById(idGroup);

        if(group == null)
        {
            Debug.LogError("Not found chip group by id - " + idGroup);
            return;
        }

        if (group.ChipData == null)
        {
            Debug.LogError("Not found chip data with group by id - " + idGroup);
            return;
        }

        group.ChipData.ChipsCount += 1;
        OnChangeChipCount?.Invoke(group.ID, group.ChipData.ChipsCount);
    }

    public void RemoveChip(int idGroup)
    {
        var group = _chipGroup.GetChipsById(idGroup);

        if (group == null)
        {
            Debug.LogError("Not found chip group by id - " + idGroup);
            return;
        }

        if (group.ChipData == null)
        {
            Debug.LogError("Not found chip data with group by id - " + idGroup);
            return;
        }

        group.ChipData.ChipsCount -= 1;
        OnChangeChipCount?.Invoke(group.ID, group.ChipData.ChipsCount);
    }
}

[Serializable]
public class ChipDatas
{
    public ChipData[] Datas;

    public ChipDatas(ChipData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class ChipData
{
    public int ChipsCount;

    public ChipData(int chipsCount)
    {
        ChipsCount = chipsCount;
    }
}
