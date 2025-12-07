using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorUnit : MonoBehaviour
{
    [Header("Unit")]
    [SerializeField] private bool isCenter;
    [SerializeField] private Transform transformUnit;
    [SerializeField] private float timeScaleUnitDuration = 0.5f;
    [SerializeField] private Vector3 vectorNormalPos;
    [SerializeField] private Vector3 vectorCenterPos;
    [SerializeField] private float timeUnitMoveToCenter;

    [Header("Door")]
    [SerializeField] private Transform transformDoor;
    [SerializeField] private float timeDoorMoveDuration = 0.5f;
    [SerializeField] private Vector3 vectorActiveDoorPosition;
    [SerializeField] private Vector3 vectorDeactiveDoorPosition;
    [SerializeField] private Vector3 scaleSizeOpenDoor;
    [SerializeField] private float timeScaleOpenDoor;

    [Header("Overlap Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float overlapPercentActivateDeactivate = 0.6f;
    [Range(0f, 1f)]
    [SerializeField] private float overlapPercentOpen = 0.6f;// Запуск 2-й анимации на 80% первой

    private Sequence sequenceUnit;

    public void Activate(Action OnComplete = null)
    {
        sequenceUnit?.Kill();

        transformDoor.localPosition = vectorDeactiveDoorPosition;
        transformDoor.localScale = Vector3.one;
        transformUnit.localPosition = vectorNormalPos;
        transformUnit.localScale = Vector3.zero;

        sequenceUnit = DOTween.Sequence();
        sequenceUnit
            .Append(transformUnit.DOScale(1, timeScaleUnitDuration))
            .Insert(timeScaleUnitDuration * overlapPercentActivateDeactivate,
                transformDoor.DOLocalMove(vectorActiveDoorPosition, timeDoorMoveDuration).OnStart(() => OnSoundMoveUp?.Invoke())
            .OnComplete(() => OnComplete?.Invoke()));
    }

    public void Deactivate(Action OnComplete = null)
    {
        sequenceUnit?.Kill();

        transformDoor.localPosition = vectorActiveDoorPosition;
        transformDoor.localScale = Vector3.one;
        transformUnit.localPosition = vectorNormalPos;
        transformUnit.localScale = Vector3.one;

        sequenceUnit = DOTween.Sequence();
        sequenceUnit
            .Append(transformDoor.DOLocalMove(vectorDeactiveDoorPosition, timeDoorMoveDuration))
            .Insert(timeDoorMoveDuration * overlapPercentActivateDeactivate,
                transformUnit.DOScale(0, timeScaleUnitDuration)).OnStart(() => OnSoundMoveDown?.Invoke())
            .OnComplete(() => OnComplete?.Invoke());
    }

    public void Hide()
    {
        sequenceUnit?.Kill();

        transformDoor.localPosition = vectorDeactiveDoorPosition;
        transformDoor.localScale = Vector3.one;
        transformUnit.localPosition = vectorNormalPos;
        transformUnit.localScale = Vector3.zero;
    }

    public void Open(Action OnComplete = null)
    {
        sequenceUnit?.Kill();

        transformDoor.localPosition = vectorActiveDoorPosition;
        transformDoor.localScale = Vector3.one;
        transformUnit.localPosition = vectorNormalPos;
        transformUnit.localScale = Vector3.one;

        sequenceUnit = DOTween.Sequence();

        if (!isCenter)
            sequenceUnit.Append(transformUnit.DOLocalMove(vectorCenterPos, timeUnitMoveToCenter));
        else
            sequenceUnit.Append(transformUnit.DOLocalMove(vectorCenterPos, timeUnitMoveToCenter/3));

        sequenceUnit.AppendCallback(() => OnSoundOpen?.Invoke());

        sequenceUnit
            .Insert(timeUnitMoveToCenter * overlapPercentOpen, transformDoor.DOScale(scaleSizeOpenDoor, timeScaleOpenDoor))
            .OnComplete(() =>
            {
                OnComplete?.Invoke();
                //OnSoundOpen?.Invoke();
            });
    }

    #region Output

    public event Action OnSoundMoveUp;
    public event Action OnSoundMoveDown;
    public event Action OnSoundOpen;

    #endregion
}
