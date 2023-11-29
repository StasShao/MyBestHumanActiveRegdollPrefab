using UnityEngine;
using ShaoGameMechanicSys;
public abstract class AIAnimatronikCharacter : MonoBehaviour
{
    public abstract IBaseControllable ibaseControllable();
    public abstract BaseController baseController();
    public abstract AnimatronicController animatronicController();
    public abstract AIController aiController();
    public abstract Delayer delayer();
    public abstract IAI iai();
    
}
