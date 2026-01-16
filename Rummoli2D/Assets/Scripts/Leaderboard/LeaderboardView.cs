using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardView : View
{
    [SerializeField] private Transform transformContent;
    [SerializeField] private UserGrid userGridPrefab;
    [SerializeField] private List<Sprite> spritesAvatar = new();

    public void GetTopPlayers(List<UserData> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            UserGrid grid = Instantiate(userGridPrefab, transformContent);
            grid.SetData(i + 1, spritesAvatar[users[i].Avatar], users[i].Nickname, users[i].Record);
        }
    }
}
