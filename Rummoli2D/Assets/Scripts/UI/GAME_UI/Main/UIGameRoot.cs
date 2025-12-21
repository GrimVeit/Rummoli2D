using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private StartPanel_Game startPanel;
    [SerializeField] private RummoliTablePanel_Game rummoliTablePanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        startPanel.Initialize();
        rummoliTablePanel.Initialize();
    }

    public void Activate()
    {
        startPanel.OnClickToPlay += HandleClickToPlay_Start;
    }

    public void Deactivate()
    {
        startPanel.OnClickToPlay -= HandleClickToPlay_Start;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        startPanel.Dispose();
        rummoliTablePanel.Dispose();
    }

    #region Input

    public void OpenStartPanel()
    {
        if(startPanel.IsActive) return;

        OpenOtherPanel(startPanel);
    }

    public void CloseStartPanel()
    {
        if(!startPanel.IsActive) return;

        CloseOtherPanel(startPanel);
    }




    public void OpenRummoliTablePanel()
    {
        if(rummoliTablePanel.IsActive) return;

        OpenOtherPanel(rummoliTablePanel);
    }

    public void CloseRummoliTablePanel()
    {
        if(!rummoliTablePanel.IsActive) return;

        CloseOtherPanel(rummoliTablePanel);
    }


    #endregion





    #region Output


    #region StartPanel

    public event Action OnClickToPlay_Start;

    private void HandleClickToPlay_Start()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Start?.Invoke();
    }

    #endregion

    #endregion
}
