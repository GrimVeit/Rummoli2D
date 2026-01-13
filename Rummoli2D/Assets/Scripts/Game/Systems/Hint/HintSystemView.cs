using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HintSystemView : View
{
    [SerializeField] private HintVisual hintVisualPrefab;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private HintConfigs hintConfigs;

    private readonly Dictionary<string, HintVisual> _spawnedHints = new();

    public void Show(string key, Language language)
    {
        if (_spawnedHints.ContainsKey(key))
        {
            Debug.LogWarning($"[HintSystem] Hint with key '{key}' is already shown!");
            return;
        }

        var hintData = hintConfigs.GetHintConfigByKey(key);
        if (hintData == null)
        {
            Debug.LogWarning($"[HintSystem] HintData with key '{key}' not found!");
            return;
        }

        var text = hintData.GetText(language);
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning($"[HintSystem] No text found for hint with key '{key}' in language '{language}'!");
            return;
        }

        HintVisual visual = Instantiate(hintVisualPrefab, transformSpawn);

        visual.transform.localPosition = hintData.TransformSpawn.localPosition;
        visual.SetSize(hintData.SizeDelta);
        visual.SetText(text);
        visual.SetFontSize(hintData.FontSize);

        _spawnedHints.Add(key, visual);

        visual.Show();
    }

    public void Hide(string key)
    {
        if (!_spawnedHints.TryGetValue(key, out var hintPrefab))
        {
            Debug.LogWarning($"[HintSystem] No spawned hint with key '{key}' to hide!");
            return;
        }

        hintPrefab.Delete();
    }

    public void ShowAll()
    {
        foreach (var kv in _spawnedHints)
        {
            kv.Value.Show();
        }
    }

    public void HideAll()
    {
        foreach (var kv in _spawnedHints)
        {
            kv.Value.Hide();
        }
    }

    public void DeleteAll()
    {
        foreach (var kv in _spawnedHints)
        {
            kv.Value.Delete();
        }

        _spawnedHints.Clear();
    }
}

[System.Serializable]
public class HintConfigs
{
    [SerializeField] private List<HintConfig> hintConfigs;

    public HintConfig GetHintConfigByKey(string key)
    {
        foreach (var hintConfig in hintConfigs)
        {
            if (hintConfig.Key == key)
            {
                return hintConfig;
            }
        }
        return null;
    }
}

[System.Serializable]
public class HintConfig
{
    [SerializeField] private string key;
    [SerializeField] private Vector2 sizeDelta;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private int fontSize;
    [SerializeField] private HintTexts hintTexts;

    public string Key => key;
    public Vector2 SizeDelta => sizeDelta;
    public Transform TransformSpawn => transformSpawn;
    public int FontSize => fontSize;
    public string GetText(Language language) => hintTexts.GetTextByLanguage(language);
}

[System.Serializable]
public class HintTexts
{
    [SerializeField] private List<HintText> texts;

    public string GetTextByLanguage(Language language)
    {
        foreach (var hintText in texts)
        {
            if (hintText.Language == language)
            {
                return hintText.Text;
            }
        }
        return string.Empty;
    } 
}

[System.Serializable]
public class HintText
{
    [SerializeField] private Language language;
    [SerializeField] private string text;

    public Language Language => language;
    public string Text => text;
}
