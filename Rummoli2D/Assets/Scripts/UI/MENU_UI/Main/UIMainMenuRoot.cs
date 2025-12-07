using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private LeaderboardPanel_Menu leaderboardPanel;
    [SerializeField] private ProfilePanel_Menu profilePanel;
    [SerializeField] private ShopPanel_Game shopPanel;
    [SerializeField] private MovePanel backgroundPanelMain;
    [SerializeField] private MovePanel backgroundPanelSecond;

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
        leaderboardPanel.Initialize();
        profilePanel.Initialize();
        shopPanel.Initialize();

        registrationPanel.Initialize();
        loadRegistrationPanel.Initialize();
    }

    public void Activate()
    {
        registrationPanel.OnClickToRegistrate += HandleClickToRegistrate_Registration;

        leaderboardPanel.OnClickToBack += HandleClickToBack_Leaderboard;
        profilePanel.OnClickToBack += HandleClickToBack_Profile;
        shopPanel.OnClickToBack += HandleClickToBack_Shop;

        mainPanel.OnClickToLeaderboard += HandleClickToLeaderboard_Main;
        mainPanel.OnClickToShop += HandleClickToShop_Main;
        mainPanel.OnClickToPlay += HandleClickToPlay_Main;
        mainPanel.OnClickToProfile += HandleClickToProfile_Main;
    }


    public void Deactivate()
    {
        registrationPanel.OnClickToRegistrate -= HandleClickToRegistrate_Registration;

        leaderboardPanel.OnClickToBack -= HandleClickToBack_Leaderboard;
        profilePanel.OnClickToBack -= HandleClickToBack_Profile;
        shopPanel.OnClickToBack -= HandleClickToBack_Shop;

        mainPanel.OnClickToLeaderboard -= HandleClickToLeaderboard_Main;
        mainPanel.OnClickToShop -= HandleClickToShop_Main;
        mainPanel.OnClickToPlay -= HandleClickToPlay_Main;
        mainPanel.OnClickToProfile -= HandleClickToProfile_Main;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);

        CloseBackgroundMainPanel();
        CloseBackgroundSecondPanel();
        CloseLeaderboardPanel();
        CloseShopPanel();
        CloseProfilePanel();
        CloseLoadRegistrationPanel();
        CloseMainPanel();
        CloseRegistrationPanel();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        leaderboardPanel.Dispose();
        profilePanel.Dispose();
        shopPanel.Dispose();

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


    public void OpenBackgroundMainPanel()
    {
        if (backgroundPanelMain.IsActive) return;

        OpenOtherPanel(backgroundPanelMain);
    }

    public void CloseBackgroundMainPanel()
    {
        if (!backgroundPanelMain.IsActive) return;

        CloseOtherPanel(backgroundPanelMain);
    }


    public void OpenBackgroundSecondPanel()
    {
        if (backgroundPanelSecond.IsActive) return;

        OpenOtherPanel(backgroundPanelSecond);
    }

    public void CloseBackgroundSecondPanel()
    {
        if (!backgroundPanelSecond.IsActive) return;

        CloseOtherPanel(backgroundPanelSecond);
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
    public event Action OnClickToShop_Main;
    public event Action OnClickToPlay_Main;
    public event Action OnClickToProfile_Main;

    private void HandleClickToLeaderboard_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToLeaderboard_Main?.Invoke();
    }

    private void HandleClickToShop_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToShop_Main?.Invoke();
    }

    private void HandleClickToPlay_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Main?.Invoke();
    }

    private void HandleClickToProfile_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToProfile_Main?.Invoke();
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

    #region ProfilePanel

    public event Action OnClickToBack_Profile;

    private void HandleClickToBack_Profile()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Profile?.Invoke();
    }

    #endregion

    #region ProfilePanel

    public event Action OnClickToBack_Shop;

    private void HandleClickToBack_Shop()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToBack_Shop?.Invoke();
    }

    #endregion

    #endregion

}
