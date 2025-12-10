using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsPresenter
{
    private readonly VolumeSettingsModel _model;

    public VolumeSettingsPresenter(VolumeSettingsModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }
}
