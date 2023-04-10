using System;
using System.Collections.Generic;
using DefaultNamespace.Store;
using Garden;
using UnityEngine;

public class PlayersStack : MonoBehaviour
{

    public event Action IsFull;
    public event Action CanStacking;


    [SerializeField] private Transform _stackPlace;
    [SerializeField]
    private VegetableObject _vegetablePrefab;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _objectHeight;
    [SerializeField] private int _limitOfStack;

    private int _vegIndex = 0;
  
    private float _vegPosition;
    
    private List<VegetableObject>_objInStack = new List<VegetableObject>();
    private bool _onSaling;
    private bool _isFullingStack;
    private bool _stacking;

    private PlayerAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<PlayerAnimator>();
        _vegPosition = _stackPlace.position.y;
        CreateVegetablesStackInactive();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Garden"))
        {
            if (!_stacking)
            {
                _stacking = true;
                other.GetComponent<GardenGrowing>().PullVegetable(this);
            }
        }

        if (other.gameObject.CompareTag("Store"))
        {
            if (!_onSaling && _vegIndex>0)
            {
                _onSaling = true;
               OutOfStack(other.gameObject.GetComponent<Store>());
            }
        }
       
    }


    public void StackVegetable()
    {
        if (!_isFullingStack)
        {
            _animator.HasStack();
            _objInStack[_vegIndex].gameObject.SetActive(true);
            _vegIndex++;
            if (_vegIndex >= _objInStack.Count)
            {
                _isFullingStack = true;
                IsFull?.Invoke();
            }
            else
            {
                _stacking = false;
            }
        }
    }

    private void OutOfStack(Store target)
    {
        if (_vegIndex >0)
        {
            
            _objInStack[_vegIndex].PushingFromPlayer(target, this);
        }
    }

    private void CreateVegetablesStackInactive()
    {
        for (int i = 0; i < _limitOfStack; i++)
        {
            var pref = Instantiate(_vegetablePrefab, _stackPlace.position, _stackPlace.rotation);
            pref.transform.position = new Vector3(_stackPlace.position.x, _vegPosition, _stackPlace.position.z);
            _vegPosition += +_objectHeight;
            pref.transform.parent = _stackPlace;
            pref.gameObject.tag = "Untagged";
            pref.gameObject.SetActive(false);
            _objInStack.Add(pref);
            
        }
    }


    public void StackOut()
    {
        _objInStack[_vegIndex].gameObject.SetActive(false);
        _vegIndex --;

        _isFullingStack = false;
        _onSaling = false;
    }
}
