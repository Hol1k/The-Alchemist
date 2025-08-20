using System;
using Game.Scripts.Cauldron;
using UnityEngine;

namespace Game.Scripts.Ingredients
{
    public class IngredientCollisionScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WaterWavesController wavesController))
                wavesController.AddWave(new Vector2(transform.position.x, transform.position.z));
        }
    }
}
