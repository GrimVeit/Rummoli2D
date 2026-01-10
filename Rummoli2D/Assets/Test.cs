using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Настройки")]
    public TextMeshProUGUI[] textFields; // 5 текстов
    public string[] words;               // 5 разных слов/фраз
    public bool randomizeDots = true;    // чуть варьировать количество точек для «естественности»

    void Start()
    {
        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].text = FillWithDots(textFields[i], words[i]);
        }
    }

    string FillWithDots(TextMeshProUGUI tmp, string word)
    {
        string result = word;
        int extraDots = 0;

        // Целевая ширина в пикселях
        float targetWidth = tmp.rectTransform.rect.width;

        // Добавляем точки, пока ширина меньше target
        while (tmp.preferredWidth - 10 < targetWidth)
        {
            result += "."; // добавляем обычную точку
            tmp.text = result;
            extraDots++;

            // Если включена случайная вариация, иногда добавляем меньше/больше точки
            if (randomizeDots && extraDots % 2 == 0 && tmp.preferredWidth - 10  + tmp.fontSize / 2 > targetWidth)
                break; // чтобы не вышло сильно за границу
        }

        return result;
    }
}
