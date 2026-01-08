using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private StartPanel_Game startPanel;
    [SerializeField] private PlayersPanel_Game playersPanel;
    [SerializeField] private RummoliTablePanel_Game rummoliTablePanel;
    [SerializeField] private CardBankPanel_Game cardBankPanel;
    [SerializeField] private RummoliPanel_Game rummoliPanel;
    [SerializeField] private RoundPanel_Game roundPanel;
    [SerializeField] private PokerPanel_Game pokerPanel;
    [SerializeField] private LeftPanel_Game leftPanel;
    [SerializeField] private RightPanel_Game rightPanel;

    [SerializeField] private PausePanel_Game pausePanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        startPanel.Initialize();
        playersPanel.Initialize();
        rummoliTablePanel.Initialize();
        cardBankPanel.Initialize();
        rummoliPanel.Initialize();
        roundPanel.Initialize();
        pokerPanel.Initialize();
        leftPanel.Initialize();
        rightPanel.Initialize();

        pausePanel.Initialize();
    }

    public void Activate()
    {
        startPanel.OnClickToPlay += HandleClickToPlay_Start;

        leftPanel.OnClickToExit += HandleClickToExit_Left;

        rightPanel.OnClickToPause += HandleClickToPause_Right;
        rightPanel.OnClickToResults += HandleClickToResults_Right;

        pausePanel.OnClickToResume += HandleClickToResume_Pause;
    }

    public void Deactivate()
    {
        startPanel.OnClickToPlay -= HandleClickToPlay_Start;

        leftPanel.OnClickToExit -= HandleClickToExit_Left;

        rightPanel.OnClickToPause -= HandleClickToPause_Right;
        rightPanel.OnClickToResults -= HandleClickToResults_Right;

        pausePanel.OnClickToResume -= HandleClickToResume_Pause;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        startPanel.Dispose();
        playersPanel.Dispose();
        rummoliTablePanel.Dispose();
        cardBankPanel.Dispose();
        rummoliPanel.Dispose();
        roundPanel.Dispose();
        pokerPanel.Dispose();
        leftPanel.Dispose();
        rightPanel.Dispose();

        pausePanel.Dispose();
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





    public void OpenPlayersPanel()
    {
        if(playersPanel.IsActive) return;

        OpenOtherPanel(playersPanel);
    }

    public void ClosePlayersPanel()
    {
        if(!playersPanel.IsActive) return;

        CloseOtherPanel(playersPanel);
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





    public void OpenCardBankPanel()
    {
        if(cardBankPanel.IsActive) return;

        OpenOtherPanel(cardBankPanel);
    }

    public void CloseCardBankPanel()
    {
        if(!cardBankPanel.IsActive) return;

        CloseOtherPanel(cardBankPanel);
    }





    public void OpenRummoliPanel()
    {
        if(rummoliPanel.IsActive) return;

        OpenOtherPanel(rummoliPanel);
    }

    public void CloseRummoliPanel()
    {
        if (!rummoliPanel.IsActive) return;

        CloseOtherPanel(rummoliPanel);
    }






    public void OpenRoundPanel()
    {
        if(roundPanel.IsActive) return;

        OpenOtherPanel(roundPanel);
    }

    public void CloseRoundPanel()
    {
        if(!roundPanel.IsActive) return;

        CloseOtherPanel(roundPanel);
    }






    public void OpenPokerPanel()
    {
        if (pokerPanel.IsActive) return;

        OpenOtherPanel(pokerPanel);
    }

    public void ClosePokerPanel()
    {
        if(!pokerPanel.IsActive) return;

        CloseOtherPanel(pokerPanel);
    }





    public void OpenLeftPanel()
    {
        if(leftPanel.IsActive) return;

        OpenOtherPanel(leftPanel);
    }

    public void CloseLeftPanel()
    {
        if (!leftPanel.IsActive) return;

        CloseOtherPanel(leftPanel);
    }






    public void OpenRightPanel()
    {
        if(rightPanel.IsActive) return;

        OpenOtherPanel(rightPanel);
    }

    public void CloseRightPanel()
    {
        if(!rightPanel.IsActive) return;

        CloseOtherPanel(rightPanel);
    }






    public void OpenPausePanel()
    {
        if(pausePanel.IsActive) return;

        OpenOtherPanel(pausePanel);
    }

    public void ClosePausePanel()
    {
        if(!pausePanel.IsActive) return;

        CloseOtherPanel(pausePanel);
    }


    #endregion





    #region Output


    #region Start

    public event Action OnClickToPlay_Start;

    private void HandleClickToPlay_Start()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Start?.Invoke();
    }

    #endregion


    #region Left

    public event Action OnClickToExit_Left;

    private void HandleClickToExit_Left()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToExit_Left?.Invoke();
    }

    #endregion


    #region Right

    public event Action OnClickToPause_Right;
    public event Action OnClickToResults_Right;

    private void HandleClickToPause_Right()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPause_Right?.Invoke();
    }

    private void HandleClickToResults_Right()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToResults_Right?.Invoke();
    }

    #endregion




    #region Pause

    public event Action OnClickToResume_Pause;

    private void HandleClickToResume_Pause()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToResume_Pause?.Invoke();
    }

    #endregion

    #endregion
}
