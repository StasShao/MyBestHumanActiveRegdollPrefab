using UnityEngine;
using ShaoGameMechanicSys;
public class AiPhysicCharacter : AIAnimatronikCharacter
{
    private Inputer _inputer = new Inputer();
    public Transform NavMesher;
    public Transform searcher;
    public Transform defaultWayPoint;
    public CharacterSettings Settings;
    private AIController _iai = new AIController();
    private AIController _aiComtroller;
    private AnimatronicController _animatronikController;
    private BaseController _baseController;
    private Delayer _delayer;
    private bool _tap;
    public override IAI iai()
    {
        return _iai;
    }
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
        return _animatronikController;
    }
    public override AIController aiController()
    {
        return _aiComtroller;
    }
    public override Delayer delayer()
    {
        return _delayer;
    }
    public void Begin()
    {
        _baseController = new BaseController(transform);
        _animatronikController = new AnimatronicController(GetComponent<Animator>());
        _aiComtroller = new AIController(searcher, transform, NavMesher, defaultWayPoint,iai(), Settings.TargetLayer);
        _delayer = new Delayer(_tap);
        
    }
    public virtual void AlienBehavior()
    {
        DirectionSetter();
        MoveBehavior();
        DirectionSetter();
        AnimationSinghronizator();
        ActionAnimationPlayer();
    }
    public virtual void MoveBehavior()
    {
        baseController().RigidbodyMove(Settings.MoveForce, ibaseControllable().forwardDirection, ibaseControllable().sideDirection);
    }
    public virtual void DirectionSetter()
    {
        aiController().AiRigidbodyFollowToNavigation(1,2,5,ibaseControllable());
    }
    public virtual void AnimationSinghronizator()
    {
        animatronicController().MoveSinghronization(ibaseControllable().forwardDirection, ibaseControllable().sideDirection, Settings.ForwardAnimationName, Settings.SideAnimationName);
    }
    public virtual void ActionAnimationPlayer()
    {
        if(iai().randomValue ==0)
        {
            animatronicController().PlayAnimationTrigger("Kick", iai().isCanAttack);
        }
        if (iai().randomValue == 1)
        {
            animatronicController().PlayAnimationTrigger("Punch", iai().isCanAttack);
        }
        if (iai().randomValue == 2)
        {
            animatronicController().PlayAnimationTrigger("LeftPunch", iai().isCanAttack);
        }

    }
    public void SearchingEnemy()
    {
        aiController().OnSearching(Settings.SearchDistance,2);
        aiController().OnAttackReason(delayer(),this);
    }
    public void DelayStart()
    {
        StartCoroutine(delayer().OnDelaye(iai(),3));
    }
    public void DelayStop()
    {
        StopCoroutine(delayer().OnDelaye(iai(),3));
    }
}
