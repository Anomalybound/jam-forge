using System;

namespace JamForge.StateMachine
{
    public interface IStateMachineRunner<TIndex, TState>
        where TState : IState
        where TIndex : IEquatable<TIndex>, IComparable<TIndex>
    {
        #region Properties

        TIndex ActiveStateIndex { get; }

        #endregion

        #region Methods

        void Start(TIndex index);

        void Run(float deltaTime);

        void SwitchState(TIndex index);

        void AddState(TIndex index, TState state);

        void AddTransition(TIndex from, TIndex to, Func<TState, bool> condition);

        #endregion

        #region Events

        public event Action<TIndex, TState> StateEnterEvent;

        public event Action<TIndex, TState> StateExitEvent;

        public event Action<TIndex, TState, float> StateUpdateEvent;

        public event Action<TIndex, TState, TIndex, TState> StateTransitionEvent;

        #endregion
    }
}
