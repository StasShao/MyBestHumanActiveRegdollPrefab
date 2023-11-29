using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AnimatronikAlienIK animatroniAlienIK;
    public PhysicAlienCharacter physicAlienCharacter;
    public PhysicAlienCharacter aiPhysicAlienCharacter;
    public SmoothCameraFollow smoothCamera;
    public AiPhysicCharacter aiChar;
    private void Start()
    {
        aiPhysicAlienCharacter.Begin();
        aiPhysicAlienCharacter.physicAnimatronicController().IgnoreMultipleCollisions();
        physicAlienCharacter.Begin();
        physicAlienCharacter.physicAnimatronicController().IgnoreMultipleCollisions();
        animatroniAlienIK.Begin();
        aiChar.Begin();
        smoothCamera.Begin();
    }
    void Update()
    {
        animatroniAlienIK.AlienBehavior();
        aiChar.AlienBehavior();
        physicAlienCharacter.physicAnimatronicController().OnJointAnimate();
        aiChar.SearchingEnemy();
        aiPhysicAlienCharacter.physicAnimatronicController().OnJointAnimate();
    }
    private void LateUpdate()
    {
        smoothCamera.cameraFollower().FreeCameraSlerpFollow(animatroniAlienIK.transform);
    }
}
