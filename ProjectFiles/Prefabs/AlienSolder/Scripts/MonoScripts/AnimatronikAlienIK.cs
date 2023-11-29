using ShaoGameMechanicSys;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AnimatronikAlienIK : AnimatronicCharacter
{
    private Inputer _inputer = new Inputer();
    private BaseController _baseController;
    private AnimatronicController _animatronicController;
    public CharacterSettings Settings;
    public override BaseController baseController()
    {
        return _baseController;
    }
    public override IBaseControllable ibaseControllable()
    {
        return _inputer;
    }
    public override AnimatronicController animatronicController()
    {
        return _animatronicController;
    }
    public void Begin()
    {
        _baseController = new BaseController(transform);
        _animatronicController = new AnimatronicController(GetComponent<Animator>());
    }
    public virtual void AlienBehavior()
    {
        ibaseControllable().SetDirection(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        baseController().RigidbodyMove(Settings.MoveForce, ibaseControllable().forwardDirection, ibaseControllable().sideDirection);
        animatronicController().MoveSinghronization(ibaseControllable().forwardDirection, ibaseControllable().sideDirection, Settings.ForwardAnimationName, Settings.SideAnimationName);
        animatronicController().PlayAnimationTrigger("Kick",Input.GetKeyDown(KeyCode.F));
        animatronicController().PlayAnimationTrigger("Punch",Input.GetKeyDown(KeyCode.E));
        animatronicController().PlayAnimationTrigger("LeftPunch", Input.GetKeyDown(KeyCode.Q));
    }
   

}
