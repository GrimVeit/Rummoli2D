using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RummoliTablePresentationSystemView : View
{
    [SerializeField] private List<RummoliTablePresentationTransform> rummoliTablePresentationTransforms = new();

    [SerializeField] private Transform transformRummoliTableMoveParent;
    [SerializeField] private Transform transformRummoliTableScaleMoveLayout;
    [SerializeField] private Transform transformRummoliTableScaleHideShow;

    [SerializeField] private float speedScaleRummoliTable;
    [SerializeField] private float speedMoveRummoliTable;

    private Tween tweenMoveParentRummoliTable;
    private Tween tweenScaleMoveLayoutRummoliTable;

    private Tween tweenScaleRummoliTableHideShow;

    public void Show(Action OnComplete)
    {
        tweenScaleRummoliTableHideShow?.Kill();

        transformRummoliTableScaleHideShow.gameObject.SetActive(true);
        tweenScaleRummoliTableHideShow = transformRummoliTableScaleHideShow.DOScale(1, speedScaleRummoliTable).OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide(Action OnComplete)
    {
        tweenScaleRummoliTableHideShow?.Kill();

        tweenScaleRummoliTableHideShow = transformRummoliTableScaleHideShow.DOScale(0, speedScaleRummoliTable).OnComplete(() =>
        {
            transformRummoliTableScaleHideShow.gameObject.SetActive(false);
            OnComplete?.Invoke();
        });
    }

    public void MoveToLayout(string key, Action OnComplete = null)
    {
        tweenMoveParentRummoliTable?.Kill();
        tweenScaleMoveLayoutRummoliTable?.Kill();

        var transformMove = GetRummoliTablePresentationTransform(key);

        if (transformMove == null)
        {
            Debug.LogWarning($"Not found TransformMove by RummoliTable with key - {key}");
            return;
        }

        tweenMoveParentRummoliTable = transformRummoliTableMoveParent.DOLocalMove(transformMove.TransformMove.localPosition, speedMoveRummoliTable).OnComplete(() => OnComplete?.Invoke());
        tweenScaleMoveLayoutRummoliTable = transformRummoliTableScaleMoveLayout.DOScale(transformMove.VectorScale, speedMoveRummoliTable);
    }

    private RummoliTablePresentationTransform GetRummoliTablePresentationTransform(string key)
    {
        return rummoliTablePresentationTransforms.Find(data => data.Key == key);
    }
}

[System.Serializable]
public class RummoliTablePresentationTransform
{
    [SerializeField] private string key;
    [SerializeField] private Transform transformMove;
    [SerializeField] private Vector3 vectorScale;

    public string Key => key;
    public Transform TransformMove => transformMove;
    public Vector3 VectorScale => vectorScale;
}
