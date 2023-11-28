using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AnimatronikAlienIK animatroniAlienIK;
    public PhysicAlienCharacter physicAlienCharacter;
    private void Start()
    {
        physicAlienCharacter.physicAnimatronicController().IgnoreMultipleCollisions();
    }
    void Update()
    {
        animatroniAlienIK.AlienBehavior();
        physicAlienCharacter.physicAnimatronicController().OnJointAnimate();
    }
}
