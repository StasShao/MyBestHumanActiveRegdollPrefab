using ShaoGameMechanicSys;
using UnityEngine;

public class SmoothCameraFollow : FreeCamera
{
    public Vector3 Offset;
    public float SlerpTimeSpeed;
    private CameraFollower _cameraFollower;
    public void Begin()
    {
        _cameraFollower = new CameraFollower(GetComponent<Camera>(), Offset, SlerpTimeSpeed);
    }
    public override CameraFollower cameraFollower()
    {
        return _cameraFollower;
    }
}
