using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HighlightSystemView : View
{
    [SerializeField] private List<HighlightPlayer> highlightPlayers = new();
    [SerializeField] private float durationLine;
    [SerializeField] private float durationSmoke;
    [SerializeField] private float durationCheck;

    public void ActivateHighlight(int playerId)
    {
        var highlight = GetHighlightPlayer(playerId);

        if(highlight == null)
        {
            Debug.LogWarning("Not found Highlight with id - " + playerId);
            return;
        }

        highlight.ActivateHighlight(durationLine, durationSmoke, durationCheck);
    }
    public void DeactivateHighlight(int playerId)
    {
        var highlight = GetHighlightPlayer(playerId);

        if (highlight == null)
        {
            Debug.LogWarning("Not found Highlight with id - " + playerId);
            return;
        }

        highlight.DeactivateHighlight(durationLine, durationSmoke, durationCheck);
    }

    private HighlightPlayer GetHighlightPlayer(int playerId)
    {
        return highlightPlayers.Find(data => data.playerId == playerId);
    }
}

[System.Serializable]
public class HighlightPlayer
{
    public int playerId => id;

    [SerializeField] private int id;

    [SerializeField] private Transform transformLine;
    [SerializeField] private Transform transformSmoke;
    [SerializeField] private Transform transformCheck;

    private Tween tweenLine;
    private Tween tweenSmoke;
    private Tween tweenCheck;

    public void ActivateHighlight(float durationLine, float durationSmoke, float durationCheck)
    {
        tweenLine?.Kill();
        tweenSmoke?.Kill();
        tweenCheck?.Kill();

        tweenLine = transformLine.DOScaleX(1, durationLine);
        tweenSmoke = transformSmoke.DOScale(6, durationSmoke);
        tweenCheck = transformCheck.DOScale(0.4f, durationCheck);
    }
    public void DeactivateHighlight(float durationLine, float durationSmoke, float durationCheck)
    {
        tweenLine?.Kill();
        tweenSmoke?.Kill();
        tweenCheck?.Kill();

        tweenLine = transformLine.DOScaleX(0, durationLine);
        tweenSmoke = transformSmoke.DOScale(3, durationSmoke);
        tweenCheck = transformCheck.DOScale(0, durationCheck);
    }
}
