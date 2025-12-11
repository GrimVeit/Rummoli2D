using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTranslateChangePresenter
{
    private readonly TextTranslateChangeModel _model;
    private readonly TextTranslateChangeView _view;

    public TextTranslateChangePresenter(TextTranslateChangeModel model, TextTranslateChangeView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseLanguage += _model.ChooseLanguage;

        _model.OnSetLanguage += _view.SetLanguage;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseLanguage -= _model.ChooseLanguage;

        _model.OnSetLanguage -= _view.SetLanguage;
    }
}
