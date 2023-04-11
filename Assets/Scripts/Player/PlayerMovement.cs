using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rotationSpeed;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private NavMeshAgent _nav;
    private CharacterAnimator _animator;
    private Vector3 _temp;
   

    private void Start()
    {
        _animator = GetComponent<CharacterAnimator>();
        _nav = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float inputHorizontal = SimpleInput.GetAxis(HORIZONTAL_AXIS);
        float inputVertical = SimpleInput.GetAxis(VERTICAL_AXIS);

        _temp.x = inputHorizontal;
        _temp.z = inputVertical;

        _animator.MoveAnimation(_temp.magnitude);

        _nav.Move(_temp * _playerSpeed * Time.fixedDeltaTime);

        Vector3 tempDirect = transform.position + Vector3.Normalize(_temp);
        Vector3 lookDirection = tempDirect - transform.position;
        if (lookDirection != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
