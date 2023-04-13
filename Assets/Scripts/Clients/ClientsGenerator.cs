using System;
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
        [FormerlySerializedAs("path")]
        [SerializeField] private List<Transform> _path;

        private float _timer;
        private List<ClientMove> _clientsList = new List<ClientMove>();
        private int _indexClient=0;
        private int _limitIndex=0;
        private List<Transform> _path2 = new List<Transform>();
       

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

        public void OpenNewStore(Transform target)
        {
            CreatePathVersion2(target);
        }

        private void CreateClientsList()
        {
            for (int i = 0; i < _clientCount; i++)
            {
                int rndClient = Random.Range(0, _clientsListPrefabs.Count);
                float rndSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);

                ClientMove client = Instantiate(_clientsListPrefabs[rndClient], transform.position, transform.rotation);
                client.transform.parent = gameObject.transform;
                client.Initialize(rndSpeed, _path, this);
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

            SetPathToClient();
            _indexClient++;
            _limitIndex++;
        }

        private void SetPathToClient()
        {
            if (_path2.Count > 0)
            {
                RandomizePath();
            }
            else
            {
                _clientsList[_indexClient].BeginPath(_path);
            }
        }

        private void RandomizePath()
        {
            int rndPath = Random.Range(0, 2);
            switch (rndPath)
            {
                case 0:
                    _clientsList[_indexClient].BeginPath(_path);
                    break;
                case 1:
                    _clientsList[_indexClient].BeginPath(_path2);
                    break;
            }
        }

        private void CreatePathVersion2(Transform target)
        {
            _path2.Add(target);
            for (int i = 1; i < _path.Count; i++)
            {
                _path2.Add(_path[i]);
            }
        }
    }
}