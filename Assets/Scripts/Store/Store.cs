using System.Collections.Generic;
using DefaultNamespace.Clients;
using Garden;
using UnityEngine;
using Vegetables;

namespace DefaultNamespace.Store
{
    public class Store: MonoBehaviour, IStack
    {
        [SerializeField]
        private float _jumpDuration;
        [SerializeField]
        private float _jumpForce;
        [SerializeField] private List<VegetableInStore> _vegetables;
        private int _vegInStore=0;
        public bool IsFull
        {
            get
            {
                if (_vegInStore >= _vegetables.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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

        public bool PullVegetableToClient(ClientStack client)
        {
            if (_vegInStore <= 0)
            {
                return false;
            }
            int lastVeg = _vegetables.Count - 1;
           _vegetables[lastVeg].PushingToClient(client, this);
           return true;
        }

        private void CleanStorePlace()
        {
            foreach (VegetableInStore vegetable in _vegetables)
            {
                vegetable.gameObject.SetActive(false);
                vegetable.InitVegetable(_jumpDuration, _jumpForce);
            }
        }
    }
}