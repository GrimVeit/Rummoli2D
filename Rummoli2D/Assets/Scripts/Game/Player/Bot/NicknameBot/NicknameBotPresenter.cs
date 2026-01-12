using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameBotPresenter
{
    private readonly NicknameBotModel _model;
    private readonly NicknameBotView _view;

    public NicknameBotPresenter(NicknameBotModel model, NicknameBotView view)
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
        _model.OnSetNickname += _view.SetNickname;
    }

    private void DeactivateEvents()
    {
        _model.OnSetNickname -= _view.SetNickname;
    }
    #region Output

    public string Nickname => _model.Nickname;

    #endregion


    #region Input

    public void SetNickname(string nickname)
    {
        _model.SetNickname(nickname);
    }

    #endregion
}
