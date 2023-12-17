using UnityEngine;

[CreateAssetMenu(fileName = "BalloonsData", menuName = "Game/BalloonsData")]
public class BalloonsData : ScriptableObject
{
    public int PopScore = 5;
    public Color[] BalloonsColorPalette;
}
