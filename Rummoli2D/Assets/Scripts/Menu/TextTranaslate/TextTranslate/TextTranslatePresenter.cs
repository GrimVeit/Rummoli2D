using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTranslatePresenter
{
    private readonly TextTranslateModel _model;
    private readonly TextTranslateView _view;

    public TextTranslatePresenter(TextTranslateModel model, TextTranslateView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnChangeLanguage += _view.SetLanguage;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeLanguage -= _view.SetLanguage;
    }
}
