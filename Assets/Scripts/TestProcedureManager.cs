using System;
using Hermit.Fsm;
using JamForge;

namespace DefaultNamespace
{
    public enum ProcedureIndex
    {
        Init,
        Game
    }
    public class TestProcedureManager : GameProcedureController<TestProcedureManager, ProcedureIndex>
    {
        private void Start()
        {
        }
    }
}
