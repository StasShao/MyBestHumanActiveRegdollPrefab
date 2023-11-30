using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShaoGameMechanicSys;
public abstract class ActiveRegdollCharacter : MonoBehaviour
{
    public abstract PhysicAnimatronicController physicAnimatronicController();
    public abstract IDamage iDamage();
    public abstract void FixedTick();
    public abstract void Tick();
    public abstract void Begin();
}
