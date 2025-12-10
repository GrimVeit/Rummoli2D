using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsModel
{
    private readonly ISoundVolumeProvider _soundVolumeProvider;

    private readonly ICustomSliderProvider _customSliderProvider_Sound;
    private readonly ICustomSliderProvider _customSliderProvider_Music;

    public VolumeSettingsModel(ISoundVolumeProvider soundVolumeProvider, ICustomSliderProvider customSliderProvider_Sound, ICustomSliderProvider customSliderProvider_Music)
    {
        _soundVolumeProvider = soundVolumeProvider;
        _customSliderProvider_Sound = customSliderProvider_Sound;
        _customSliderProvider_Music = customSliderProvider_Music;

        _customSliderProvider_Sound.OnChangedValue += ChangeVolumeSound;
        _customSliderProvider_Music.OnChangedValue += ChangeVolumeMusic;
    }

    public void Initialize()
    {
        Debug.Log($"SOUND => {_soundVolumeProvider.VolumeSound()}");
        Debug.Log($"MUSIC => {_soundVolumeProvider.VolumeMusic()}");

        _customSliderProvider_Sound.SetValue(_soundVolumeProvider.VolumeSound());
        _customSliderProvider_Music.SetValue(_soundVolumeProvider.VolumeMusic());
    }

    public void Dispose()
    {
        _customSliderProvider_Sound.OnChangedValue -= ChangeVolumeSound;
        _customSliderProvider_Music.OnChangedValue -= ChangeVolumeMusic;
    }

    private void ChangeVolumeSound(float value)
    {
        _soundVolumeProvider.SetVolume(value, AudioType.Sound);
    }

    private void ChangeVolumeMusic(float value)
    {
        _soundVolumeProvider.SetVolume(value, AudioType.Music);
    }
}
