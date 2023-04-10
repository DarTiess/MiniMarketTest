using System;
using System.Collections.Generic;
using Garden;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Store
{
    public class Store: MonoBehaviour
    {
        [SerializeField]
        private TriggerObserver _trigger;
        [SerializeField] private List<VegetableObject> _vegetables;
        private int _vegInStore=0;
       
        private void Start()
        {
            CleanStorePlace();
        }

        private void CleanStorePlace()
        {
            foreach (VegetableObject vegetable in _vegetables)
            {
                vegetable.gameObject.SetActive(false);
            }
        }

        public void StackIn()
        {
            if (_vegInStore <= _vegetables.Count)
            {
                _vegetables[_vegInStore].gameObject.SetActive(true);
                _vegInStore++;
            }
           
        }
    }
}