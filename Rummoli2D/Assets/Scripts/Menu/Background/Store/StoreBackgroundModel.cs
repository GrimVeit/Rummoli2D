using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreBackgroundModel
{
    public event Action<int> OnOpenBackground;
    public event Action<int> OnCloseBackground;

    public event Action<int> OnDeselectBackground;
    public event Action<int> OnSelectBackground;

    private readonly List<BackgroundData> _backgroundDatas = new();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Backgrounds.json");

    public StoreBackgroundModel()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            BackgroundDatas backgroundDatas = JsonUtility.FromJson<BackgroundDatas>(loadedJson);

            Debug.Log("Load data");

            this._backgroundDatas = backgroundDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            _backgroundDatas = new List<BackgroundData>();

            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    _backgroundDatas.Add(new BackgroundData(true, true));
                }
                else
                {
                    _backgroundDatas.Add(new BackgroundData(false, false));
                }
            }
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < _backgroundDatas.Count; i++)
        {
            if (_backgroundDatas[i].IsOpen)
                OnOpenBackground?.Invoke(i);
            else
                OnCloseBackground?.Invoke(i);


            if (_backgroundDatas[i].IsSelect)
                OnSelectBackground?.Invoke(i);
            else
                OnDeselectBackground?.Invoke(i);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new BackgroundDatas(_backgroundDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void OpenBackground(int idBackground, Action OnComplete)
    {
        var backgroundData = _backgroundDatas[idBackground];

        if (backgroundData == null)
        {
            Debug.LogWarning("Not found BackgroundData by id - " + idBackground);
            return;
        }

        backgroundData.IsOpen = true;
        OnOpenBackground?.Invoke(idBackground);

        OnComplete?.Invoke();
    }

    public void SelectBackground(int idBackground, Action OnComplete)
    {
        var backgroundData = _backgroundDatas[idBackground];

        if (backgroundData == null)
        {
            Debug.LogWarning("Not found BackgroundData by id - " + idBackground);
            return;
        }

        if (!backgroundData.IsOpen || backgroundData.IsSelect)
        {
            Debug.LogWarning("BackgroundData by id - " + idBackground + " is Locked or Used");
            return;
        }

        UnselectAll();

        backgroundData.IsSelect = true;
        OnSelectBackground?.Invoke(idBackground);

        OnComplete?.Invoke();
    }

    public int GetBackgroundIndex()
    {
        for (int i = 0; i < _backgroundDatas.Count; i++)
        {
            if (_backgroundDatas[i].IsSelect)
                return i;
        }

        return -1;
    }

    public BackgroundData GetBackgroundData(int id)
    {
        return _backgroundDatas[id];
    }

    private void UnselectAll()
    {
        for (int i = 0; i < _backgroundDatas.Count; i++)
        {
            if (_backgroundDatas[i].IsSelect)
            {
                _backgroundDatas[i].IsSelect = false;
                OnDeselectBackground?.Invoke(i);
            }
        }
    }
}

[Serializable]
public class BackgroundDatas
{
    public BackgroundData[] Datas;

    public BackgroundDatas(BackgroundData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class BackgroundData
{
    public bool IsOpen;
    public bool IsSelect;

    public BackgroundData(bool isOpen, bool isSelect)
    {
        IsOpen = isOpen;
        IsSelect = isSelect;
    }
}
