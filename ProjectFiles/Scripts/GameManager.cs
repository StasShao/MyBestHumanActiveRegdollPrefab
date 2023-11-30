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
    public Transform CamTarget;
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
        physicAlienCharacter.physicAnimatronicController().OnJointAnimate(physicAlienCharacter,500,6000.0f,200.0f);
        aiChar.SearchingEnemy();
        aiPhysicAlienCharacter.physicAnimatronicController().OnJointAnimate(aiPhysicAlienCharacter, 500, 6000.0f,200.0f);
        if(Input.GetMouseButtonDown(0))
        {
            aiPhysicAlienCharacter.iDamage().SetDamage(200);
            Debug.Log(aiPhysicAlienCharacter.iDamage().damageStateValue);
        }
        if (Input.GetMouseButtonDown(1))
        {
            aiPhysicAlienCharacter.iDamage().SetDamage(-500);
            Debug.Log(aiPhysicAlienCharacter.iDamage().damageStateValue);
        }
        if (Input.GetKeyDown(KeyCode.L))
        { 
            aiPhysicAlienCharacter.iDamage().SetControllState(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            aiPhysicAlienCharacter.iDamage().SetControllState(true);
        }

    }
    private void LateUpdate()
    {
        smoothCamera.cameraFollower().FreeCameraSlerpFollow(CamTarget);
    }
}
