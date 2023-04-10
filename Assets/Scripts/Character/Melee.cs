using UnityEngine;
using Zenject;

public class Melee : MonoBehaviour
{
    private PlayersStack _playerStack;

    [Inject]
    private void Construct(PlayersStack playerStack)
    {
        _playerStack = playerStack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grass"))
        {
         //   _playerStack.CreateBlockOfGrass(other.gameObject.transform);
        }
    }
}
