using Plugins.Zenject.OptionalExtras.FFG;
using UniRx;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;
using Zenject.SpaceFighter;

#pragma warning disable 649

namespace Plugins.Zenject.OptionalExtras.Scripts.Ship
{
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer _meshRenderer;

        [SerializeField]
        ParticleEmitter _particleEmitter;

        ShipStateFactory _stateFactory;
        ShipState _state = null;

        private InputController _inputController;
        private Bullet.BulletFactory _bulletFactory;

        [Inject]
        public void Construct(ShipStateFactory stateFactory, InputController inputController, Bullet.BulletFactory bulletFactory)
        {
            _stateFactory = stateFactory;
            _inputController = inputController;
            _inputController.ShootCommand = new ReactiveCommand();
            _bulletFactory = bulletFactory;

            _inputController.ShootCommand.Subscribe(_ =>
            {
                var bullet = _bulletFactory.Create();
                bullet.Shoot(transform.position, transform.rotation.eulerAngles);
            });
        }

        public MeshRenderer MeshRenderer
        {
            get { return _meshRenderer; }
        }

        public ParticleEmitter ParticleEmitter
        {
            get { return _particleEmitter; }
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Quaternion Rotation
        {
            get { return transform.rotation; }
            set { transform.rotation = value; }
        }

        public void Start()
        {
            ChangeState(ShipStates.WaitingToStart);
        }

        public void Update()
        {
            _state.Update();
        }

        public void OnTriggerEnter(Collider other)
        {
            _state.OnTriggerEnter(other);
        }

        public void ChangeState(ShipStates state)
        {
            if (_state != null)
            {
                _state.Dispose();
                _state = null;
            }

            _state = _stateFactory.CreateState(state);
            _state.Start();
        }
    }
}

