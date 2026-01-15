using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectHideShowPresenter : ITextEffectHideShowActivator
{
    private readonly TextEffectHideShowView _view;

    public TextEffectHideShowPresenter(TextEffectHideShowView view)
    {
        _view = view;
    }

    #region Input

    public void ActivateVisual(float duration) => _view.ActivateVisual(duration);
    public void DeactivateVisual(float duration) => _view.DeactivateVisual(duration);

    #endregion
}

public interface ITextEffectHideShowActivator
{
    public void ActivateVisual(float duration = 1f);
    public void DeactivateVisual(float duration = 1f);
}
