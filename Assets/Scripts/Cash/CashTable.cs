using System.Collections.Generic;
using DefaultNamespace.Clients;
using UnityEngine;

namespace DefaultNamespace.Cash
{
    public class CashTable: MonoBehaviour
    {
        public Queue<ClientStack> _clientsList = new Queue<ClientStack>();
        public int ClientCount => _clientsList.Count;

        public void StayOnQueue(ClientStack clientStack)
        {
            _clientsList.Enqueue(clientStack);
        }

        public void CreateBox()
        {
            _clientsList.Dequeue().StackOut();
           
        }
    }
}