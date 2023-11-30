using ShaoGameMechanicSys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicAlienCharacter : ActiveRegdollCharacter,IDamage
{
    public List<Transform> AnimationTargetPartList;
    public List<ConfigurableJoint> configurableJointList;
    public List<Collider> ignoreColliderList;
    private PhysicAnimatronicController _physicAnimatorController;
    private IDamage _idamage;
    [SerializeField] private List<string> LegsJointNameGroupList;
    [SerializeField] private List<string> ArmsJointNameGroupList;
    [SerializeField] private string MainPivotJointHipsName;
    [SerializeField] private string SpineJointName;

    public float damageStateValue { get; private set; }

    public bool isLostControll { get; private set; }
    public void SetControllState(bool isControll = false)
    {
        isLostControll = isControll;
    }
    public void SetDamage(float damage = 100)
    {
        damageStateValue += damage;
    }

    public void Begin()
    {
        _idamage = GetComponent<IDamage>();
        _physicAnimatorController = new PhysicAnimatronicController(AnimationTargetPartList, configurableJointList, ignoreColliderList, LegsJointNameGroupList,ArmsJointNameGroupList,MainPivotJointHipsName,SpineJointName);
    }
    public override PhysicAnimatronicController physicAnimatronicController()
    {
        return _physicAnimatorController;
    }

    public override IDamage iDamage()
    {
        return _idamage;
    }
}
