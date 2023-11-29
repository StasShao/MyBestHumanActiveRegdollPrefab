using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseControllable
{
    float forwardDirection { get; }
    float sideDirection { get; }
    string animationName { get; }
    bool isActive { get; }
    void SetDirection(float forwardDir,float sideDir);
    void SetName(string animName);
    void SetActive(bool isActive = true);

}