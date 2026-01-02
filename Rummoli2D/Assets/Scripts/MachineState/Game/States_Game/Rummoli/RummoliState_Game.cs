using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RummoliState_Game : IState
{
    private readonly IStateMachineProvider _stateProvider;
    private readonly List<IPlayer> _players;
    private readonly IStoreCardRummoliProvider _storeCardRummoliProvider;
    private readonly List<int> _passCycle;

    private int _currentPlayerId = 0;

    public RummoliState_Game(IStateMachineProvider stateProvider, List<IPlayer> players, IStoreCardRummoliProvider storeCardRummoliProvider)
    {
        _stateProvider = stateProvider;
        _players = players;
        _storeCardRummoliProvider = storeCardRummoliProvider;
        _passCycle = new List<int>();
    }

    public void EnterState()
    {
        Debug.Log($"ACTIVATE STATE: <color=red>{this.GetType()}</color>");

        RequestCardToPlayer();
    }

    public void ExitState()
    {

    }

    private void RequestCardToPlayer()
    {
        if (_storeCardRummoliProvider.CurrentCardData != null)
            _players[_currentPlayerId].ActivateRequestCard(_storeCardRummoliProvider.CurrentCardData);
        else
        {
            //Debug.Log("FINISH");
        }
    }

    private void SubscribeEvents(IPlayer player)
    {
        player.OnPass += PassCard;
        player.OnCardLaid += LaidCard;
    }

    private void DescribeEvents(IPlayer player)
    {
        player.OnPass -= PassCard;
        player.OnCardLaid -= LaidCard;
    }

    private void PassCard(int playerIndex)
    {

    }

    private void LaidCard(int playerIndex, ICard card)
    {

    }

    private IPlayer GetPlayer(int index)
    {
        return _players.Find(data => data.Id == index);
    }
}
