using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private DoorsPanel_Game doorsPanel;
    [SerializeField] private WinPanel_Game winPanel;
    [SerializeField] private LosePanel_Game losePanel;

    [Header("Nothing Door")]
    [SerializeField] private DoorNothingPanel_Game doorNothingPanel;
    [SerializeField] private DoorNothingBackgroundPanel_Game doorNothingBackgroundPanel;

    [Header("Danger Door")]
    [SerializeField] private DoorDangerPanel_Game doorDangerPanel;

    [Header("Bonus Door")]
    [SerializeField] private NoApplyBonusPanel_Game noApplyBonusPanel;
    [SerializeField] private ChooseDoorForApplyPanel_Game chooseDoorForApplyPanel;
    [SerializeField] private DoorBonusPanel_Game doorBonusPanel;
    [SerializeField] private DoorBonusBackgroundPanel_Game doorBonusBackgroundPanel;
    [SerializeField] private BonusRewardPanel_Game bonusRewardPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        footerPanel.Initialize();
        doorsPanel.Initialize();
        winPanel.Initialize();
        losePanel.Initialize();

        doorNothingPanel.Initialize();
        doorNothingBackgroundPanel.Initialize();

        doorDangerPanel.Initialize();

        noApplyBonusPanel.Initialize();
        chooseDoorForApplyPanel.Initialize();
        doorBonusPanel.Initialize();
        doorBonusBackgroundPanel.Initialize();
        bonusRewardPanel.Initialize();
    }

    public void Activate()
    {
        //mainPanel.OnClickToExit += HandleClickToExit_Main;
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit_Main;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseMainPanel();
        CloseFooterPanel();
        CloseDoorNothingPanel();
        CloseDoorNothingBaackgroundPanel();
        CloseDoorDangerPanel();
        CloseDoorBonusPanel();
        CloseDoorBonusBackgroundPanel();
        CloseBonusRewardPanel();
        CloseChooseDoorForApplyPanel();
        CloseNoApplyBonusPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
        doorsPanel.Dispose();
        winPanel.Dispose();
        losePanel.Dispose();

        doorNothingPanel.Dispose();
        doorNothingBackgroundPanel.Dispose();

        doorDangerPanel.Dispose();

        noApplyBonusPanel.Dispose();
        chooseDoorForApplyPanel.Dispose();
        doorBonusPanel.Dispose();
        doorBonusBackgroundPanel.Dispose();
        bonusRewardPanel.Dispose();
    }

    #region Input


    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if(!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }

    public void OpenFooterPanel()
    {
        if(footerPanel.IsActive) return;

        OpenOtherPanel(footerPanel);
    }

    public void CloseFooterPanel()
    {
        if(!footerPanel.IsActive) return;

        CloseOtherPanel(footerPanel);
    }

    public void OpenDoorsPanel()
    {
        if(doorsPanel.IsActive) return;

        OpenOtherPanel(doorsPanel);
    }

    public void CloseDoorsPanel()
    {
        if (!doorsPanel.IsActive) return;

        CloseOtherPanel(doorsPanel);
    }

    public void OpenWinPanel()
    {
        if(winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if(!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }

    public void OpenLosePanel()
    {
        if(losePanel.IsActive) return;

        OpenOtherPanel(losePanel);
    }

    public void CloseLosePanel()
    {
        if(!losePanel.IsActive) return;

        CloseOtherPanel(losePanel);
    }




    public void OpenDoorNothingPanel()
    {
        if(doorNothingPanel.IsActive) return;

        OpenOtherPanel(doorNothingPanel);
    }

    public void CloseDoorNothingPanel()
    {
        if(!doorNothingPanel.IsActive) return;

        CloseOtherPanel(doorNothingPanel);
    }

    public void OpenDoorNothingBackgroundPanel()
    {
        if(doorNothingBackgroundPanel.IsActive) return;

        OpenOtherPanel(doorNothingBackgroundPanel);
    }

    public void CloseDoorNothingBaackgroundPanel()
    {
        if(!doorNothingBackgroundPanel.IsActive) return;

        CloseOtherPanel(doorNothingBackgroundPanel);
    }



    public void OpenDoorDangerPanel()
    {
        if(doorDangerPanel.IsActive) return;

        OpenOtherPanel(doorDangerPanel);
    }

    public void CloseDoorDangerPanel()
    {
        if(!doorDangerPanel.IsActive) return;

        CloseOtherPanel(doorDangerPanel);
    }



    public void OpenDoorBonusPanel()
    {
        if(doorBonusPanel.IsActive) return;

        OpenOtherPanel(doorBonusPanel);
    }

    public void CloseDoorBonusPanel()
    {
        if(!doorBonusPanel.IsActive) return;

        CloseOtherPanel(doorBonusPanel);
    }

    public void OpenDoorBonusBackgroundPanel()
    {
        if(doorBonusBackgroundPanel.IsActive) return;

        OpenOtherPanel(doorBonusBackgroundPanel);
    }

    public void CloseDoorBonusBackgroundPanel()
    {
        if (!doorBonusBackgroundPanel.IsActive) return;

        CloseOtherPanel(doorBonusBackgroundPanel);
    }

    public void OpenBonusRewardPanel()
    {
        if(bonusRewardPanel.IsActive) return;

        OpenOtherPanel(bonusRewardPanel);
    }

    public void CloseBonusRewardPanel()
    {
        if(!bonusRewardPanel.IsActive) return;

        CloseOtherPanel(bonusRewardPanel);
    }

    public void OpenChooseDoorForApplyPanel()
    {
        if(chooseDoorForApplyPanel.IsActive) return;

        OpenOtherPanel(chooseDoorForApplyPanel);
    }

    public void CloseChooseDoorForApplyPanel()
    {
        if(!chooseDoorForApplyPanel.IsActive) return;

        CloseOtherPanel(chooseDoorForApplyPanel);
    }

    public void OpenNoApplyBonusPanel()
    {
        if(noApplyBonusPanel.IsActive) return;

        OpenOtherPanel(noApplyBonusPanel);
    }

    public void CloseNoApplyBonusPanel()
    {
        if (!noApplyBonusPanel.IsActive) return;

        CloseOtherPanel(noApplyBonusPanel);
    }

    #endregion





    #region Output


    public event Action OnClickToExit_Main;

    private void HandleClickToExit_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToExit_Main?.Invoke();
    }


    #endregion
}
