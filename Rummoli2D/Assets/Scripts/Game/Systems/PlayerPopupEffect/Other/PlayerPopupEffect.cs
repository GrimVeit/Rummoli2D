using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerPopupEffect : MonoBehaviour
{
    public void Activate(float pulseTime = 1f, float pulseScale = 0.15f, float pulseSpeed = 0.3f, float killTime = 0.3f)
    {
        //float pulseTime = 2f;      // сколько секунд пульсирует
        //float pulseScale = 0.15f;  // сила пульса
        //float pulseSpeed = 0.3f;   // скорость пульса
        //float killTime = 0.3f;     // врем€ схлопывани€

        Vector3 baseScale = transform.localScale;

        // Ѕесконечна€ пульсаци€
        Tween pulse = transform
            .DOScale(baseScale * (1f + pulseScale), pulseSpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // „ерез pulseTime секунд останавливаем пульс и уничтожаем
        DOVirtual.DelayedCall(pulseTime, () =>
        {
            pulse.Kill();
            transform
                .DOScale(Vector3.zero, killTime)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject));
        });
    }
}
