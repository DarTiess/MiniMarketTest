using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Clients
{
    public class ClientsGenerator: MonoBehaviour
    {
        [SerializeField] private List<ClientMove> _clientsListPrefabs;
        [SerializeField] private int _clientCount;
        [SerializeField] private int _clientsLimitInScene;
        [SerializeField] private bool _endless;
        [SerializeField] private float _timerSpawnClient;
       
        [SerializeField] private float _maxMoveSpeed;
        [SerializeField] private float _minMoveSpeed;
        [SerializeField] private List<Transform> path;

        private float _timer;
        private List<ClientMove> _clientsList = new List<ClientMove>();
        private int _indexClient=0;
        private int _limitIndex=0;

        private void Start()
        {
            CreateClientsList();
            
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer < _timerSpawnClient)
            {
                return;
            }

            if (_limitIndex >= _clientsLimitInScene)
            {
                return;
            }

            SpawnClient();
        }

        public void ClientIsGone(ClientMove clientMove)
        {
            _clientsList[_clientsList.IndexOf(clientMove)].gameObject.SetActive(false);
            
          
            _limitIndex--;
        }

        private void CreateClientsList()
        {
            for (int i = 0; i < _clientCount; i++)
            {
                int rndClient = Random.Range(0, _clientsListPrefabs.Count);
                float rndSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);

                ClientMove client = Instantiate(_clientsListPrefabs[rndClient], transform.position, transform.rotation);
                client.Initialize(rndSpeed, path, this);
                client.gameObject.SetActive(false);
                _clientsList.Add(client);
                
            }

            _timer = _timerSpawnClient-1f;
        }

        private void SpawnClient()
        { 
            _timer = 0;
            if (_endless)
            {
                if (_indexClient >= _clientsList.Count)
                {
                    _indexClient = 0;
                }
            }
            if (_indexClient >= _clientsList.Count)
            {
                return;
            }
          
            while (_clientsList[_indexClient].gameObject.activeInHierarchy)
            {
                _indexClient++;
            }

            _clientsList[_indexClient].gameObject.transform.position = transform.position;
            _clientsList[_indexClient].gameObject.SetActive(true);
            _clientsList[_indexClient].BeginPath();
            _indexClient++;
            _limitIndex++;
        }
    }
}