using ShaoGameMechanicSys;
using UnityEngine;

public abstract class AnimatronicCharacter : MonoBehaviour
{
    public abstract IBaseControllable ibaseControllable();
    public abstract BaseController baseController();
    public abstract AnimatronicController animatronicController();
}
