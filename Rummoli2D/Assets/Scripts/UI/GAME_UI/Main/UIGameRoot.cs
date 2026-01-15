using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private PlayersPanel_Game playersPanel;
    [SerializeField] private RummoliTablePanel_Game rummoliTablePanel;
    [SerializeField] private CardBankPanel_Game cardBankPanel;
    [SerializeField] private RummoliPanel_Game rummoliPanel;
    [SerializeField] private RoundPanel_Game roundPanel;
    [SerializeField] private PokerPanel_Game pokerPanel;
    [SerializeField] private LeftPanel_Game leftPanel;
    [SerializeField] private RightPanel_Game rightPanel;

    [SerializeField] private PausePanel_Game pausePanel;
    [SerializeField] private ResultsPanel_Game resultsPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        playersPanel.Initialize();
        rummoliTablePanel.Initialize();
        cardBankPanel.Initialize();
        rummoliPanel.Initialize();
        roundPanel.Initialize();
        pokerPanel.Initialize();
        leftPanel.Initialize();
        rightPanel.Initialize();

        pausePanel.Initialize();
        resultsPanel.Initialize();
    }

    public void Activate()
    {
        leftPanel.OnClickToExit += HandleClickToExit_Left;

        rightPanel.OnClickToPause += HandleClickToPause_Right;
        rightPanel.OnClickToResults += HandleClickToResults_Right;

        pausePanel.OnClickToResume += HandleClickToResume_Pause;
        resultsPanel.OnClickToResume += HandleClickToResume_Results;
    }

    public void Deactivate()
    {
        leftPanel.OnClickToExit -= HandleClickToExit_Left;

        rightPanel.OnClickToPause -= HandleClickToPause_Right;
        rightPanel.OnClickToResults -= HandleClickToResults_Right;

        pausePanel.OnClickToResume -= HandleClickToResume_Pause;
        resultsPanel.OnClickToResume -= HandleClickToResume_Results;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        playersPanel.Dispose();
        rummoliTablePanel.Dispose();
        cardBankPanel.Dispose();
        rummoliPanel.Dispose();
        roundPanel.Dispose();
        pokerPanel.Dispose();
        leftPanel.Dispose();
        rightPanel.Dispose();

        pausePanel.Dispose();
        resultsPanel.Dispose();
    }

    #region Input




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




    public void OpenResultsPanel()
    {
        if (resultsPanel.IsActive) return;

        OpenOtherPanel(resultsPanel);
    }

    public void CloseResultsPanel()
    {
        if (!resultsPanel.IsActive) return;

        CloseOtherPanel(resultsPanel);
    }


    #endregion





    #region Output


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



    #region Results

    public event Action OnClickToResume_Results;

    private void HandleClickToResume_Results()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToResume_Results?.Invoke();
    }

    #endregion

    #endregion
}
