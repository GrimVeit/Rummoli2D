using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBotPresenter
{
    private readonly AvatarBotModel _model;
    private readonly AvatarBotView _view;

    public AvatarBotPresenter(AvatarBotModel model, AvatarBotView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnSetAvatarIndex += _view.SetAvatar;
    }

    private void DeactivateEvents()
    {
        _model.OnSetAvatarIndex -= _view.SetAvatar;
    }

    #region Input

    public void SetAvatarIndex(int id)
    {
        _model.SetAvatarIndex(id);
    }

    #endregion
}
