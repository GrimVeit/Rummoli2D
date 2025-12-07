using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private int idDoor;

    private bool isActiveInteraction = false;

    public void ActivateInteraction()
    {
        isActiveInteraction = true;
    }

    public void DeactivateInteraction()
    {
        isActiveInteraction = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isActiveInteraction) return;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isActiveInteraction) return;

        OnChooseDoor?.Invoke(idDoor);
    }

    #region Output

    public event Action<int> OnChooseDoor;

    #endregion
}
