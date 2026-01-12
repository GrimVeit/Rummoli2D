using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreEarnLeaderboardModel
{
    private readonly Dictionary<int, IPlayer> _players = new();

    public void Initialize()
    {
    }

    public void Dispose()
    {
        ClearPlayers();
    }

    private void ClearPlayers()
    {
        foreach (var player in _players.Values)
        {
            player.OnAddScore -= AddScore;
        }

        _players.Clear();

        OnClearPlayers?.Invoke();
    }

    public void RegisterPlayers(List<IPlayer> players)
    {
        ClearPlayers();

        foreach (var player in players)
        {
            _players[player.Id] = player;
            player.OnAddScore += AddScore;
        }

        OnRegisterPlayers?.Invoke(_players.Values.Cast<IPlayerInfo>().ToList());
    }

    private void AddScore(int playerId, int score)
    {
        if (!_players.TryGetValue(playerId, out var player))
            return;

        OnAddScore?.Invoke(player.Id, player.TotalEarnedScore);
    }

    public event Action OnClearPlayers;
    public event Action<List<IPlayerInfo>> OnRegisterPlayers;
    public event Action<int, int> OnAddScore;
}
