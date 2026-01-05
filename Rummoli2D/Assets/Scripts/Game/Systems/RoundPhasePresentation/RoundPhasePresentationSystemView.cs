using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RoundPhasePresentationSystemView : View
{
    [SerializeField] private RoundPresentation roundPresentation;
    [SerializeField] private List<PhaseTextPresentation> phaseTextPresentations;
    [SerializeField] private List<PhaseNamePresentation> phaseNamePresentations;

    [SerializeField] private float durationRoundScale = 0.2f;
    [SerializeField] private float durationPhaseTextScale = 0.2f;
    [SerializeField] private float durationPhaseNameScale = 0.2f;
    [SerializeField] private float durationPhaseNameMove = 0.2f;

    #region Round
    public void ShowRoundOpen(Action OnComplete)
    {
        roundPresentation.ShowOpen(durationRoundScale, OnComplete);
    }

    public void HideRoundOpen(Action OnComplete)
    {
        roundPresentation.HideOpen(durationRoundScale, OnComplete);
    }

    public void ShowRoundComplete(Action OnComplete)
    {
        roundPresentation.ShowComplete(durationRoundScale, OnComplete);
    }

    public void HideRoundComplete(Action OnComplete)
    {
        roundPresentation.HideComplete(durationRoundScale, OnComplete);
    }
    #endregion

    #region TextPhase
    public void ShowTextPhase(int phaseId, Action OnComplete)
    {
        var textPhase = GetPhaseTextPresentation(phaseId);

        if(textPhase == null)
        {
            Debug.Log("Not found PhaseTextPresentation with phaseId - " + phaseId);
            return;
        }

        textPhase.Show(durationPhaseTextScale, OnComplete);
    }

    public void HideTextPhase(int phaseId, Action OnComplete)
    {
        var textPhase = GetPhaseTextPresentation(phaseId);

        if (textPhase == null)
        {
            Debug.Log("Not found PhaseTextPresentation with phaseId - " + phaseId);
            return;
        }

        textPhase.Hide(durationPhaseTextScale, OnComplete);
    }
    #endregion

    #region NamePhase
    public void ShowNamePhase(int phaseId, Action OnComplete)
    {
        var textName = GetPhaseNamePresentation(phaseId);

        if (textName == null)
        {
            Debug.Log("Not found PhaseNamePresentation with phaseId - " + phaseId);
            return;
        }

        textName.Show(durationPhaseNameScale, OnComplete);
    }

    public void HideNamePhase(int phaseId, Action OnComplete)
    {
        var textName = GetPhaseNamePresentation(phaseId);

        if (textName == null)
        {
            Debug.Log("Not found PhaseNamePresentation with phaseId - " + phaseId);
            return;
        }

        textName.Hide(durationPhaseNameScale, OnComplete);
    }

    public void SetToLayoutNamePhase(int phaseId, string key)
    {
        var textName = GetPhaseNamePresentation(phaseId);

        if (textName == null)
        {
            Debug.Log("Not found PhaseNamePresentation with phaseId - " + phaseId);
            return;
        }

        textName.SetToLayout(key);
    }

    public void MoveToLayoutNamePhase(int phaseId, string key, Action OnComplete)
    {
        var textName = GetPhaseNamePresentation(phaseId);

        if (textName == null)
        {
            Debug.Log("Not found PhaseNamePresentation with phaseId - " + phaseId);
            return;
        }

        textName.MoveToLayout(key, durationPhaseNameMove, OnComplete);
    }
    #endregion

    private PhaseTextPresentation GetPhaseTextPresentation(int id)
    {
        return phaseTextPresentations.Find(data => data.PhaseTextId == id);
    }

    private PhaseNamePresentation GetPhaseNamePresentation(int id)
    {
        return phaseNamePresentations.Find(data => data.PhaseNameId == id);
    }
}

[Serializable]
public class RoundPresentation
{
    [SerializeField] private Transform transformRound;
    [SerializeField] private Transform transformCompletedRound;

    private Tween tweenScale;

    public void ShowOpen(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();

        transformRound.gameObject.SetActive(true);
        tweenScale = transformRound.DOScale(1, durationsScale).OnComplete(() => OnComplete?.Invoke());
    }

