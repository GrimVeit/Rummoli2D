using UnityEngine;

[CreateAssetMenu(fileName = "ChipCount", menuName = "Game/Chip/ChipCount")]
public class Chips : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Chip chip;
    private ChipData _chipData;

    public int ID => id;
    public Chip Chip => chip;
    public ChipData ChipData => _chipData;

    public void SetData(ChipData chipData)
    {
        _chipData = chipData;
    }
}
