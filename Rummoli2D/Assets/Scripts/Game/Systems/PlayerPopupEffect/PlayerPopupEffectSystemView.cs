using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPopupEffectSystemView : View
{
    [SerializeField] private PlayerPopupEffect effectPrefab;
    [SerializeField] private PlayerTransforms playerTransforms;

    public void ShowPass(int playerId)
    {
        var playerTransform = playerTransforms.GetTransformPlayer(playerId);

        if(playerTransform == null)
        {
            Debug.LogWarning("Not found PlayerTransform with PlayerId - " + playerId);
            return;
        }

        var prefab = Instantiate(effectPrefab, playerTransform);
        prefab.transform.localEulerAngles = new Vector3(0, 0, Random.Range(-30, 30));
        prefab.transform.localPosition = Vector3.zero;
        prefab.Activate();
    }
}
