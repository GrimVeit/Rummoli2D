using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPhasePresentationSystemPresenter : IRoundPhasePresentationSystemProvider
{
    private readonly RoundPhasePresentationSystemModel _model;
    private readonly RoundPhasePresentationSystemView _view;

    public RoundPhasePresentationSystemPresenter(RoundPhasePresentationSystemModel model, RoundPhasePresentationSystemView view)
    {
        _model = model;
        _view = view;
    }

    #region Input

    public void ShowRound(Action OnComplete = null) => _view.ShowRound(OnComplete);
    public void HideRound(Action OnComplete = null) => _view.HideRound(OnComplete);


    public void ShowTextPhase(int phaseId, Action OnComplete = null) => _view.ShowTextPhase(phaseId, OnComplete);
    public void HideTextPhase(int phaseId, Action OnComplete = null) => _view.HideTextPhase(phaseId, OnComplete);


    public void ShowNamePhase(int phaseNameId, Action OnComplete = null) => _view.ShowNamePhase(phaseNameId, OnComplete);
    public void HideNamePhase(int phaseNameId, Action OnComplete = null) => _view.HideNamePhase(phaseNameId, OnComplete);
    public void SetToLayoutNamePhase(int phaseId, string key) => _view.SetToLayoutNamePhase(phaseId, key);
    public void MoveToLayoutNamePhase(int phaseId, string key, Action OnComplete = null) => _view.MoveToLayoutNamePhase(phaseId, key, OnComplete);

    #endregion
}

public interface IRoundPhasePresentationSystemProvider
{
    public void ShowRound(Action OnComplete = null);
    public void HideRound(Action OnComplete = null);


    public void ShowTextPhase(int phaseId, Action OnComplete = null);
    public void HideTextPhase(int phaseId, Action OnComplete = null);


    public void ShowNamePhase(int phaseNameId, Action OnComplete = null);
    public void HideNamePhase(int phaseNameId, Action OnComplete = null);
    public void SetToLayoutNamePhase(int phaseId, string key);
    public void MoveToLayoutNamePhase(int phaseId, string key, Action OnComplete = null);
}
