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
        physicAlienCharacter.Begin();
        animatroniAlienIK.Begin();
        aiChar.Begin(aiPhysicAlienCharacter);
        smoothCamera.Begin();
    }
    void Update()
    {
        animatroniAlienIK.AlienBehavior();
        aiChar.AlienBehavior();
        aiChar.SearchingEnemy();
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
    private void FixedUpdate()
    {
        physicAlienCharacter.FixedTick();
        aiPhysicAlienCharacter.FixedTick();
    }
}
