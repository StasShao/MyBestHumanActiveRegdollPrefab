using UnityEngine;
[CreateAssetMenu(fileName = "CharacterSettings",menuName ="CharacterSettings/Data")]
public class CharacterSettings : ScriptableObject
{
    [SerializeField][Range(0,2000)]private int _moveForce;
    [SerializeField] [Range(0, 2000)] private int _searchDistance;
    [SerializeField]  private string _forwardAnimationName;
    [SerializeField] private string _sideAnimationName;
    [SerializeField] private LayerMask _targetLayer;
    public float MoveForce { get { return _moveForce; } }
    public float SearchDistance { get { return _searchDistance; } }
    public string ForwardAnimationName { get { return _forwardAnimationName; } }
    public string SideAnimationName { get { return _sideAnimationName; } }
    public LayerMask TargetLayer { get { return _targetLayer; } }
}
