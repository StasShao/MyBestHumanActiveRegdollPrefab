using ShaoGameMechanicSys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicAlienCharacter : ActiveRegdollCharacter
{
    public List<Transform> AnimationTargetPartList;
    public List<ConfigurableJoint> configurableJointList;
    public List<Collider> ignoreColliderList;
    public override PhysicAnimatronicController physicAnimatronicController()
    {
        return new PhysicAnimatronicController(AnimationTargetPartList,configurableJointList, ignoreColliderList);
    }
}
