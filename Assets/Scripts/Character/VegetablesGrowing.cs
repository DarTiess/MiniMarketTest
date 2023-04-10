using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VegetablesGrowing : MonoBehaviour
{
    [SerializeField] private GameObject _vegetablePrefabs;
   
    [SerializeField] private float _width;
    [SerializeField] private float _depth;

    private Transform _ground;
    private List<GameObject> _grass = new List<GameObject>();

    void Start()
    {
        GroundVegetebales();
    }

    private void GroundVegetebales()
    {
        _ground = transform;
        float groundWidthHalf = _width / 2;
        float groundDepthHalf = _depth / 2;

       
            Vector3 position = transform.position +
                               new Vector3(Random.Range(-groundWidthHalf, groundWidthHalf), 0, Random.Range(-groundDepthHalf, groundDepthHalf));

         //   GameObject newGrass = Instantiate(_vegetablePrefabs[Random.Range(0, _vegetablePrefabs.Count)], position, Quaternion.Euler(0, Random.Range(0, 360), 0),
        //                                      _ground.transform);

       //     _grass.Add(newGrass);
        

      
    }
}
