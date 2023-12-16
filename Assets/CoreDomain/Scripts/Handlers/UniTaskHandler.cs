using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public static class UniTaskHandler
{
     public static async UniTask WaitForAnyKeyPressed()
     {
         await Observable.EveryUpdate().Where(_ => Input.anyKeyDown).First().ToTask();
     }
}
