using UnityEngine;

namespace CoreDomain.Services
{
    public interface ICameraService
    {
        Vector3 ScreenToWorldPoint(GameCameraType type, Vector3 screenPoint);
        Vector3 GetCameraPosition(GameCameraType type);
        void SetCameraFollowTarget(GameCameraType type, Transform target);
        void SetCameraZoom(GameCameraType type, bool isZoomIn);
    }
}