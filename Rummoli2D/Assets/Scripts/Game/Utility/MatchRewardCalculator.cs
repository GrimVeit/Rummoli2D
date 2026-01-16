using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatchRewardCalculator
{
    private const int MAX_REWARD = 30;

    private static readonly Dictionary<GameDifficulty, Dictionary<int, int[]>> BaseRewards = new()
    {
        {
            GameDifficulty.Easy, new Dictionary<int, int[]>
            {
                { 2, new[] { 8, 3 } },
                { 3, new[] { 10, 6, 2 } },
                { 4, new[] { 12, 7, 3, 1 } },
                { 5, new[] { 15, 9, 4, 2, 1 } },
            }
        },
        {
            GameDifficulty.Medium, new Dictionary<int, int[]>
            {
                { 2, new[] { 12, 5 } },
                { 3, new[] { 15, 8, 3 } },
                { 4, new[] { 18, 10, 4, 2 } },
                { 5, new[] { 20, 12, 6, 3, 1 } },
            }
        },
        {
            GameDifficulty.Hard, new Dictionary<int, int[]>
            {
                { 2, new[] { 17, 7 } },
                { 3, new[] { 20, 11, 5 } },
                { 4, new[] { 24, 14, 6, 3 } },
                { 5, new[] { 30, 18, 9, 4, 2 } },
            }
        }
    };

    private static readonly float[] RoundMultipliers =
    {
        0f,     // dummy index 0
        0.6f,   // 1
        0.7f,   // 2
        0.8f,   // 3
        0.9f,   // 4
        1.0f,   // 5
        1.05f,  // 6
        1.1f,   // 7
        1.15f,  // 8
        1.2f,   // 9
        1.25f   // 10
    };

    /// <summary>
    /// ¬озвращает список наград по местам (index 0 = 1 место)
    /// </summary>
    public static int[] GetRewards(int playersCount, GameDifficulty difficulty, int rounds)
    {
        if (playersCount < 2 || playersCount > 5)
            throw new ArgumentException("Players must be between 2 and 5");

        if (rounds < 1 || rounds > 10)
            throw new ArgumentException("Rounds must be between 1 and 10");

        int[] baseRewards = BaseRewards[difficulty][playersCount];
        float multiplier = RoundMultipliers[rounds];

        int[] result = new int[baseRewards.Length];

        for (int i = 0; i < baseRewards.Length; i++)
        {
            int value = Mathf.RoundToInt(baseRewards[i] * multiplier);
            value = Mathf.Clamp(value, 1, MAX_REWARD);
            result[i] = value;
        }

        return result;
    }
}
