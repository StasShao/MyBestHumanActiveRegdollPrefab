using ShaoGameMechanicSys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicAlienCharacter : ActiveRegdollCharacter
{
    public List<Transform> AnimationTargetPartList;
    public List<ConfigurableJoint> configurableJointList;
    public List<Collider> ignoreColliderList;
    private PhysicAnimatronicController _physicAnimatorController;
    public void Begin()
    {
        _physicAnimatorController = new PhysicAnimatronicController(AnimationTargetPartList, configurableJointList, ignoreColliderList);
    }
    public override PhysicAnimatronicController physicAnimatronicController()
    {
        return _physicAnimatorController;
    }
}
