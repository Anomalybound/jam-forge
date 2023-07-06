using JamForge.StateMachine;
using JetBrains.Annotations;
using UnityEngine;

namespace JamForge
{
    public abstract class ProcedureBase : State
    {
        [UsedImplicitly]
        public GameProcedures GameProcedures { get; internal set; }
    }

    public class GameProcedures : MonoBehaviour
    {
        [SerializeField] private string defaultState;
        [SerializeField] private string[] procedures;

        public IStateMachineRunner<string, ProcedureBase> StateMachineRunner { get; private set; }

        private void Start()
        {
            StateMachineRunner = new StateMachineRunner<string, ProcedureBase>();

            for (var i = 0; i < procedures.Length; i++)
            {
                var procedureType = TypeFinder.Get(procedures[i]);
                if (procedureType == null) { continue; }

                if (Jam.Resolver.Create(procedureType) is not ProcedureBase procedure) { continue; }

                procedure.GameProcedures = this;

                var procedureName = procedureType.Name;
                StateMachineRunner.AddState(procedureName, procedure);
            }

            if (string.IsNullOrEmpty(defaultState))
            {
                defaultState = procedures[0];
            }

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
