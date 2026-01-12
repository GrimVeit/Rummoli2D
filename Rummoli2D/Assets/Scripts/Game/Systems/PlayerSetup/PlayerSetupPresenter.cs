using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupPresenter
{
    private readonly PlayerSetupModel _model;
    private readonly PlayerSetupView _view;

    public PlayerSetupPresenter(PlayerSetupModel model, PlayerSetupView view)
    {
        _model = model;
        _view = view;
    }

    public void Setup() => _model.Setup();
    public void SetStartPositions(int count) => _view.SetCount(count);
    public List<IPlayer> GetPlayers() => _model.GetPlayers();
}
