using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTranslateView : View
{
    [SerializeField] private List<TextTranslate> textTranslates = new List<TextTranslate>();

    public void SetLanguage(Language language, bool animate)
    {
        textTranslates.ForEach(data => data.SetLanguage(language, animate));
    }
}

[System.Serializable]
public class TextTranslate
{
    [Header("Texts")]
    [SerializeField] private List<TextLanguage> textLanguages = new();

    [Header("Use")]
    [SerializeField] private List<TextMeshProUGUI> textMeshProUGUIs = new();
    [SerializeField] private List<TypeTextEffect> typeTextEffects = new();
    public void SetLanguage(Language language, bool animate)
    {
        var text = textLanguages.Find(data => data.Language == language).Text;

        textMeshProUGUIs.ForEach(data => data.text = text);

        for (int i = 0; i < typeTextEffects.Count; i++)
        {
            typeTextEffects[i].SetText(text);

            if(animate)
                typeTextEffects[i].ActivateEffect();
        }
    }
}

[System.Serializable]
public class TextLanguage
{
    [SerializeField] private string text;
    [SerializeField] private Language language;

    public string Text => text;
    public Language Language => language;
}