    public void HideOpen(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();
        tweenScale?.Kill();

        transformRound.gameObject.SetActive(true);
        tweenScale = transformRound.DOScale(0, durationsScale).OnComplete(() =>
        {
            OnComplete?.Invoke();
            transformRound.gameObject.SetActive(false);
        });
    }

    public void ShowComplete(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();

        transformCompletedRound.gameObject.SetActive(true);
        tweenScale = transformCompletedRound.DOScale(1, durationsScale).OnComplete(() => OnComplete?.Invoke());
    }

    public void HideComplete(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();
        tweenScale?.Kill();

        transformCompletedRound.gameObject.SetActive(true);
        tweenScale = transformCompletedRound.DOScale(0, durationsScale).OnComplete(() =>
        {
            OnComplete?.Invoke();
            transformCompletedRound.gameObject.SetActive(false);
        });
    }
}

[Serializable]
public class PhaseTextPresentation
{
    public int PhaseTextId => phaseTextId;

    [SerializeField] private int phaseTextId;
    [SerializeField] private Transform transformRound;

    private Tween tweenScale;

    public void Show(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();

        transformRound.gameObject.SetActive(true);
        tweenScale = transformRound.DOScale(1, durationsScale).OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide(float durationsScale, Action OnComplete)
    {
        tweenScale?.Kill();
        tweenScale?.Kill();

        transformRound.gameObject.SetActive(true);
        tweenScale = transformRound.DOScale(0, durationsScale).OnComplete(() =>
        {
            OnComplete?.Invoke();
            transformRound.gameObject.SetActive(false);
        });
    }
}

[Serializable]
public class PhaseNamePresentation
{
    public int PhaseNameId => phaseNameId;

    [SerializeField] private int phaseNameId;
    [SerializeField] private Transform transformPhaseNameMove;
    [SerializeField] private Transform transformPhaseNameScale;
    [SerializeField] private List<PhaseNamePresentationTransform> phaseNameTransforms = new();

    private Tween tweenScaleMain;
    private Tween tweenScaleOther;
    private Tween tweenMove;

    public void Show(float durationsScale, Action OnComplete)
    {
        tweenScaleMain?.Kill();

        transformPhaseNameMove.gameObject.SetActive(true);
        tweenScaleMain = transformPhaseNameMove.DOScale(1, durationsScale).OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide(float durationsScale, Action OnComplete)
    {
        tweenScaleMain?.Kill();
        tweenScaleMain?.Kill();

        transformPhaseNameMove.gameObject.SetActive(true);
        tweenScaleMain = transformPhaseNameMove.DOScale(0, durationsScale).OnComplete(() =>
        {
            OnComplete?.Invoke();
            transformPhaseNameMove.gameObject.SetActive(false);
        });
    }

    public void SetToLayout(string key)
    {
        tweenMove?.Kill();
        tweenScaleOther?.Kill();

        var transformMove = GetPhaseNamePresentationTransform(key);

        if (transformMove == null)
        {
            Debug.LogWarning($"Not found TransformMove with key - {key}");
            return;
        }

        transformPhaseNameScale.localScale = new Vector3(transformMove.Scale, transformMove.Scale, transformMove.Scale);
        transformPhaseNameMove.localPosition = transformMove.TransformMove.localPosition;
    }

    public void MoveToLayout(string key, float speedMovePlayer, Action OnComplete = null)
    {
        tweenMove?.Kill();
        tweenScaleOther?.Kill();

        var transformMove = GetPhaseNamePresentationTransform(key);

        if (transformMove == null)
        {
            Debug.LogWarning($"Not found TransformMove with key - {key}");
            return;
        }

        tweenScaleOther = transformPhaseNameScale.DOScale(transformMove.Scale, speedMovePlayer);
        tweenMove = transformPhaseNameMove.DOLocalMove(transformMove.TransformMove.localPosition, speedMovePlayer).OnComplete(() => OnComplete?.Invoke());
    }

    private PhaseNamePresentationTransform GetPhaseNamePresentationTransform(string key)
    {
        return phaseNameTransforms.Find(data => data.Key == key);
    }
}

[Serializable]
public class PhaseNamePresentationTransform
{
    [SerializeField] private string key;
    [SerializeField] private Transform transformMove;
    [SerializeField] private float scale;

    public string Key => key;
    public float Scale => scale;
    public Transform TransformMove => transformMove;
}
