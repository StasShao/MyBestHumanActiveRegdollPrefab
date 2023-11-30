using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    int randomValue { get; }
    Transform detectedTarget { get; }
     bool isDetected { get; }
    bool isAttackable { get; }
    bool isDelay{ get; }
    bool isDelayStop { get; }
    bool isCanAttack { get; }
    void SetDetectedTarget(Transform detectedTarg,bool isDetect = false);
    void SetAttackable(bool isAtttacked = false);
    void SetDelay(bool isdelay = true);
    void SetStopDelay(bool delayStop = true);
    void SetCanAttack(bool canAttack = true);
    void SetRandomValue(int value);
}
public interface IDamage
{
    float damageStateValue { get; }
    bool isLostControll { get; }
    void SetControllState(bool isControll = false);
    void SetDamage(float damage = 100.0f);
}