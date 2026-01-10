using System;
using System.Collections.Generic;

public class PlayerBot : IPlayer
{
    public int Id => _playerId;
    public string Name => _name;
    public int CardCount => _storeCardPlayerPresenter.Cards.Count;
    public int TotalScore => _scorePlayerPresenter.TotalScore;
    public int TotalEarnedScore => _scorePlayerPresenter.TotalEarnedScore;

    private readonly PlayerBotStateMachine _playerBotStateMachine;
    private readonly IPlayerHighlightSystemProvider _highlightProvider;

    private readonly StoreCardPlayerPresenter _storeCardPlayerPresenter;
    private readonly PlayerBotCardVisualPresenter _playerBotCardVisualPresenter;

    private readonly int _playerId;
    private readonly string _name;
    private readonly ScorePlayerPresenter _scorePlayerPresenter;

    public PlayerBot(
        int playerIndex,
        string name,
        IPlayerHighlightSystemProvider highlightProvider,
        ICardPokerSelectorBotProvider cardPlayerPresenter,
        BetSystemPresenter betSystemPresenter,
        IStoreGameDifficultyInfoProvider storeGameDifficultyInfoProvider,
        ViewContainer viewContainer)
    {
        _playerId = playerIndex;
        _name = name;
        _highlightProvider = highlightProvider;
        _scorePlayerPresenter = new ScorePlayerPresenter(new ScorePlayerModel(), viewContainer.GetView<ScorePlayerView>($"Bot_{_playerId}"));
        _storeCardPlayerPresenter = new StoreCardPlayerPresenter(new StoreCardPlayerModel());
        _playerBotCardVisualPresenter = new PlayerBotCardVisualPresenter(new PlayerBotCardVisualModel(_storeCardPlayerPresenter), viewContainer.GetView<PlayerBotCardVisualView>($"Bot_{_playerId}"));

        _playerBotStateMachine = new PlayerBotStateMachine
            (playerIndex,
            betSystemPresenter,
            betSystemPresenter,
            betSystemPresenter,
            cardPlayerPresenter,
            _scorePlayerPresenter,
            _storeCardPlayerPresenter,
            storeGameDifficultyInfoProvider);
    }

    public void Initialize()
    {
        _playerBotStateMachine.OnChoose5Cards += Choose5Cards;
        _playerBotStateMachine.OnCardLaid_Next += CardLiad_Next;
        _playerBotStateMachine.OnPass_Next += Pass_Next;
        _playerBotStateMachine.OnCardLaid_RandomTwo += CardLiad_RandomTwo;
        _playerBotStateMachine.OnPass_RandomTwo += Pass_RandomTwo;

        _scorePlayerPresenter.Initialize();
        _playerBotCardVisualPresenter.Initialize();
    }

    public void Dispose()
    {
        _playerBotStateMachine.OnChoose5Cards -= Choose5Cards;
        _playerBotStateMachine.OnCardLaid_Next -= CardLiad_Next;
        _playerBotStateMachine.OnPass_Next -= Pass_Next;
        _playerBotStateMachine.OnCardLaid_RandomTwo -= CardLiad_RandomTwo;
        _playerBotStateMachine.OnPass_RandomTwo -= Pass_RandomTwo;

        _scorePlayerPresenter.Dispose();
        _playerBotCardVisualPresenter.Dispose();
    }

    #region API

    #region Output

    //BET-----------------------------------------------------
    public event Action OnApplyBet
    {
        add => _playerBotStateMachine.OnApplyBet += value;
        remove => _playerBotStateMachine.OnApplyBet -= value;
    }

    //SCORE---------------------------------------------------

    public event Action<int> OnAddScore
    {
        add => _scorePlayerPresenter.OnAddScore += value;
        remove => _scorePlayerPresenter.OnAddScore -= value;
    }

    public event Action<int> OnRemoveScore
    {
        add => _scorePlayerPresenter.OnRemoveScore += value;
        remove => _scorePlayerPresenter.OnRemoveScore -= value;
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

    //APPLY START SCORE----------------------------------------------------------------------------------
    public void SetScore(int score)
    {
        _scorePlayerPresenter.SetScore(score);
    }

    public void AddScore(int score)
    {
        _scorePlayerPresenter.AddScore(score);
    }


    //APPLY BET------------------------------------------------------------------------------------------
    public void ActivateApplyBet()
    {
        _playerBotStateMachine.EnterState(_playerBotStateMachine.GetState<PlayerBetState_PlayerBot>());

        _highlightProvider.ActivateHighlight(_playerId);
    }

    public void DeactivateApplyBet()
    {
        _playerBotStateMachine.ExitState(_playerBotStateMachine.GetState<PlayerBetState_PlayerBot>());

        _highlightProvider.DeactivateHighlight(_playerId);
    }

    //CARD-----------------------------------------------------------------------------------------------
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

    //POKER----------------------------------------------------------------------------------------------
    public void ActiveChoose5Cards()
    {
        _playerBotStateMachine.EnterState(_playerBotStateMachine.GetState<Choose5CardsState_PlayerBot>());
    }

    public void DeactivateChoose5Cards()
    {
        _playerBotStateMachine.ExitState(_playerBotStateMachine.GetState<Choose5CardsState_PlayerBot>());
    }

    //RUMMOLI-----------------------------------------------------------------------------------------------------
    public void ActivateRequestCard(CardData card)
    {
        var istate = _playerBotStateMachine.GetState<ChooseRequestCard_PlayerBot>();
        ChooseRequestCard_PlayerBot state = (ChooseRequestCard_PlayerBot)istate;
        state.SetCardData(card);
        _playerBotStateMachine.EnterState(state);
    }

    public void DeactivateRequestCard()
    {
        _playerBotStateMachine.ExitState(_playerBotStateMachine.GetState<ChooseRequestCard_PlayerBot>());
    }

    public void ActivateRequestRandomTwo()
    {
        _playerBotStateMachine.EnterState(_playerBotStateMachine.GetState<ChooseRequestRandomTwo_PlayerBot>());
    }

    public void DeactivateRequestRandomTwo()
    {
        _playerBotStateMachine.ExitState(_playerBotStateMachine.GetState<ChooseRequestRandomTwo_PlayerBot>());
    }

    #endregion

    #endregion
}
