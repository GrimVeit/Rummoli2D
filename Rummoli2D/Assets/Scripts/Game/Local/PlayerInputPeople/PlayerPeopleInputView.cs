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

    [Header("Transforms")]
    [SerializeField] private Transform transformParentChoose;
    [SerializeField] private Transform transformParentPass;
    [SerializeField] private Transform transformMain;
    [SerializeField] private Transform transformSecond;

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

    public void SetMainPass()
    {
        transformParentChoose.transform.localPosition = transformSecond.transform.localPosition;
        transformParentPass.transform.localPosition = transformMain.transform.localPosition;
    }

    public void SetMainChoose()
    {
        transformParentChoose.transform.localPosition = transformMain.transform.localPosition;
        transformParentPass.transform.localPosition = transformSecond.transform.localPosition;
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

    #region Pass

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
