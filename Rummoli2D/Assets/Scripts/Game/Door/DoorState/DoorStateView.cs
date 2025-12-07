using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateView : View
{
    [SerializeField] private DoorUnit doorUnit_1;
    [SerializeField] private DoorUnit doorUnit_2;
    [SerializeField] private DoorUnit doorUnit_3;

    [SerializeField] private float timeWaitActivateDeactivateAll;
    [SerializeField] private float timeWait;

    private IEnumerator timerActivator;

    public void Initialize()
    {
        doorUnit_1.OnSoundMoveUp += SoundMoveUp;
        doorUnit_2.OnSoundMoveUp += SoundMoveUp;
        doorUnit_3.OnSoundMoveUp += SoundMoveUp;

        doorUnit_1.OnSoundMoveDown += SoundMoveDown;
        doorUnit_2.OnSoundMoveDown += SoundMoveDown;
        doorUnit_3.OnSoundMoveDown += SoundMoveDown;

        doorUnit_1.OnSoundOpen += SoundOpen;
        doorUnit_2.OnSoundOpen += SoundOpen;
        doorUnit_3.OnSoundOpen += SoundOpen;
    }

    public void Dispose()
    {
        doorUnit_1.OnSoundMoveUp -= SoundMoveUp;
        doorUnit_2.OnSoundMoveUp -= SoundMoveUp;
        doorUnit_3.OnSoundMoveUp -= SoundMoveUp;

        doorUnit_1.OnSoundMoveDown -= SoundMoveDown;
        doorUnit_2.OnSoundMoveDown -= SoundMoveDown;
        doorUnit_3.OnSoundMoveDown -= SoundMoveDown;

        doorUnit_1.OnSoundOpen -= SoundOpen;
        doorUnit_2.OnSoundOpen -= SoundOpen;
        doorUnit_3.OnSoundOpen -= SoundOpen;
    }

    public void Hide()
    {
        doorUnit_1.Hide();
        doorUnit_2.Hide();
        doorUnit_3.Hide();
    }

    public void ActivateAll()
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = ActivateAll_Coro();
        Coroutines.Start(timerActivator);
    }

    public void DeactivateAll()
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = DeactivateAll_Coro();
        Coroutines.Start(timerActivator);
    }

    public void Open(int door)
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = Open_Coro(door);
        Coroutines.Start(timerActivator);
    }

    private IEnumerator ActivateAll_Coro()
    {
        doorUnit_1.Activate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_2.Activate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_3.Activate(() => OnEndActivateAllDoors?.Invoke());
    }

    private IEnumerator DeactivateAll_Coro()
    {
        doorUnit_1.Deactivate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_2.Deactivate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_3.Deactivate(() => OnEndDeactivateAllDoors?.Invoke());
    }

    private IEnumerator Open_Coro(int door)
    {
        if (door == 0)
        {
            doorUnit_2.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_3.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_1.Open(() => OnEndOpenDoor?.Invoke());
        }
        else if (door == 1)
        {
            doorUnit_1.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_3.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_2.Open(() => OnEndOpenDoor?.Invoke());
        }
        else
        {
            doorUnit_2.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_1.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_3.Open(() => OnEndOpenDoor?.Invoke());
        }
    }

    #region Output

    public event Action OnEndActivateAllDoors;
    public event Action OnEndDeactivateAllDoors;
    public event Action OnEndOpenDoor;

    public event Action OnSoundMoveUp;
    public event Action OnSoundMoveDown;
    public event Action OnSoundOpen;

    private void SoundMoveUp()
    {
        OnSoundMoveUp?.Invoke();
    }

    private void SoundMoveDown()
    {
        OnSoundMoveDown?.Invoke();
    }

    private void SoundOpen()
    {
        OnSoundOpen?.Invoke();
    }

    #endregion
}
