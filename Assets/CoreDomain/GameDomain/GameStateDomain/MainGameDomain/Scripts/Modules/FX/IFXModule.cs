using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IFXModule
{
    UniTask ShowScoreGainedFx(Vector3 positon, int scoreGained);
}