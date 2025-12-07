using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorResultVisualPresenter
{
    private readonly DoorResultVisualModel _model;
    private readonly DoorResultVisualView _view;

    public DoorResultVisualPresenter(DoorResultVisualModel model, DoorResultVisualView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }
}

public interface IDoorResultVisualProvider
{
    void SetDoor(Door door);
    void ShowVisual();
}
