using System;
using System.Collections.Generic;

public class PlayerBot : IPlayer
{
    public int Id => _playerId;
    public string Name => _name;

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
        BetSystemPresenter betSystemPresenter,
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
            _scorePlayerPresenter,
            _storeCardPlayerPresenter);
    }

    public void Initialize()
    {
        _playerBotStateMachine.OnChoose5Cards += Choose5Cards;

        _scorePlayerPresenter.Initialize();
        _playerBotCardVisualPresenter.Initialize();
    }

    public void Dispose()
    {
        _playerBotStateMachine.OnChoose5Cards -= Choose5Cards;

        _scorePlayerPresenter.Dispose();
        _playerBotCardVisualPresenter.Dispose();
    }

    #region API

    #region Output

    public event Action OnApplyBet
    {
        add => _playerBotStateMachine.OnApplyBet += value;
        remove => _playerBotStateMachine.OnApplyBet -= value;
    }

    public event Action<IPlayer, List<ICard>> OnChoose5Cards;

    private void Choose5Cards(List<ICard> cards)
    {
        OnChoose5Cards?.Invoke(this, cards);
    }

    #endregion

    #region Input

    //APPLY START SCORE
    public void SetScore(int score)
    {
        _scorePlayerPresenter.SetScore(score);
    }


    //APPLY BET
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

    //CARD
    public void AddCard(ICard card)
    {
        _storeCardPlayerPresenter.AddCard(card);
    }

    public void RemoveCard(ICard card)
    {
        _storeCardPlayerPresenter.RemoveCard(card);
    }

    //POKER
    public void ActiveChoose5Cards()
    {
        _playerBotStateMachine.EnterState(_playerBotStateMachine.GetState<Choose5CardsState_PlayerBot>());
    }
    public void DeactivateChoose5Cards()
    {

    }

    #endregion

    #endregion
}
