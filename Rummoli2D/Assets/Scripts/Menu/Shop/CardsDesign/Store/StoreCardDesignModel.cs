using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreCardDesignModel
{
    public event Action<int> OnOpenDesign;
    public event Action<int> OnCloseDesign;

    public event Action<int> OnDeselectDesign;
    public event Action<int> OnSelectDesign;

    private readonly List<CardDesignData> _designDatas = new();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "CardDesigns.json");

    public StoreCardDesignModel()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            CardDesignDatas cardDesignDatas = JsonUtility.FromJson<CardDesignDatas>(loadedJson);

            Debug.Log("Load data");

            _designDatas = cardDesignDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            _designDatas = new List<CardDesignData>();

            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    _designDatas.Add(new CardDesignData(true, true));
                }
                else
                {
                    _designDatas.Add(new CardDesignData(false, false));
                }
            }
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < _designDatas.Count; i++)
        {
            if (_designDatas[i].IsOpen)
                OnOpenDesign?.Invoke(i);
            else
                OnCloseDesign?.Invoke(i);


            if (_designDatas[i].IsSelect)
                OnSelectDesign?.Invoke(i);
            else
                OnDeselectDesign?.Invoke(i);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new CardDesignDatas(_designDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void OpenDesign(int id, Action OnComplete)
    {
        var designData = _designDatas[id];

        if (designData == null)
        {
            Debug.LogWarning("Not found CardDesignData by id - " + id);
            return;
        }

        designData.IsOpen = true;
        OnOpenDesign?.Invoke(id);

        OnComplete?.Invoke();
    }

    public void SelectDesign(int id, Action OnComplete)
    {
        var designData = _designDatas[id];

        if (designData == null)
        {
            Debug.LogWarning("Not found CardDesignData by id - " + id);
            return;
        }

        if (!designData.IsOpen || designData.IsSelect)
        {
            Debug.LogWarning("CardDesignData by id - " + id + " is Locked or Used");
            return;
        }

        UnselectAll();

        designData.IsSelect = true;
        OnSelectDesign?.Invoke(id);

        OnComplete?.Invoke();
    }

    public int GetDesignIndex()
    {
        for (int i = 0; i < _designDatas.Count; i++)
        {
            if (_designDatas[i].IsSelect)
                return i;
        }

        return -1;
    }

    public CardDesignData GetCardDesignData(int id)
    {
        return _designDatas[id];
    }

    private void UnselectAll()
    {
        for (int i = 0; i < _designDatas.Count; i++)
        {
            if (_designDatas[i].IsSelect)
            {
                _designDatas[i].IsSelect = false;
                OnDeselectDesign?.Invoke(i);
            }
        }
    }
}

[Serializable]
public class CardDesignDatas
{
    public CardDesignData[] Datas;

    public CardDesignDatas(CardDesignData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class CardDesignData
{
    public bool IsOpen;
    public bool IsSelect;

    public CardDesignData(bool isOpen, bool isSelect)
    {
        IsOpen = isOpen;
        IsSelect = isSelect;
    }
}
