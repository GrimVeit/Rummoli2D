using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chip", menuName = "Game/Chip/New Chip")]
public class Chip : ScriptableObject
{
    [SerializeField] private int nominal;
    [SerializeField] private Sprite spriteChip;

    public int Nominal => nominal;
    public Sprite SpriteChip => spriteChip;
}
