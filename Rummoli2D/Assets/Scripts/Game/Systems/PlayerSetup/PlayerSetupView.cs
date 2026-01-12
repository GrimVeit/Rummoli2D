using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupView : View
{
    [SerializeField] private List<Transform> transformPlayers = new();
    [Header("Points")]
    [SerializeField] private List<Transform> transforms5 = new();
    [SerializeField] private List<Transform> transforms4 = new();
    [SerializeField] private List<Transform> transforms3 = new();
    [SerializeField] private List<Transform> transforms2 = new();

    public void SetCount(int count)
    {
        List<Transform> selectedTransforms = count switch
        {
            2 => transforms2,
            3 => transforms3,
            4 => transforms4,
            5 => transforms5,
            _ => null
        };
        if (selectedTransforms == null)
        {
            Debug.LogError("Unsupported player count: " + count);
            return;
        }
        for (int i = 0; i < selectedTransforms.Count; i++)
        {
            transformPlayers[i].localPosition = selectedTransforms[i].localPosition;
        }
    }
}
