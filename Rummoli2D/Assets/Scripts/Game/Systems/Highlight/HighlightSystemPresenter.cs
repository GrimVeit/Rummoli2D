using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSystemPresenter : IHighlightProvider
{
    private readonly HighlightSystemView _view;

    public HighlightSystemPresenter(HighlightSystemView view)
    {
        _view = view;
    }

    #region Input

    public void ActivateHighlight(int playerId) => _view.ActivateHighlight(playerId);
    public void DeactivateHighlight(int playerId) => _view.DeactivateHighlight(playerId);

    #endregion
}

public interface IHighlightProvider
{
    void ActivateHighlight(int playerId);
    void DeactivateHighlight(int playerId);
}
