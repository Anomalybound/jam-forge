using Hermit.Fsm;

namespace DefaultNamespace
{
    public class TestGameProcedure : GameProcedure<TestProcedureManager,ProcedureIndex >
    {
        public override ProcedureIndex Index => ProcedureIndex.Init;
    }
}
