using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreEarnLeaderboardView : View
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private PlayerEarnScoreVisual playerEarnScoreVisualPrefab;

    private readonly Dictionary<int, PlayerEarnScoreVisual> _playerEarnScoreVisuals = new();

    public void ClearAll()
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
        _playerEarnScoreVisuals.Clear();
    }

    public void RegisterPlayers(List<IPlayerInfo> players)
    {
        ClearAll();

        foreach (var info in players)
        {
            var visual = Instantiate(playerEarnScoreVisualPrefab, _contentParent);
            visual.SetData(info.Id, info.Name);
            visual.SetScore(0);

            _playerEarnScoreVisuals[info.Id] = visual;
        }

        SortByScoreDescending();
    }

    public void UpdateScore(int playerId, int totalScore)
    {
        if (!_playerEarnScoreVisuals.TryGetValue(playerId, out var view))
            return;

        view.SetScore(totalScore);

        SortByScoreDescending();
    }

    private void SortByScoreDescending()
    {
        var sorted = _playerEarnScoreVisuals.Values
            .OrderByDescending(v => v.Score)
            .ToList();

        for (int i = 0; i < sorted.Count; i++)
        {
            sorted[i].transform.SetSiblingIndex(i);
            sorted[i].SetPlace(i + 1); // +1, чтобы позиции были 1, 2, 3...
        }
    }
}
