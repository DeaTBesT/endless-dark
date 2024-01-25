using Base;
using MapGeneration;
using UnityEngine;

namespace Managers
{
    public class GameBootstrap : Bootstrap
    {
        [SerializeField] private MapGenerator _mapGenerator;

        public override void OnStartClient()
        {
            _mapGenerator.Initialize();
        }
    }
}