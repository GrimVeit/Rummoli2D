using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private RulesPanel_Menu rulesPanel;
    [SerializeField] private ProfilePanel_Menu profilePanel;
    [SerializeField] private BalancePanel_Menu balancePanel;
    [SerializeField] private SettingsPanel_Menu settingsPanel;
    [SerializeField] private LeaderboardPanel_Menu leaderboardPanel;
    [SerializeField] private ResetGlobalPanel_Menu resetGlobalPanel;
    [SerializeField] private NewGamePanel_Menu newGamePanel;

    [Header("Shop")]
    [SerializeField] private ShopPanel_Game shopPanel;
    [SerializeField] private ShopBackgroundPanel_Menu shopBackgroundPanel;
    [SerializeField] private ShopCardsPanel_Menu shopCardsPanel;

    [Header("Registration")]
    [SerializeField] private RegistrationPanel_Menu registrationPanel;
    [SerializeField] private LoadingPanel_Menu loadRegistrationPanel;

    private ISoundProvider _soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        rulesPanel.Initialize();
        profilePanel.Initialize();
        balancePanel.Initialize();
        settingsPanel.Initialize();
        leaderboardPanel.Initialize();
        resetGlobalPanel.Initialize();
        newGamePanel.Initialize();

        shopPanel.Initialize();
        shopBackgroundPanel.Initialize();
        shopCardsPanel.Initialize();

        registrationPanel.Initialize();
        loadRegistrationPanel.Initialize();
    }

    public void Activate()
    {
        registrationPanel.OnClickToRegistrate += HandleClickToRegistrate_Registration;

        mainPanel.OnClickToLeaderboard += HandleClickToLeaderboard_Main;
        mainPanel.OnClickToRules += HandleClickToRules_Main;
        mainPanel.OnClickToProfile += HandleClickToProfile_Main;
        mainPanel.OnClickToBalance += HandleClickToBalance_Main;
        mainPanel.OnClickToSettings += HandleClickToSettings_Main;
        mainPanel.OnClickToShop += HandleClickToShop_Main;
        mainPanel.OnClickToNewGame += HandleClickToNewGame_Main;

        newGamePanel.OnClickToPlay += HandleClickToPlay_NewGame;
        newGamePanel.OnClickToBack += HandleClickToBack_NewGame;

        resetGlobalPanel.OnClickToBack += HandleClickToBack_ResetProgress;
        resetGlobalPanel.OnClickToReset += HandleClickToReset_ResetProgress;
        leaderboardPanel.OnClickToBack += HandleClickToBack_Leaderboard;
        rulesPanel.OnClickToBack += HandleClickToBack_Rules;
        profilePanel.OnClickToBack += HandleClickToBack_Profile;
        balancePanel.OnClickToBack += HandleClickToBack_Balance;
        balancePanel.OnClickToShop += HandleClickToShop_Balance;
        balancePanel.OnClickToPlay += HandleClickToPlay_Balance;
        settingsPanel.OnClickToBack += HandleClickToBack_Settings;
        shopPanel.OnClickToBack += HandleClickToBack_Shop;
    }


    public void Deactivate()
    {
        registrationPanel.OnClickToRegistrate -= HandleClickToRegistrate_Registration;

        mainPanel.OnClickToLeaderboard -= HandleClickToLeaderboard_Main;
        mainPanel.OnClickToRules -= HandleClickToRules_Main;
        mainPanel.OnClickToProfile -= HandleClickToProfile_Main;
        mainPanel.OnClickToBalance -= HandleClickToBalance_Main;
        mainPanel.OnClickToSettings -= HandleClickToSettings_Main;
        mainPanel.OnClickToShop -= HandleClickToShop_Main;
        mainPanel.OnClickToNewGame -= HandleClickToNewGame_Main;

        newGamePanel.OnClickToPlay -= HandleClickToPlay_NewGame;
        newGamePanel.OnClickToBack -= HandleClickToBack_NewGame;

        resetGlobalPanel.OnClickToBack -= HandleClickToBack_ResetProgress;
        resetGlobalPanel.OnClickToReset -= HandleClickToReset_ResetProgress;
        leaderboardPanel.OnClickToBack -= HandleClickToBack_Leaderboard;
        rulesPanel.OnClickToBack -= HandleClickToBack_Rules;
        profilePanel.OnClickToBack -= HandleClickToBack_Profile;
        balancePanel.OnClickToBack -= HandleClickToBack_Balance;
        balancePanel.OnClickToShop -= HandleClickToShop_Balance;
        balancePanel.OnClickToPlay -= HandleClickToPlay_Balance;
        settingsPanel.OnClickToBack -= HandleClickToBack_Settings;
        shopPanel.OnClickToBack -= HandleClickToBack_Shop;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseBalancePanel();
        CloseLoadRegistrationPanel();
        CloseMainPanel();
        CloseNewGamePanel();
        CloseProfilePanel();
        CloseRegistrationPanel();
        CloseRulesPanel();
        CloseSettingsPanel();
        CloseShopPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        rulesPanel.Dispose();
        profilePanel.Dispose();
        balancePanel.Dispose();
        settingsPanel.Dispose();
        leaderboardPanel.Dispose();
        resetGlobalPanel.Dispose();
        newGamePanel.Dispose();

        shopPanel.Dispose();
        shopBackgroundPanel.Dispose();
        shopCardsPanel.Dispose();

        registrationPanel.Dispose();
        loadRegistrationPanel.Dispose();
    }

    #region OTHERS

    public void OpenMainPanel()
    {
        if (mainPanel.IsActive) return;

        OpenOtherPanel(mainPanel);
    }

    public void CloseMainPanel()
    {
        if (!mainPanel.IsActive) return;

        CloseOtherPanel(mainPanel);
    }





    public void OpenNewGamePanel()
    {
        if (newGamePanel.IsActive) return;

        OpenOtherPanel(newGamePanel);
    }

    public void CloseNewGamePanel()
    {
        if (!newGamePanel.IsActive) return;

        CloseOtherPanel(newGamePanel);
    }




    public void OpenRulesPanel()
    {
        if(rulesPanel.IsActive) return;

        OpenOtherPanel(rulesPanel);
    }

    public void CloseRulesPanel()
    {
        if(!rulesPanel.IsActive) return;

        CloseOtherPanel(rulesPanel);
    }




    public void OpenProfilePanel()
    {
        if (profilePanel.IsActive) return;

        OpenOtherPanel(profilePanel);
    }

    public void CloseProfilePanel()
    {
        if(!profilePanel.IsActive) return;

        CloseOtherPanel(profilePanel);
    }






    public void OpenBalancePanel()
    {
        if (balancePanel.IsActive) return;

        OpenOtherPanel(balancePanel);
    }

    public void CloseBalancePanel()
    {
        if (!balancePanel.IsActive) return;

        CloseOtherPanel(balancePanel);
    }





    public void OpenSettingsPanel()
    {
        if (settingsPanel.IsActive) return;

        OpenOtherPanel(settingsPanel);
    }

    public void CloseSettingsPanel()
    {
        if (!settingsPanel.IsActive) return;

        CloseOtherPanel(settingsPanel);
    }






    public void OpenResetProgressPanel()
    {
        if(resetGlobalPanel.IsActive) return;

        OpenOtherPanel(resetGlobalPanel);
    }

    public void CloseResetProgressPanel()
    {
        if (!resetGlobalPanel.IsActive) return;

        CloseOtherPanel(resetGlobalPanel);
    }





    public void OpenLeaderboardPanel()
    {
        if (leaderboardPanel.IsActive) return;

        OpenOtherPanel(leaderboardPanel);
    }

    public void CloseLeaderboardPanel()
    {
        if (!leaderboardPanel.IsActive) return;

        CloseOtherPanel(leaderboardPanel);
    }




    public void OpenShopPanel()
    {
        if (shopPanel.IsActive) return;

        OpenOtherPanel(shopPanel);
    }

    public void CloseShopPanel()
    {
        if (!shopPanel.IsActive) return;

        CloseOtherPanel(shopPanel);
    }


    //--------------------------------------------//



    public void OpenRegistrationPanel()
    {
        if (registrationPanel.IsActive) return;

        OpenOtherPanel(registrationPanel);
    }

    public void CloseRegistrationPanel()
    {
        if (!registrationPanel.IsActive) return;

        CloseOtherPanel(registrationPanel);
    }


    public void OpenLoadRegistrationPanel()
    {
        if (loadRegistrationPanel.IsActive) return;

        OpenOtherPanel(loadRegistrationPanel);
    }

    public void CloseLoadRegistrationPanel()
    {
        if (!loadRegistrationPanel.IsActive) return;

        CloseOtherPanel(loadRegistrationPanel);
    }

    #endregion


    #region Output

    #region OTHER

    public event Action OnClickToRegistrate_Registration;

    private void HandleClickToRegistrate_Registration()
    {
        OnClickToRegistrate_Registration?.Invoke();
    }

    #endregion

    #region MainPanel

    public event Action OnClickToLeaderboard_Main;
    public event Action OnClickToRules_Main;
    public event Action OnClickToProfile_Main;
    public event Action OnClickToBalance_Main;
    public event Action OnClickToSettings_Main;
    public event Action OnClickToShop_Main;
    public event Action OnClickToNewGame_Main;

    private void HandleClickToLeaderboard_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToLeaderboard_Main?.Invoke();
    }

    private void HandleClickToRules_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToRules_Main?.Invoke();
    }

    private void HandleClickToProfile_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToProfile_Main?.Invoke();
    }

    private void HandleClickToBalance_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBalance_Main?.Invoke();
    }

    private void HandleClickToSettings_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToSettings_Main?.Invoke();
    }

    private void HandleClickToShop_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToShop_Main?.Invoke();
    }

    private void HandleClickToNewGame_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToNewGame_Main?.Invoke();
    }

    #endregion

    #region NewGame

    public event Action OnClickToPlay_NewGame;
    public event Action OnClickToBack_NewGame;

    private void HandleClickToPlay_NewGame()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_NewGame?.Invoke();
    }

    private void HandleClickToBack_NewGame()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_NewGame?.Invoke();
    }

    #endregion

    #region LeaderboardPanel

    public event Action OnClickToBack_ResetProgress;
    public event Action OnClickToReset_ResetProgress;

    private void HandleClickToBack_ResetProgress()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_ResetProgress?.Invoke();
    }

    private void HandleClickToReset_ResetProgress()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToReset_ResetProgress?.Invoke();
    }

    #endregion


    #region LeaderboardPanel

    public event Action OnClickToBack_Leaderboard;

    private void HandleClickToBack_Leaderboard()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Leaderboard?.Invoke();
    }

    #endregion

    #region RulesPanel

    public event Action OnClickToBack_Rules;

    private void HandleClickToBack_Rules()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Rules?.Invoke();
    }

    #endregion

    #region ProfilePanel

    public event Action OnClickToBack_Profile;

    private void HandleClickToBack_Profile()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Profile?.Invoke();
    }

    #endregion

    #region BalancePanel

    public event Action OnClickToBack_Balance;
    public event Action OnClickToShop_Balance;
    public event Action OnClickToPlay_Balance;

    private void HandleClickToBack_Balance()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Balance?.Invoke();
    }

    private void HandleClickToShop_Balance()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToShop_Balance?.Invoke();
    }

    private void HandleClickToPlay_Balance()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Balance?.Invoke();
    }

    #endregion

    #region SettingsPanel

    public event Action OnClickToBack_Settings;

    private void HandleClickToBack_Settings()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Settings?.Invoke();
    }

    #endregion

    #region ShopPanel

    public event Action OnClickToBack_Shop;

    private void HandleClickToBack_Shop()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Shop?.Invoke();
    }

    #endregion

    #endregion

}
