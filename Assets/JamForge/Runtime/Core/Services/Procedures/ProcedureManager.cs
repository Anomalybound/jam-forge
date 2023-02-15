using JamForge.StateMachine;
using UnityEngine;

namespace JamForge.Procedures
{
    public enum ProcedureState
    {
        None,
        Init,
        Update,
        Shutdown,
    }

    public class InitProcedureBase : GameProcedure<ProcedureManager, ProcedureState>
    {
        public override ProcedureState Index => ProcedureState.None;

        protected override void OnEnter()
        {
            Jam.Logger.Debug($"Procedure: {Index}");
        }
    }

    public class ProcedureManager : GameProcedureController<ProcedureManager, ProcedureState> { }
}
