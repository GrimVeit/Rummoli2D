using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetSystemView : View
{
    [SerializeField] private BetAddChip chipAddBetPrefab;
    [SerializeField] private BetReturnChip chipReturnBetPrefab;
    [SerializeField] private RectTransform transformSpawnParent;
    [SerializeField] private Canvas canvas;

    [SerializeField] private PlayerTransforms playerTransforms;
    [SerializeField] private Sectors sectors;

    public void Initialize()
    {
        sectors.OnChooseSector += ChooseSector;
        sectors.Initialize();
    }

    public void Dispose()
    {
        sectors.OnChooseSector -= ChooseSector;
        sectors.Dispose();
    }

    public void SetSectorBetCount(int sectorIndex, int count)
    {
        sectors.SetSectorBetCount(sectorIndex, count);
    }

    public void ActivateInteractive()
    {
        sectors.ActivateInteractive();
    }

    public void DeactivateInteractive()
    {
        sectors.DeactivateInteractive();
    }

    #region AddBet

    public void StartAddBet(int playerIndex, int sectorIndex)
    {
        var chip = Instantiate(chipAddBetPrefab, transformSpawnParent);
        chip.SetData(playerIndex, sectorIndex);
        chip.OnEndMove += DestroyAddBetChip;
        chip.MoveTo(playerTransforms.GetTransformPlayer(playerIndex), sectors.GetTransformSector(sectorIndex), transformSpawnParent, canvas, 0.5f);
    }

    private void DestroyAddBetChip(int playerIndex, int sectorIndex, BetAddChip chip)
    {
        chip.OnEndMove -= DestroyAddBetChip;

        OnAddBet?.Invoke(playerIndex, sectorIndex);

        Destroy(chip.gameObject);
    }

    #endregion

    #region ReturnBet

    public void StartReturnBet(int playerIndex, int sectorIndex, int score)
    {
        var chip = Instantiate(chipReturnBetPrefab, transformSpawnParent);
        chip.SetData(playerIndex, score);
        chip.OnEndMove += DestroyReturnBetChip;
        chip.MoveTo(sectors.GetTransformSector(sectorIndex), playerTransforms.GetTransformPlayer(playerIndex), transformSpawnParent, canvas, 0.5f);
    }

    private void DestroyReturnBetChip(int playerIndex, int sectorIndex, BetReturnChip chip)
    {
        chip.OnEndMove -= DestroyReturnBetChip;

        OnReturnBet?.Invoke(playerIndex, sectorIndex);

        Destroy(chip.gameObject);
    }

    #endregion

    #region Output

    public event Action<int, int> OnAddBet;
    public event Action<int, int> OnReturnBet;
    public event Action<int> OnChooseSector;

    private void ChooseSector(int sector)
    {
        OnChooseSector?.Invoke(sector);
    } 

    #endregion
}

[System.Serializable]
public class PlayerTransforms
{
    [SerializeField] private List<PlayerTransform> betPlayerTransforms = new List<PlayerTransform>();

    public RectTransform GetTransformPlayer(int playerIndex)
    {
        return betPlayerTransforms.Find(data => data.PlayerIndex == playerIndex).Transform;
    }
}

[System.Serializable]
public class PlayerTransform
{
    [SerializeField] private int playerIndex;
    [SerializeField] private RectTransform transformPlayer;

    public int PlayerIndex => playerIndex;
    public RectTransform Transform => transformPlayer;
}


[System.Serializable]
public class Sectors
{
    [SerializeField] private List<Sector> sectors = new List<Sector>();

    public void Initialize()
    {
        for (int i = 0; i < sectors.Count; i++)
        {
            sectors[i].OnChooseSector += ChooseSector;
            sectors[i].Initialize();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < sectors.Count; i++)
        {
            sectors[i].OnChooseSector -= ChooseSector;
            sectors[i].Dispose();
        }
    }

    public void SetSectorBetCount(int index, int count)
    {
        var sector = GetSector(index);

        if(sector == null)
        {
            Debug.LogWarning("Not found Sector with index - " + index);
            return;
        }

        sector.SetCount(count);
    }

    public void ActivateInteractive()
    {
        sectors.ForEach(data => data.ActivateInteractive());
    }

    public void DeactivateInteractive()
    {
        sectors.ForEach(data => data.DeactivateInteractive());
    }

    public Sector GetSector(int index)
    {
        return sectors.Find(data => data.SectorIndex == index);
    }

    public RectTransform GetTransformSector(int index)
    {
        return GetSector(index).Transform;
    }

    #region Output

    public event Action<int> OnChooseSector;

    private void ChooseSector(int sectorIndex)
    {
        OnChooseSector?.Invoke(sectorIndex);
    }

    #endregion
}

[System.Serializable]
public class Sector
{
    [SerializeField] private int sectorIndex;
    [SerializeField] private RectTransform transform;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private Button buttonSector;

    public int SectorIndex => sectorIndex;
    public RectTransform Transform => transform;

    public void Initialize()
    {
        buttonSector.onClick.AddListener(ChooseSector);
    }

    public void Dispose()
    {
        buttonSector.onClick.RemoveListener(ChooseSector);
    }

    public void ActivateInteractive()
    {
        buttonSector.enabled = true;
    }

    public void SetCount(int count)
    {
        textCount.text = count.ToString();
    }

    public void DeactivateInteractive()
    {
        buttonSector.enabled = false;
    }

    #region Output

    public event Action<int> OnChooseSector;

    private void ChooseSector()
    {
        OnChooseSector?.Invoke(sectorIndex);
    }

    #endregion
}
