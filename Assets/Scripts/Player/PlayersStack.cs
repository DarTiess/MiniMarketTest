using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Store;
using Garden;
using UnityEngine;
using Vegetables;

public class PlayersStack : MonoBehaviour, IStack
{

    public event Action IsFull;
    public event Action IsFreeStack;
    public event Action CanStacking;


    [SerializeField] private Transform _stackPlace;
    [SerializeField] private VegetableInPlayer _vegetablePrefab;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _objectHeight;
    [SerializeField] private int _limitOfStack;

    private int _vegIndex = 0;

    private float _vegPosition;

    private List<VegetableInPlayer>_objInStack = new List<VegetableInPlayer>();
    private bool _onSaling;
    private bool _isFullingStack;
    private bool _stacking;

    private CharacterAnimator _animator;

    private void Start()
    {
        _animator = GetComponent<CharacterAnimator>();
        _vegPosition = _stackPlace.position.y;
        CreateVegetablesStackInactive();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Garden"))
        {
            if (_isFullingStack)
            {
                return;
            }
            if (!_stacking)
            {
                _stacking = true;
                StartCoroutine(TryGetVegetable(other.GetComponent<GardenGrowing>()));
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Garden"))
        {
            if (_stacking)
            {
                StopAllCoroutines();
                _stacking = false;
            }
           
        }
    }

    public void StackIn()
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
            ReplaceStackPositions();
        }
    }

    public void StackOut()
    {
       _objInStack[_vegIndex-1].gameObject.SetActive(false);
       
        _vegIndex--;
      
        _isFullingStack = false;
        _onSaling = false;
        _stacking = false;
        if (_vegIndex <= 0)
        {
            _animator.StackEmpty();
            ReplaceStackPositions();
          
        } 
        IsFreeStack?.Invoke();
    }

    private IEnumerator TryGetVegetable(GardenGrowing garden)
    {
        bool isPulling= garden.PullVegetable(this);
        while (!isPulling)
        {
            isPulling = garden.PullVegetable(this);
            yield return null;
        }
    }

    private void ReplaceStackPositions()
    {
        _vegPosition = _stackPlace.position.y;
        foreach (VegetableInPlayer vegetableObject in _objInStack)
        {
            vegetableObject.transform.position=
                new Vector3(_stackPlace.position.x, _vegPosition, _stackPlace.position.z);
           
            _vegPosition += +_objectHeight;
        }
    }

    private void OutOfStack(Store target)
    {
        if (target.IsFull)
        {
            _onSaling = false;
            return;
        }
        _objInStack[_vegIndex-1].PushingFromPlayer(target, this);
    }

    private void CreateVegetablesStackInactive()
    {
        for (int i = 0; i < _limitOfStack; i++)
        {
            VegetableInPlayer pref = Instantiate(_vegetablePrefab, _stackPlace.position, _stackPlace.rotation);
            pref.Init(_jumpDuration, _jumpForce);
         
            pref.transform.position =
                new Vector3(_stackPlace.position.x, _vegPosition, _stackPlace.position.z);
           
            _vegPosition += +_objectHeight;
            pref.transform.parent = _stackPlace;
            pref.gameObject.tag = "Untagged";
            pref.gameObject.SetActive(false);
            _objInStack.Add(pref);
            
        }
    }
}
