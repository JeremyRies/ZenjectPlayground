using System;
using UnityEngine;
using Zenject;

namespace Plugins.Zenject.OptionalExtras.Scripts.Ship
{
    public class Bullet : MonoBehaviour, IProjectile
    {
        private Setting _settings;
        private Vector2 _direction;

        [Inject]
        public void Construct(Setting settings)
        {
            _settings = settings;
        }

        [Serializable]
        public class Setting
        {
            public float Damage;
            public float Speed;
        }

        public void Shoot(Vector2 position, Vector2 direction)
        {
            transform.position = position;
            _direction = direction;
        }

        private void FixedUpdate()
        {
            var newPos = (Vector2) transform.position + (_direction.normalized * _settings.Speed);
            transform.position = newPos;
        }

        public class BulletFactory : Factory<Bullet>
        {
        }
    }
}