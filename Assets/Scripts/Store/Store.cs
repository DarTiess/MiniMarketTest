using System.Collections.Generic;
using DefaultNamespace.Clients;
using UnityEngine;
using Vegetables;

namespace DefaultNamespace.Store
{
    public class Store: MonoBehaviour, IStack
    {
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private List<VegetableInStore> _vegetables;
        public int _vegInStore=0;
        public bool IsFull => _vegInStore >= _vegetables.Count;
        public bool NotEmpty => _vegInStore > 0;

        private void Start()
        {
            CleanStorePlace();
        }

        public void StackIn()
        {
            if (_vegInStore < _vegetables.Count)
            { 
                _vegetables[_vegInStore].gameObject.SetActive(true);
                _vegInStore++;
            }
        }

        public void StackOut()
        {

            _vegetables[_vegInStore-1].gameObject.SetActive(false);
       
            _vegInStore--;
          
        }

        public void PullVegetableToClient(ClientStack client)
        {
            _vegetables[_vegInStore-1].PushingToClient(client, this);
        }

        private void CleanStorePlace()
        {
            foreach (VegetableInStore vegetable in _vegetables)
            {
                vegetable.Init(_jumpDuration, _jumpForce);
                vegetable.gameObject.SetActive(false);
                
            }
        }
    }
}