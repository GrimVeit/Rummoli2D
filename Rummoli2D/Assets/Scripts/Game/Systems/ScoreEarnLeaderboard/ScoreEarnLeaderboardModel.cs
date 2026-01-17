using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreEarnLeaderboardModel
{
    private readonly Dictionary<int, IPlayer> _players = new();
    private readonly IStorePlayersCountInfoProvider _playersCountInfoProvider;
    private readonly IStoreRoundCountInfoProvider _roundsInfoProvider;
    private readonly IStoreGameDifficultyInfoProvider _gameDifficultyInfoProvider;

    public ScoreEarnLeaderboardModel(IStorePlayersCountInfoProvider playersCountInfoProvider, IStoreRoundCountInfoProvider roundsInfoProvider, IStoreGameDifficultyInfoProvider gameDifficultyInfoProvider)
    {
        _playersCountInfoProvider = playersCountInfoProvider;
        _roundsInfoProvider = roundsInfoProvider;
        _gameDifficultyInfoProvider = gameDifficultyInfoProvider;
    }

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

    public void SearchWinners()
    {
        List<IPlayer> sortedPlayers = _players.Values
            .OrderByDescending(p => p.TotalEarnedScore)
            .ToList();

        int playersCount = _playersCountInfoProvider.PlayersCount;
        int rounds = _roundsInfoProvider.RoundsCount;
        var difficulty = _gameDifficultyInfoProvider.GameDifficulty;

        int[] baseRewards = MatchRewardCalculator.GetRewards(playersCount, difficulty, rounds);

        List<int> finalRewards = AdjustRewardsForTies(sortedPlayers, baseRewards);

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            sortedPlayers[i].SendMoney(finalRewards[i]);
            sortedPlayers[i].SendProgressScore(sortedPlayers[i].TotalEarnedScore);
        }

        OnSetCoins?.Invoke(finalRewards);
    }

    private void AddScore(int playerId, int score)
    {
        if (!_players.TryGetValue(playerId, out var player))
            return;

        OnAddScore?.Invoke(player.Id, player.TotalEarnedScore);
    }

    public List<int> AdjustRewardsForTies(List<IPlayer> sortedPlayers, int[] baseRewards)
    {
        List<int> adjustedRewards = new List<int>();

        if (sortedPlayers.Count == 0)
            return adjustedRewards;

        int previousScore = sortedPlayers[0].TotalEarnedScore;
        int previousReward = baseRewards[0];
        adjustedRewards.Add(previousReward);

        int rewardIndex = 1; // индекс в baseRewards

        for (int i = 1; i < sortedPlayers.Count; i++)
        {
            int score = sortedPlayers[i].TotalEarnedScore;

            if (score == previousScore)
            {
                // Ничья → даем ту же награду
                adjustedRewards.Add(previousReward);
            }
            else
            {
                // Берем следующую награду из baseRewards, если она есть
                previousReward = rewardIndex < baseRewards.Length ? baseRewards[rewardIndex] : baseRewards[^1];
                adjustedRewards.Add(previousReward);
                rewardIndex++;
            }

            previousScore = score;
        }

        return adjustedRewards;
    }

    public event Action OnClearPlayers;
    public event Action<List<IPlayerInfo>> OnRegisterPlayers;
    public event Action<int, int> OnAddScore;
    public event Action<List<int>> OnSetCoins;
}
