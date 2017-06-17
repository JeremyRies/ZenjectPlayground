using System;
using System.Linq;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;

namespace Plugins.Zenject.OptionalExtras.Scripts.Ship
{
    public class Bullet : MonoBehaviour, IProjectile
    {
        private Setting _settings;
        private Vector2 _direction;
        BulletShotSignal _bulletShotSignal;

        [Inject]
        public void Construct(Setting settings, BulletShotSignal bulletShotSignal)
        {
            _settings = settings;
            _bulletShotSignal = bulletShotSignal;
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
            _bulletShotSignal.Fire();

        }

        private void FixedUpdate()
        {
            var newPos = (Vector2) transform.position + (_direction.normalized * _settings.Speed);
            transform.position = newPos;
        }

        public class BulletPool : MonoMemoryPool<Vector2, Vector2, Bullet>
        {
            protected override void Reinitialize(Vector2 position, Vector2 direction, Bullet item)
            {
                item.Shoot(position, direction);
            }
        }
    }
}