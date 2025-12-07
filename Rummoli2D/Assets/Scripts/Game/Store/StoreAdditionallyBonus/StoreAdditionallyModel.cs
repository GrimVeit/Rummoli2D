using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAdditionallyModel
{
    private readonly Dictionary<int, bool> _bonusConditions = new();

    private readonly List<string> _keys;

    public StoreAdditionallyModel(List<string> keys)
    {
        _keys = keys;

        for (int i = 0; i < _keys.Count; i++)
        {
            var value = PlayerPrefs.GetInt(_keys[i]);

            if(value != 0)
                value = 1;

            if (value == 0)
            {
                _bonusConditions[i] = false;
            }
            else if (value == 1)
            {
                _bonusConditions[i] = true;
            }
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            int value;
            if (_bonusConditions[i] == true)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }

            PlayerPrefs.SetInt(_keys[i], value);
        }
    }

    public void ActivateBonusCondition(int id)
    {
        _bonusConditions[id] = true;
    }

    public bool IsActiveBonusCondition(int id) => _bonusConditions[id] == true;
}
