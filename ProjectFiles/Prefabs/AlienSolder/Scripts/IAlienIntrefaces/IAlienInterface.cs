using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAI
{
    Transform detectedTarget { get; }
     bool isDetected { get; }
    bool isAttackable { get; }
    bool isDelay{ get; }
    void SetDetectedTarget(Transform detectedTarg,bool isDetect = false);
    void SetAttackable(bool isAtttacked = false);
    void SetDelay(bool isdelay = true);
}