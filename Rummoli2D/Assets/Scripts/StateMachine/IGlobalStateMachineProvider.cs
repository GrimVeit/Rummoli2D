using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalStateMachineProvider
{
    public IState GetState<T>() where T : IState;

    public void SetState(IState state);
}
