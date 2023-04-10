using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(float speed)
    {
        _animator.SetFloat("IsMove", speed);
    }


    public void HasStack()
    {
        _animator.SetBool("HasStack", true);
    }
    
    public void StackEmpty()
    {
        _animator.SetBool("HasStack", false);
    }
}
