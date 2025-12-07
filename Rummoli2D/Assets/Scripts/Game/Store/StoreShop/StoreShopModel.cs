using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreShopModel
{
    public int LevelShield => _levelShield;
    public int LevelEvil => _levelEvil;
    public int LevelOracle => _levelOracle;

    private int _levelShield = -1;
    private int _levelEvil = -1;
    private int _levelOracle = -1;

    private readonly string _keyShield;
    private readonly string _keyEvil;
    private readonly string _keyOracle;

    public StoreShopModel(string keyShield, string keyEvil, string keyOracle)
    {
        _keyShield = keyShield;
        _keyEvil = keyEvil;
        _keyOracle = keyOracle;
    }

    public void Initialize()
    {
        _levelShield = PlayerPrefs.GetInt(_keyShield, 0);
        _levelEvil = PlayerPrefs.GetInt(_keyEvil, 0);
        _levelOracle = PlayerPrefs.GetInt(_keyOracle, 0);

        OnLevelShopChanged?.Invoke(ShopGroup.Shield, _levelShield);
        OnLevelShopChanged?.Invoke(ShopGroup.Evil, _levelEvil);
        OnLevelShopChanged?.Invoke(ShopGroup.Oracle, _levelOracle);
    }

    public void SetLevel(ShopGroup group, int levelId)
    {
        switch (group)
        {
            case ShopGroup.Shield:
                _levelShield = levelId;
                OnLevelShopChanged.Invoke(ShopGroup.Shield, _levelShield);
                break;
            case ShopGroup.Evil:
                _levelEvil = levelId;
                OnLevelShopChanged.Invoke(ShopGroup.Evil, _levelEvil);
                break;
            case ShopGroup.Oracle:
                _levelOracle = levelId;
                OnLevelShopChanged.Invoke(ShopGroup.Oracle, _levelOracle);
                break;
        }
    }

    public void Dispose()
    {
        PlayerPrefs.SetInt(_keyShield, _levelShield);
        PlayerPrefs.SetInt(_keyEvil, _levelEvil);
        PlayerPrefs.SetInt(_keyOracle, _levelOracle);
    }

    #region Output

    public event Action<ShopGroup, int> OnLevelShopChanged;

    #endregion
}

public enum ShopGroup
{
    Shield = 0, 
    Evil = 1, 
    Oracle = 2
}
