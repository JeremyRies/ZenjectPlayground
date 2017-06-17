using UniRx;
using UnityEngine;
using Zenject;

namespace Plugins.Zenject.OptionalExtras.FFG
{
    public class InputController : ITickable
    {
        public ReactiveCommand ShootCommand { get; set; }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootCommand.Execute();
            }
        }
    }
}
