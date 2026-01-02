using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPeopleInputView : View
{
    [Header("Choose")]
    [SerializeField] private UIEffect effectButtonChoose;
    [SerializeField] private Button buttonChoose;

    [Header("Pass")]
    [SerializeField] private UIEffect effectButtonPass;
    [SerializeField] private Button buttonPass;

    public void Initialize()
    {
        buttonChoose.onClick.AddListener(Choose);
        buttonPass.onClick.AddListener(Pass);

        effectButtonChoose.Initialize();
        effectButtonPass.Initialize();
    }

    public void Dispose()
    {
        buttonChoose.onClick.RemoveListener(Choose);
        buttonPass.onClick.RemoveListener(Pass);

        effectButtonChoose.Dispose();
        effectButtonPass.Dispose();
    }

    #region Choose

    public void ActivateChoose()
    {
        buttonChoose.enabled = true;
        effectButtonChoose.ActivateEffect();
    }

    public void DeactivateChoose()
    {
        buttonChoose.enabled = false;
        effectButtonChoose.DeactivateEffect();
    }

    #endregion

    #region Choose

    public void ActivatePass()
    {
        buttonPass.enabled = true;
        effectButtonPass.ActivateEffect();
    }

    public void DeactivatePass()
    {
        buttonPass.enabled = false;
        effectButtonPass.DeactivateEffect();
    }

    #endregion

    #region Output

    public event Action OnChoose;
    public event Action OnPass;

    private void Choose()
    {
        OnChoose?.Invoke();
    }

    private void Pass()
    {
        OnPass?.Invoke();
    }

    #endregion
}
