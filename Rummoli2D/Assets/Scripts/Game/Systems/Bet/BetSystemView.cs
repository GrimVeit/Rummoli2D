using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetSystemView : View
{
    [SerializeField] private BetSystemChip chipMovePrefab;
    [SerializeField] private Transform transformSpawnParent;

    [SerializeField] private BetPlayerTransforms playerTransforms;
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

    public void AddBet(int playerIndex, int sectorIndex)
    {
        var chip = Instantiate(chipMovePrefab, transformSpawnParent);
        chip.SetData(playerIndex, sectorIndex);
        chip.OnEndMove += DestroyChip;
        chip.MoveTo(playerTransforms.GetTransformPlayer(playerIndex), sectors.GetTransformSector(sectorIndex), 0.3f);
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

    private void DestroyChip(int playerIndex, int sectorIndex, BetSystemChip chip)
    {
        chip.OnEndMove -= DestroyChip;

        OnSubmitBet?.Invoke(playerIndex, sectorIndex);

        Destroy(chip.gameObject);
    }

    #region Output

    public event Action<int, int> OnSubmitBet;
    public event Action<int> OnChooseSector;

    private void ChooseSector(int sector)
    {
        OnChooseSector?.Invoke(sector);
    } 

    #endregion
}

[System.Serializable]
public class BetPlayerTransforms
{
    [SerializeField] private List<BetPlayerTransform> betPlayerTransforms = new List<BetPlayerTransform>();

    public Transform GetTransformPlayer(int playerIndex)
    {
        return betPlayerTransforms.Find(data => data.PlayerIndex == playerIndex).Transform;
    }
}

[System.Serializable]
public class BetPlayerTransform
{
    [SerializeField] private int playerIndex;
    [SerializeField] private Transform transformPlayer;

    public int PlayerIndex => playerIndex;
    public Transform Transform => transformPlayer;
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

    public Transform GetTransformSector(int index)
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
    [SerializeField] private Transform transform;
    [SerializeField] private TextMeshProUGUI textCount;
    [SerializeField] private Button buttonSector;

    public int SectorIndex => sectorIndex;
    public Transform Transform => transform;

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
