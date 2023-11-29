using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaoGameMechanicSys
{
    using UnityEngine.AI;
    public class BaseController
    {
        private Rigidbody _rb;
        private Transform _transform;
        public BaseController(Transform transform)
        {
            _transform = transform;
            if(_transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                _rb = rb;
            }
        }
        public void RigidbodyMove(float force,float forwardDirection,float sideDirection)
        {
            _rb.AddForce(_rb.transform.forward * forwardDirection * force,ForceMode.Force);
            _rb.AddForce(_rb.transform.right * sideDirection * force, ForceMode.Force);
        }
    }
    public class AnimatronicController
    {
        private Animator _animator;
        private Rigidbody _rb;
        private KeyCode _keyCode;
        public enum KeyCode
        {
            firstAttack = 1,
            secondAttack = 2,
            thirdAttack = 3
        }
        public AnimatronicController(Animator animator)
        {
            _animator = animator;
            if (_animator.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb)) { _rb = rb;}
        }
        public void MoveSinghronization(float forwardDirection, float sideDirection, string forwardDirectionName,string sideDirectionName)
        {
            if (_rb == null) return;
            if (forwardDirection > 0) { _animator.SetFloat(forwardDirectionName, Mathf.Clamp(_rb.velocity.magnitude , -1, 1)); }
            if (forwardDirection < 0) { _animator.SetFloat(forwardDirectionName, Mathf.Clamp(_rb.velocity.magnitude * -1, -1, 1)); }
            if (sideDirection > 0) { _animator.SetFloat(sideDirectionName, Mathf.Clamp(_rb.velocity.magnitude, -1, 1)); }
            if (sideDirection < 0) { _animator.SetFloat(sideDirectionName, Mathf.Clamp(_rb.velocity.magnitude * -1, -1, 1)); }
            if (sideDirection == 0)
            {
                if (_animator.GetFloat(sideDirectionName) > 0) { _animator.SetFloat(sideDirectionName, Mathf.Clamp(_animator.GetFloat(sideDirectionName) - 1f * Time.deltaTime, 0, 1)); }
                if (_animator.GetFloat(sideDirectionName) < 0) { _animator.SetFloat(sideDirectionName, Mathf.Clamp(_animator.GetFloat(sideDirectionName) + 1 * Time.deltaTime, -1, 0)); }

            }
            if (forwardDirection==0)
            {
                if (_animator.GetFloat(forwardDirectionName) > 0) {_animator.SetFloat(forwardDirectionName, Mathf.Clamp(_animator.GetFloat(forwardDirectionName) - 1f * Time.deltaTime, 0, 1)); }
                if (_animator.GetFloat(forwardDirectionName) < 0) { _animator.SetFloat(forwardDirectionName, Mathf.Clamp(_animator.GetFloat(forwardDirectionName) + 1 * Time.deltaTime, -1, 0)); }

            }
        }
        public void PlayAnimationTrigger(string animationTriggerName,bool isPlay = false)
        {
            if (!isPlay) return;
            _animator.SetTrigger(animationTriggerName);
        }
    }
    public class PhysicAnimatronicController
    {
        #region Variables
        private List<Transform> _rotationTargetList = new List<Transform>();
        private List<ConfigurableJoint> _cJointList = new List<ConfigurableJoint>();
        private List<Quaternion> _startQuaternionList = new List<Quaternion>();
        private List<Collider> _collidersA = new List<Collider>();
        private List<Collider> _collidersB = new List<Collider>();
        #endregion
        public PhysicAnimatronicController(List<Transform> rotationTargetList,List<ConfigurableJoint> cJointList,List<Collider> ignoreColliders)
        {
            for (int i = 0; i < rotationTargetList.Count; i++)
            {
                _rotationTargetList.Add(rotationTargetList[i]);
                _cJointList.Add(cJointList[i]);
                _startQuaternionList.Add(_rotationTargetList[i].localRotation);
            }
            for (int i = 0; i < ignoreColliders.Count; i++)
            {
                _collidersA.Add(ignoreColliders[i]);
                _collidersB.Add(ignoreColliders[i]);
            }
        }
        public void OnJointAnimate()
        {
            for (int i = 0; i < _rotationTargetList.Count; i++)
            {
                _cJointList[i].targetRotation = Quaternion.Inverse(_rotationTargetList[i].localRotation);
            }
        }
        public void IgnoreMultipleCollisions(bool isIgnore = true)
        {
            for (int i = 0; i < _collidersA.Count; i++)
            {
                for (int j = 0; j < _collidersB.Count; j++)
                {
                    Physics.IgnoreCollision(_collidersA[i],_collidersB[j],isIgnore);
                }
            }
        }
    }
    public class Inputer:IBaseControllable
    {
        #region IBaseControllable
        public float forwardDirection { get; private set; }
        public float sideDirection { get; private set; }
        public string animationName { get; private set; }
        public bool isActive { get; private set; }
        public void SetDirection(float forwardDir, float sideDir)
        {
            forwardDirection = forwardDir;
            sideDirection = sideDir;
        }
        public void SetName(string animName)
        {
            animationName = animName;
        }
        public void SetActive(bool isActive = true)
        {
            this.isActive = isActive;
        }
        #endregion

        public Inputer()
        {
            
        }

    }
    public class CameraFollower
    {
        private Camera _camera;
        private Vector3 _offset;
        private float _t;
        public CameraFollower(Camera camera,Vector3 offset,float t)
        {
            _camera = camera;
            _offset = offset;
            _t = t;
        }

        public void FreeCameraSlerpFollow(Transform target)
        {
            var dir = target.position - _camera.transform.position;
            var lookRotation = Quaternion.LookRotation(dir);
            _camera.transform.position = Vector3.Slerp(_camera.transform.position, target.position, _t * Time.deltaTime) + _offset;
            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,lookRotation, _t * Time.deltaTime);
        }
        public void SetActive(bool isActive = true)
        {
            
        }
    }
    public class AIController:IAI
    {
        private Rigidbody _rb;
        private NavMeshAgent _agent;
        private Transform _searcher;
        private Transform _navMesher;
        private Animator _searcherAnimator;
        private Transform _defaultWaypoint;
        private LayerMask _targetLayer;
        private IAI _iai;
        private bool _atk =true;
        public AIController(Transform searcher,Transform transform,Transform navMesher,Transform defaultWaypoint,IAI iai,LayerMask targetLayer)
        {
            _searcher = searcher;
            _iai = iai;
            _targetLayer = targetLayer;
            _navMesher = navMesher;
            _defaultWaypoint = defaultWaypoint;
            _iai.SetDelay(true);
            _navMesher.gameObject.AddComponent<NavMeshAgent>();
            if (_navMesher.TryGetComponent<NavMeshAgent>(out NavMeshAgent nav)) { _agent = nav; }
            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb)) { _rb = rb; }
            if (searcher.TryGetComponent<Animator>(out Animator anim)) { _searcherAnimator = anim; }
        }
        public AIController()
        {

        }

        #region IAI
        public Transform detectedTarget { get; private set; }
        public bool isDetected { get; private set; }
        public bool isAttackable { get; private set; }
        public bool isDelay { get; private set; }
        public void SetDelay(bool isdelay)
        {
            isDelay = isdelay;
        }
        public void SetAttackable(bool isAtttacked = false)
        {
            isAttackable = isAtttacked;
        }
        public void SetDetectedTarget(Transform detectedTarg, bool isDetect = false)
        {
            detectedTarget = detectedTarg;
            isDetected = isDetect;
        }
        #endregion

        public void OnSearching(float searchDistance,float attackableDistance)
        {
            if (EnemySearching(searchDistance, out Collider col)) { _iai.SetDetectedTarget(col.transform, true); } else { _iai.SetDetectedTarget(_defaultWaypoint, false); }
            _agent.SetDestination(_iai.detectedTarget.position);
            if (_agent != null&&_iai.isDetected)
            {
                var attackedDistance = Vector3.Distance(_rb.transform.position,_iai.detectedTarget.position);
                if (attackedDistance <= attackableDistance) { _iai.SetAttackable(true); } else { _iai.SetAttackable(false); }
                _searcherAnimator.enabled = false;
                _searcher.LookAt(_iai.detectedTarget.position);
            }
            if(!_iai.isDetected)
            {
                _searcherAnimator.enabled = true;
            }
        }
        public void AiRigidbodyFollowToNavigation(float followDistance,float navStopDistance,float rotationSpeed,IBaseControllable ibaseControllable)
        {
            var dist = MoveDistance(out Quaternion lookRotation);

            #region Rigidbody move condition
            if (dist > followDistance)
            {
                _rb.transform.localRotation = Quaternion.Slerp(_rb.rotation,lookRotation,rotationSpeed * Time.deltaTime);
                ibaseControllable.SetDirection(1, 0);
                
            }else
            {
                ibaseControllable.SetDirection(0,0);
            }
            #endregion

            #region Navigation move condition
            if (dist > navStopDistance)
            {
                _agent.speed = 0;
            }else
            {
                _agent.speed = 3.5f;
            }
            #endregion
        }
        public void OnAttackReason(Delayer delayer,AiPhysicCharacter aiChar)
        {
            
            if(_iai.isAttackable)
            {
                if(_iai.isDelay)
                {
                    aiChar.DelayStart();
                }
                _atk = false;
            }
        }
        private bool EnemySearching(float searchDistance,out Collider col)
        {
            Ray ray = new Ray(_searcher.position,_searcher.forward * searchDistance);
            Debug.DrawRay(ray.origin, ray.direction * searchDistance, Color.red);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,searchDistance,_targetLayer))
            {
                col = hit.collider;
                return true;
            }
            col = null;
            return false;
        }
        private float MoveDistance(out Quaternion lookRotationXZ)
        {
            var dir = _navMesher.localPosition - _rb.transform.localPosition;
            var dirXZ = new Vector3(dir.x,0,dir.z);
            lookRotationXZ = Quaternion.LookRotation(dirXZ);
            return Vector3.Distance(_rb.transform.position,_navMesher.position);
        }

    }
    public class Delayer:Courotiner
    {
        public Delayer(bool tap)
        {
            _tap = tap;
        }
    }
    public abstract class Courotiner
    {
        protected bool _tap;
        public IEnumerator OnDelaye(IAI iai)
        {
            iai.SetDelay(false);
            yield return new WaitForSeconds(2f);
            _tap = true;
            yield return new WaitForSeconds(0.01f);
            _tap = false;
            iai.SetDelay(true);
        }
    }
}