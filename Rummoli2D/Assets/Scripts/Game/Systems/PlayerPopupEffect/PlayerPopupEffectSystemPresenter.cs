using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPopupEffectSystemPresenter : IPlayerPopupEffectSystemProvider
{
    private readonly PlayerPopupEffectSystemView _view;

    public PlayerPopupEffectSystemPresenter(PlayerPopupEffectSystemView view)
    {
        _view = view;
    }

    #region Input

    public void ShowPass(int playerId) => _view.ShowPass(playerId);

    #endregion
}

public interface IPlayerPopupEffectSystemProvider
{
    public void ShowPass(int playerId);
}
