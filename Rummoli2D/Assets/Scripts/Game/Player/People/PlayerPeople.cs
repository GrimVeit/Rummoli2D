using System;
using System.Collections.Generic;

public class PlayerPeople : IPlayer
{
    public int Id => _playerId;
    public string Name => _nicknamePresenter.Nickname;

    private readonly PlayerPeopleStateMachine _playerPeopleStateMachine;
    private readonly IPlayerHighlightSystemProvider _highlightProvider;

    private readonly NicknamePresenter _nicknamePresenter;
    private readonly StoreCardPlayerPresenter _storeCardPlayerPresenter; 
    private readonly PlayerPeopleCardVisualPresenter _playerPeopleCardVisualPresenter;

    private readonly int _playerId;
    private readonly ScorePlayerPresenter _scorePlayerPresenter;

    public PlayerPeople(
        int playerIndex, 
        IPlayerHighlightSystemProvider highlightProvider,
        ISoundProvider soundProvider,
        BetSystemPresenter betSystemPresenter,
        ViewContainer viewContainer)
    {
        _playerId = playerIndex;
        _highlightProvider = highlightProvider;
        _nicknamePresenter = new NicknamePresenter(new NicknameModel(PlayerPrefsKeys.NICKNAME, soundProvider), viewContainer.GetView<NicknameView>());
        _scorePlayerPresenter = new ScorePlayerPresenter(new ScorePlayerModel(), viewContainer.GetView<ScorePlayerView>("Player"));
        _storeCardPlayerPresenter = new StoreCardPlayerPresenter(new StoreCardPlayerModel());
        _playerPeopleCardVisualPresenter = new PlayerPeopleCardVisualPresenter(new PlayerPeopleCardVisualModel(_storeCardPlayerPresenter), viewContainer.GetView<PlayerPeopleCardVisualView>());

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
            _playerPeopleCardVisualPresenter,
            _playerPeopleCardVisualPresenter);
    }

    public void Initialize()
    {
        _playerPeopleStateMachine.OnChoose5Cards += Choose5Cards;

        _nicknamePresenter.Initialize();
        _scorePlayerPresenter.Initialize();
        _playerPeopleCardVisualPresenter.Initialize();
    }

    public void Dispose()
    {
        _playerPeopleStateMachine.OnChoose5Cards -= Choose5Cards;

        _nicknamePresenter.Dispose();
        _scorePlayerPresenter.Dispose();
        _playerPeopleCardVisualPresenter.Dispose();
    }

    #region API

    #region Output

    public event Action OnApplyBet
    {
        add => _playerPeopleStateMachine.OnApplyBet += value;
        remove => _playerPeopleStateMachine.OnApplyBet -= value;
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
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

        _highlightProvider.ActivateHighlight(_playerId);
    }

    public void DeactivateApplyBet()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<PlayerBetState_PlayerPeople>());

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
        _playerPeopleStateMachine.EnterState(_playerPeopleStateMachine.GetState<Choose5CardsState_PlayerPeople>());
    }

    public void DeactivateChoose5Cards()
    {
        _playerPeopleStateMachine.ExitState(_playerPeopleStateMachine.GetState<Choose5CardsState_PlayerPeople>());
    }

    #endregion

    #endregion
}
