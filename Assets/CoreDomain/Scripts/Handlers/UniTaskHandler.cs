using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public static class UniTaskHandler
{
     public static async UniTask WaitForAnyKeyPressed()
     {
#if UNITY_EDITOR
         await Observable.EveryUpdate().Where(_ => Input.anyKeyDown).First().ToTask();
#else
         await Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).First().ToTask();
#endif
     }
}
