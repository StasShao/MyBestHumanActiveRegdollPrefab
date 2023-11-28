using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaoGameMechanicSys
{
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
}