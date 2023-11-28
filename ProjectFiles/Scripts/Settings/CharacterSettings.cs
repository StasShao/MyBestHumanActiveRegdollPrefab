using UnityEngine;
[CreateAssetMenu(fileName = "CharacterSettings",menuName ="CharacterSettings/Data")]
public class CharacterSettings : ScriptableObject
{
    [SerializeField][Range(0,2000)]private int _moveForce;
    [SerializeField]  private string _forwardAnimationName;
    [SerializeField] private string _sideAnimationName;
    public float MoveForce { get { return _moveForce; } }
    public string ForwardAnimationName { get { return _forwardAnimationName; } }
    public string SideAnimationName { get { return _sideAnimationName; } }
}
