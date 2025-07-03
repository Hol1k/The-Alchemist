using Game.Scripts.Cauldron;
using Game.Scripts.Ingredients;
using UnityEngine;
using Zenject;

public class CookInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<IngredientSpawner>().FromComponentInHierarchy().AsTransient();
        
        Container.BindInterfacesAndSelfTo<CookController>().FromComponentInHierarchy().AsSingle();
    }
}