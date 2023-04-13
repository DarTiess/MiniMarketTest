using DefaultNamespace.Clients;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private LevelEventService _levelEventService;
   
    [SerializeField] private Economics _economics;
    [SerializeField] private ClientsGenerator _clientsGenerator;
        

 
    public override void InstallBindings()
    {
        BindLevelManager();
        BindEconimics();
        BindClientsGenerator();
    }

    private void BindLevelManager()
    {
        Container.Bind<LevelEventService>().FromInstance(_levelEventService).AsSingle();
    }


    private void BindClientsGenerator()
    {
        Container.Bind<ClientsGenerator>().FromInstance(_clientsGenerator).AsSingle();
    }

    private void BindEconimics()
    {
        Container.Bind<Economics>().FromInstance(_economics).AsSingle();
    }
}