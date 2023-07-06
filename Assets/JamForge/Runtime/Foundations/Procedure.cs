using System;
using JamForge.StateMachine;
using UnityEngine;
using VContainer;

namespace JamForge
{
    public abstract class GameProcedure : State { }

    public class GameProcedureManager : MonoBehaviour
    {
        [SerializeField] private string defaultState;
        [SerializeField] private string[] procedures;

        public IStateMachineRunner<string, GameProcedure> StateMachineRunner { get; private set; }

        private void Awake()
        {
            StateMachineRunner = new StateMachineRunner<string, GameProcedure>();
            
            // TODO: Instantiate procedure instances
        }

        private void Start()
        {
            StateMachineRunner.Start(defaultState);
        }

        private void Update()
        {
            StateMachineRunner.Run(Time.deltaTime);
        }

        public void SwitchState(string stateIndex)
        {
            StateMachineRunner.SwitchState(stateIndex);
        }
    }
}
