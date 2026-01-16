using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ScoreEarnLeaderboardView : View
{
    [SerializeField] private ScoreEarnLeaderboardPause scoreEarnLeaderboardPause;
    [SerializeField] private ScoreEarnLeaderboardFinish scoreEarnLeaderboardFinish;

    public void Initialize()
    {
        scoreEarnLeaderboardFinish.OnEndSetCoins += EndSetCoins;
    }

    public void Dispose()
    {
        scoreEarnLeaderboardFinish.OnEndSetCoins -= EndSetCoins;
    }

    public void ClearAll()
    {
        scoreEarnLeaderboardPause.ClearAll();
        scoreEarnLeaderboardFinish.ClearAll();
    }

    public void RegisterPlayers(List<IPlayerInfo> players)
    {
        scoreEarnLeaderboardPause.RegisterPlayers(players);
        scoreEarnLeaderboardFinish.RegisterPlayers(players);
    }

    public void UpdateScore(int playerId, int totalScore)
    {
        scoreEarnLeaderboardPause.UpdateScore(playerId, totalScore);
        scoreEarnLeaderboardFinish.UpdateScore(playerId, totalScore);
    }

    public void SetCoins(List<int> coins)
    {
        scoreEarnLeaderboardFinish.SetCoins(coins);
    }

    #region Output

    public event Action OnEndSetCoins;

    private void EndSetCoins()
    {
        OnEndSetCoins?.Invoke();
    }

    #endregion
}

[System.Serializable]
public class ScoreEarnLeaderboardFinish
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private PlayerEarnScoreVisual playerEarnScoreVisualPrefab;
    [SerializeField] private List<RectTransform> transformsEarn;
    [SerializeField] private RectTransform transformScrollView;
    [SerializeField] private Transform addCoindParent;
    [SerializeField] private Vector3 vectorOpen;
    [SerializeField] private AddCoin addCoinPrefab;

    private readonly Dictionary<int, PlayerEarnScoreVisual> _playerEarnScoreVisuals = new();

    public void ClearAll()
    {
        foreach (Transform child in _contentParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        _playerEarnScoreVisuals.Clear();
    }

    public void RegisterPlayers(List<IPlayerInfo> players)
    {
        ClearAll();

        foreach (var info in players)
        {
            var visual = GameObject.Instantiate(playerEarnScoreVisualPrefab, _contentParent);
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

    public void SetCoins(List<int> coins)
    {
        Coroutines.Start(Timer(coins));
    }

    private IEnumerator Timer(List<int> coins)
    {
        transformScrollView.DOLocalMove(vectorOpen, 0.2f);

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < coins.Count; i++)
        {
            // Создаём эффект
            AddCoin effect = GameObject.Instantiate(addCoinPrefab, addCoindParent);
            effect.SetCoins(coins[i]);
            effect.transform.localPosition = transformsEarn[i].localPosition;
            effect.transform.localScale = Vector3.zero;
            effect.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(0.2f);
        }

        OnEndSetCoins?.Invoke();
    }

    #region Output

    public event Action OnEndSetCoins;

    #endregion
}

[System.Serializable]
public class ScoreEarnLeaderboardPause
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private PlayerEarnScoreVisual playerEarnScoreVisualPrefab;

    private readonly Dictionary<int, PlayerEarnScoreVisual> _playerEarnScoreVisuals = new();

    public void ClearAll()
    {
        foreach (Transform child in _contentParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        _playerEarnScoreVisuals.Clear();
    }

    public void RegisterPlayers(List<IPlayerInfo> players)
    {
        ClearAll();

        foreach (var info in players)
        {
            var visual = GameObject.Instantiate(playerEarnScoreVisualPrefab, _contentParent);
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
