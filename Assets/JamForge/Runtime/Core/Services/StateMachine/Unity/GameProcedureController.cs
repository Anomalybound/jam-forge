using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JamForge.StateMachine.Core;
using UnityEngine;

namespace JamForge.StateMachine
{
    public abstract class GameProcedureController<TProcedureController, TProcedureIndex> : FsmContainer
        where TProcedureController : GameProcedureController<TProcedureController, TProcedureIndex>
        where TProcedureIndex : struct, IConvertible
    {
        private readonly Dictionary<TProcedureIndex, GameProcedure<TProcedureController, TProcedureIndex>> _indices = new();

        private readonly Dictionary<IState, TProcedureIndex> _indexLookup = new();

        [SerializeField, HideInInspector]
        private TProcedureIndex initState;

        public TProcedureIndex InitState => initState;

        public TProcedureIndex Current => _indexLookup[RootNode.ActiveChildrenStates.Peek()];

        public event Action<IState, IState> OnStateChanged;

        protected override IState BuildState()
        {
            var root = new State();
            var types = GetType().Assembly.GetTypes()
                .Where(x => !x.IsAbstract && typeof(GameProcedure<TProcedureController, TProcedureIndex>).IsAssignableFrom(x));

            var procedures = new List<GameProcedure<TProcedureController, TProcedureIndex>>();

            foreach (var type in types)
            {
                if (Jam.Resolver.Create(type) is not GameProcedure<TProcedureController, TProcedureIndex> instance)
                {
                    continue;
                }

                instance.SetContext((TProcedureController)this);
                procedures.Add(instance);
            }

            procedures = procedures.OrderBy(x => x.Index).ToList();

            foreach (var procedure in procedures)
            {
                var id = procedure.Index;

                if (_indices.ContainsKey(id))
                {
                    Debug.LogErrorFormat("{0}[{1}] already added.", id, procedure.GetType().Name);
                    continue;
                }

                _indices.Add(id, procedure);
                _indexLookup.Add(procedure, id);
                root.AddChild(id.ToString(CultureInfo.InvariantCulture), procedure);
            }

            RootNode = root;

            if (procedures.Count <= 0)
            {
                return RootNode;
            }

            if (procedures.Any(p => p.Index.Equals(InitState)))
            {
                ChangeState(InitState);
            }
            else
            {
                var first = procedures[0].Index;
                Jam.Logger.Warn($"Procedure of [{InitState}] is no available, change to {first} instead.");
                ChangeState(first);
            }

            return RootNode;
        }

        #region Facade

        public void ClearState()
        {
            TrackStateEvent(() => RootNode.ClearStates());
        }

        public void ChangeState(TProcedureIndex index)
        {
            TrackStateEvent(() => RootNode.ChangeState(index.ToString(CultureInfo.InvariantCulture)));
        }

        public void PushState(TProcedureIndex index)
        {
            TrackStateEvent(() => RootNode.PushState(index.ToString(CultureInfo.InvariantCulture)));
        }

        public void ChangeState(string stateName)
        {
            TrackStateEvent(() => RootNode.ChangeState(stateName));
        }

        public void PushState(string stateName, bool allowDuplicated = false)
        {
            TrackStateEvent(() => RootNode.PushState(stateName, allowDuplicated));
        }

        public void PopState(bool includingRoot)
        {
            TrackStateEvent(() => RootNode.PopState(includingRoot));
        }

        public void TriggerEvent(string eventId, EventArgs args)
        {
            TrackStateEvent(() => RootNode.TriggerEvent(eventId, args));
        }

        private void TrackStateEvent(Action action)
        {
            var previousActive =
                RootNode.ActiveChildrenStates.Count > 0 ? RootNode.ActiveChildrenStates.Peek() : null;

            action?.Invoke();

            var currentActive =
                RootNode.ActiveChildrenStates.Count > 0 ? RootNode.ActiveChildrenStates.Peek() : null;

            if (previousActive != currentActive)
            {
                OnStateChanged?.Invoke(previousActive, currentActive);
            }
        }

        #endregion
    }
}
