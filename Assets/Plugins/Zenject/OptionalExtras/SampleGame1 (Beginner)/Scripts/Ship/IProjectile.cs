using UnityEngine;

namespace Plugins.Zenject.OptionalExtras.Scripts.Ship
{
    public interface IProjectile
    {
        void Shoot(Vector2 position, Vector2 direction);
    }
}