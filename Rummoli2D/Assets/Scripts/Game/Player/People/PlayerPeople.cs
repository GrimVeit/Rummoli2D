using System;
using System.Collections.Generic;

public class PlayerPeople : IPlayer
{
    public int Id => _playerId;
    public string Name => _nicknamePresenter.Nickname;
    public int CardCount => _storeCardPlayerPresenter.Cards.Count;

    private readonly PlayerPeopleStateMachine _playerPeopleStateMachine;
    private readonly IPlayerHighlightSystemProvider _highlightProvider;

    private readonly NicknamePresenter _nicknamePresenter;
    private readonly StoreCardPlayerPresenter _storeCardPlayerPresenter; 
    private readonly PlayerPeopleCardVisualPresenter _playerPeopleCardVisualPresenter;
    private readonly PlayerPeopleInputPresenter _playerPeopleInputPresenter;

    private readonly int _playerId;
    private readonly ScorePlayerPresenter _scorePlayerPresenter;

    public PlayerPeople(
        int playerIndex, 
        IPlayerHighlightSystemProvider highlightProvider,
        ISoundProvider soundProvider,
        ICardPokerSelectorPlayerProvider cardPokerSelectorPlayerProvider,
        BetSystemPresenter betSystemPresenter,
        ViewContainer viewContainer)
    {
        _playerId = playerIndex;
        _highlightProvider = highlightProvider;
        _nicknamePresenter = new NicknamePresenter(new NicknameModel(PlayerPrefsKeys.NICKNAME, soundProvider), viewContainer.GetView<NicknameView>());
        _scorePlayerPresenter = new ScorePlayerPresenter(new ScorePlayerModel(), viewContainer.GetView<ScorePlayerView>("Player"));
        _storeCardPlayerPresenter = new StoreCardPlayerPresenter(new StoreCardPlayerModel());
        _playerPeopleCardVisualPresenter = new PlayerPeopleCardVisualPresenter(new PlayerPeopleCardVisualModel(_storeCardPlayerPresenter), viewContainer.GetView<PlayerPeopleCardVisualView>());
        _playerPeopleInputPresenter = new PlayerPeopleInputPresenter(viewContainer.GetView<PlayerPeopleInputView>());

        _playerPeopleStateMachine = new PlayerPeopleStateMachine
            (playerIndex, 
            betSystemPresenter, 
            betSystemPresenter,
            betSystemPresenter,
            betSystemPresenter,
            _scorePlayerPresenter,
            _playerPeopleCardVisualPresenter,
            _playerPeopleCardVisualPresenter,
            _playerPeopleCardVisualPresenter,
            _playerPeopleInputPresenter,
            _playerPeopleInputPresenter,
            cardPokerSelectorPlayerProvider);
    }

    public void Initialize()
    {
        _playerPeopleStateMachine.OnChoose5Cards += Choose5Cards;
        _playerPeopleStateMachine.OnCardLaid_Next += CardLiad_Next;
        _playerPeopleStateMachine.OnPass_Next += Pass_Next;
        _playerPeopleStateMachine.OnCardLaid_RandomTwo += CardLiad_RandomTwo;
        _playerPeopleStateMachine.OnPass_RandomTwo += Pass_RandomTwo;

        _nicknamePresenter.Initialize();
        _scorePlayerPresenter.Initialize();
        _playerPeopleCardVisualPresenter.Initialize();
        _playerPeopleInputPresenter.Initialize();
    }

    public void Dispose()
    {
        _playerPeopleStateMachine.OnChoose5Cards -= Choose5Cards;
        _playerPeopleStateMachine.OnCardLaid_Next -= CardLiad_Next;
        _playerPeopleStateMachine.OnPass_Next -= Pass_Next;
        _playerPeopleStateMachine.OnCardLaid_RandomTwo -= CardLiad_RandomTwo;
        _playerPeopleStateMachine.OnPass_RandomTwo -= Pass_RandomTwo;

        _nicknamePresenter.Dispose();
        _scorePlayerPresenter.Dispose();
        _playerPeopleCardVisualPresenter.Dispose();
        _playerPeopleInputPresenter.Dispose();
    }

    #region API

    #region Output

    //BET-----------------------------------------------------
    public event Action OnApplyBet
    {
        add => _playerPeopleStateMachine.OnApplyBet += value;
        remove => _playerPeopleStateMachine.OnApplyBet -= value;
    }

    //POKER---------------------------------------------------
    public event Action<IPlayer, List<ICard>> OnChoose5Cards;

    private void Choose5Cards(List<ICard> cards)
    {
        OnChoose5Cards?.Invoke(this, cards);
    }

    //RUMMOLI-------------------------------------------------
    public event Action<int, ICard> OnCardLaid_Next;
    public event Action<int> OnPass_Next;

    private void CardLiad_Next(ICard card)
    {
        OnCardLaid_Next?.Invoke(_playerId, card);
    }

    private void Pass_Next()
    {
        OnPass_Next?.Invoke(_playerId);
    }


    public event Action<int, ICard> OnCardLaid_RandomTwo;
    public event Action<int> OnPass_RandomTwo;

    private void CardLiad_RandomTwo(ICard card)
    {
        OnCardLaid_RandomTwo?.Invoke(_playerId, card);
    }

    private void Pass_RandomTwo()
    {
        OnPass_RandomTwo?.Invoke(_playerId);
    }

    #endregion

    #region Input

    //APPLY START SCORE
    public void SetScore(int score)
    {
        _scorePlayerPresenter.SetScore(score);
    }

    public void AddScore(int score)
    {
        _scorePlayerPresenter.AddScore(score);
    }


    //APPLY BET-------------------------------------------------------------------------------------------------
    public void ActivateApplyBet()
    {
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

        _highlightProvider.ActivateHighlight(_playerId);
    }

    public void DeactivateApplyBet()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

        _highlightProvider.DeactivateHighlight(_playerId);
    }

    //CARD-------------------------------------------------------------------------------------------------------
    public void AddCard(ICard card)
    {
        _storeCardPlayerPresenter.AddCard(card);
    }

    public void RemoveCard(ICard card)
    {
        _storeCardPlayerPresenter.RemoveCard(card);
    }

    public void DeleteCards()
    {
        _storeCardPlayerPresenter.DeleteCards();
    }

    //POKER-------------------------------------------------------------------------------------------------------
    public void ActiveChoose5Cards()
    {
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<Choose5CardsState_PlayerPeople>());
    }

    public void DeactivateChoose5Cards()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<Choose5CardsState_PlayerPeople>());
    }

    //RUMMOLI-----------------------------------------------------------------------------------------------------
    public void ActivateRequestCard(CardData card)
    {
        var istate = _playerPeopleStateMachine.GetState<ChooseRequestCard_PlayerPeople>();
        ChooseRequestCard_PlayerPeople state = (ChooseRequestCard_PlayerPeople)istate;
        state.SetCardData(card);
        _playerPeopleStateMachine.EnterState(state);
    }

    public void DeactivateRequestCard()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<ChooseRequestCard_PlayerPeople>());
    }

    public void ActivateRequestRandomTwo()
    {
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<ChooseRequestRandomTwo_PlayerPeople>());
    }

    public void DeactivateRequestRandomTwo()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<ChooseRequestRandomTwo_PlayerPeople>());
    }

    #endregion

    #endregion
}
