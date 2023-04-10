using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Garden
{
    public class GardenGrowing: MonoBehaviour
    {
        [SerializeField]
        private VegetableType vegetableType;
            
        [SerializeField]
        private List<VegetableObject> vegetables;
        [Header("Vegetables Settings")]
        [SerializeField]
        private float _growingSpeed;
        [SerializeField]
        private float _readyHeight;
        [SerializeField]
        private float _maxTimerToGrow;

        public VegetableType VegetableType => vegetableType;

        private List<VegetableObject> readyVegetables = new List<VegetableObject>();
        
        private void Start()
        {
           GrowingVegetables();
        }

        private void GrowingVegetables()
        {
            foreach (VegetableObject vegetable in vegetables)
            {
                float rndTimer = Random.Range(0, _maxTimerToGrow);
                vegetable.InitVegetable(_growingSpeed, _readyHeight, rndTimer, this);
               
            }
        }

        public void PullVegetable(PlayersStack target)
        {
            if (readyVegetables.Count <= 0)
            {
                return;
            }
            int lastVeg = readyVegetables.Count - 1;
            readyVegetables[lastVeg].PushingToPlayer(target);
            readyVegetables.RemoveAt(lastVeg);
        }


        public void VagetableIsReady(VegetableObject vegetableObject)
        {
            readyVegetables.Add(vegetableObject);
        }
    }
}