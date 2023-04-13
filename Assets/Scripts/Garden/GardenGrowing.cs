using System.Collections.Generic;
using UnityEngine;
using Vegetables;
using Random = UnityEngine.Random;

namespace Garden
{
    public class GardenGrowing: MonoBehaviour
    {
        [SerializeField] private VegetableType vegetableType;
            
        [SerializeField] private List<VegetableInGarden> vegetables;
        [Header("Vegetables Settings")]
        [SerializeField] private float _growingSpeed;
        [SerializeField] private float _readyHeight;
        [SerializeField] private float _maxTimerToGrow;

        public VegetableType VegetableType => vegetableType;

        private List<VegetableInGarden> readyVegetables = new List<VegetableInGarden>();
        
        private void Start()
        {
           GrowingVegetables();
        }

        private void GrowingVegetables()
        {
            foreach (VegetableInGarden vegetable in vegetables)
            {
                float rndTimer = Random.Range(0, _maxTimerToGrow);
                vegetable.InitVegetable(_growingSpeed, _readyHeight, rndTimer, this);
               
            }
        }

        public bool PullVegetable(PlayersStack target)
        {
            if (readyVegetables.Count <= 0)
            {
                return false;
            }
            int lastVeg = readyVegetables.Count - 1;
            readyVegetables[lastVeg].PushingToPlayer(target);
            readyVegetables.RemoveAt(lastVeg);
            return true;
        }


        public void VagetableIsReady(VegetableInGarden vegetableInGarden)
        {
            readyVegetables.Add(vegetableInGarden);
        }

      
    }
}