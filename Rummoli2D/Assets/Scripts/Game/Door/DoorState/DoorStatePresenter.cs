using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatePresenter : IDoorStateProvider, IDoorStateEventsProvider
{
    private readonly DoorStateView _view;

    private readonly ISoundProvider _soundProvider;

    public DoorStatePresenter(DoorStateView view, ISoundProvider soundProvider)
    {
        _view = view;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnSoundMoveUp += SoundMoveUp;
        _view.OnSoundMoveDown += SoundMoveDown;
        _view.OnSoundOpen += SoundOpen;
    }

    private void DeactivateEvents()
    {
        _view.OnSoundMoveUp -= SoundMoveUp;
        _view.OnSoundMoveDown -= SoundMoveDown;
        _view.OnSoundOpen -= SoundOpen;
    }

    private void SoundMoveUp()
    {
        _soundProvider.PlayOneShot("DoorMoveUp");
    }

    private void SoundMoveDown()
    {
        _soundProvider.PlayOneShot("DoorMoveDown");
    }

    private void SoundOpen()
    {
        _soundProvider.PlayOneShot("DoorOpen");
    }

    #region Output

    public event Action OnEndActivateAllDoors { add => _view.OnEndActivateAllDoors += value; remove => _view.OnEndActivateAllDoors -= value; }
    public event Action OnEndDeactivateAllDoors { add => _view.OnEndDeactivateAllDoors += value; remove => _view.OnEndDeactivateAllDoors -= value; }
    public event Action OnEndOpenDoor { add => _view.OnEndOpenDoor += value; remove => _view.OnEndOpenDoor -= value; }

    #endregion

    #region Input

    public void OpenDoor(int id) => _view.Open(id);
    public void DeactivateAll() => _view.DeactivateAll();
    public void ActivateAll() => _view.ActivateAll();
    public void Hide() => _view.Hide();

    #endregion
}

public interface IDoorStateProvider
{
    void OpenDoor(int id);
    void DeactivateAll();
    void ActivateAll();
    void Hide();
}

public interface IDoorStateEventsProvider
{
    public event Action OnEndActivateAllDoors;
    public event Action OnEndDeactivateAllDoors;
    public event Action OnEndOpenDoor;
}
