using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabsInstaller", menuName = "Installers/PrefabsInstaller")]
public class PrefabsInstaller : ScriptableObjectInstaller<PrefabsInstaller>
{
    [SerializeField] private GameObject player;
    [SerializeField] private Bullet bullet;
    public override void InstallBindings()
    {
        Player newPlayer = Container.InstantiatePrefabForComponent<Player>(player);
        Container.Bind<Player>().FromInstance(newPlayer).AsSingle();
        Container.BindInstance(bullet);
    }
}