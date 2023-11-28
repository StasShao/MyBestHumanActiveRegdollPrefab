using ShaoGameMechanicSys;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AnimatronikAlienIK : AnimatronicCharacter
{
    private Inputer _inputer = new Inputer();
    public CharacterSettings Settings;
    public override BaseController baseController()
    {
        return new BaseController(transform);
    }
    public override IBaseControllable ibaseControllable()
    {
        return _inputer;
    }
    public override AnimatronicController animatronicController()
    {
        return new AnimatronicController(GetComponent<Animator>());
    }
    public virtual void AlienBehavior()
    {
        ibaseControllable().SetDirection(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        baseController().RigidbodyMove(Settings.MoveForce, ibaseControllable().forwardDirection, ibaseControllable().sideDirection);
        animatronicController().MoveSinghronization(ibaseControllable().forwardDirection, ibaseControllable().sideDirection, Settings.ForwardAnimationName, Settings.SideAnimationName);
        animatronicController().PlayAnimationTrigger(PlayKick(),Input.GetKeyDown(KeyCode.E));
    }
    public string PlayKick()
    {
        ibaseControllable().SetName("Kick");
        return ibaseControllable().animationName;
    }
}
