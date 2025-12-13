using System;

public class StoreChipPresenter : IStoreChip, IStoreChipChangeEvents
{ 
    private readonly StoreChipModel _model;

    public StoreChipPresenter(StoreChipModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void AddChip(int id)
    {
        _model.AddChip(id);
    }

    public void RemoveChip(int id)
    {
        _model.RemoveChip(id);
    }

    #endregion

    #region Output

    public event Action<int, int> OnChangeCountChips
    {
        add => _model.OnChangeChipCount += value;
        remove => _model.OnChangeChipCount -= value;
    }

    #endregion
}

public interface IStoreChipChangeEvents
{
    public event Action<int, int> OnChangeCountChips;
}

public interface IStoreChip
{
    public void AddChip(int id);

    public void RemoveChip(int id);
}
