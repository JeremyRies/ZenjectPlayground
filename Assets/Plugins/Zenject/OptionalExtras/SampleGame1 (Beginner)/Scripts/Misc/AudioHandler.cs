using System;
using UnityEngine;
using Zenject;

namespace Zenject.Asteroids
{
    public class AudioHandler : IInitializable, IDisposable
    {
        readonly Settings _settings;
        readonly AudioSource _audioSource;

        ShipCrashedSignal _shipCrashedSignal;

        [Inject]
        BulletShotSignal _bulletShotSignal;

        public AudioHandler(
            AudioSource audioSource,
            Settings settings,
            ShipCrashedSignal shipCrashedSignal)
        {
            _shipCrashedSignal = shipCrashedSignal;
            _settings = settings;
            _audioSource = audioSource;
        }

        public void Initialize()
        {
            _shipCrashedSignal += OnShipCrashed;
            _bulletShotSignal += OnBulletShot;
        }

        public void Dispose()
        {
            _shipCrashedSignal -= OnShipCrashed;
            _bulletShotSignal -= OnBulletShot;
        }

        void OnShipCrashed()
        {
            _audioSource.PlayOneShot(_settings.CrashSound);
        }

        void OnBulletShot() {
            _audioSource.PlayOneShot(_settings.BulletShotSound);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip CrashSound;
            public AudioClip BulletShotSound;
        }
    }
}
