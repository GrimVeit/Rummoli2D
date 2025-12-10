using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private RulesPanel_Menu rulesPanel;
    [SerializeField] private ProfilePanel_Menu profilePanel;
    [SerializeField] private ShopPanel_Game shopPanel;

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
        shopPanel.Initialize();

        registrationPanel.Initialize();
        loadRegistrationPanel.Initialize();
    }

    public void Activate()
    {
        registrationPanel.OnClickToRegistrate += HandleClickToRegistrate_Registration;

        mainPanel.OnClickToRules += HandleClickToRules_Main;
        mainPanel.OnClickToProfile += HandleClickToProfile_Main;
        mainPanel.OnClickToBalance += HandleClickToBalance_Main;
        mainPanel.OnClickToSettings += HandleClickToSettings_Main;
        mainPanel.OnClickToShop += HandleClickToShop_Main;
        mainPanel.OnClickToPlay += HandleClickToPlay_Main;

        rulesPanel.OnClickToBack += HandleClickToBack_Rules;
        profilePanel.OnClickToBack += HandleClickToBack_Profile;
        shopPanel.OnClickToBack += HandleClickToBack_Shop;
    }


    public void Deactivate()
    {
        registrationPanel.OnClickToRegistrate -= HandleClickToRegistrate_Registration;


        mainPanel.OnClickToRules -= HandleClickToRules_Main;
        mainPanel.OnClickToProfile -= HandleClickToProfile_Main;
        mainPanel.OnClickToBalance -= HandleClickToBalance_Main;
        mainPanel.OnClickToSettings -= HandleClickToSettings_Main;
        mainPanel.OnClickToShop -= HandleClickToShop_Main;
        mainPanel.OnClickToPlay -= HandleClickToPlay_Main;

        rulesPanel.OnClickToBack -= HandleClickToBack_Rules;
        profilePanel.OnClickToBack -= HandleClickToBack_Profile;
        shopPanel.OnClickToBack -= HandleClickToBack_Shop;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        rulesPanel.Dispose();
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

    public event Action OnClickToRules_Main;
    public event Action OnClickToProfile_Main;
    public event Action OnClickToBalance_Main;
    public event Action OnClickToSettings_Main;
    public event Action OnClickToShop_Main;
    public event Action OnClickToPlay_Main;

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

    private void HandleClickToPlay_Main()
    {
        _soundProvider.PlayOneShot("Click");

        OnClickToPlay_Main?.Invoke();
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
