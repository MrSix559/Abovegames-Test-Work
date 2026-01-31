namespace Tools
{
    public abstract class FsmState
    {
        protected readonly Fsm _fsm;

        public FsmState(Fsm fsm)
        {
            _fsm = fsm;
        }
        public virtual void Init(object data = null) { }
        public virtual void EnterState() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void LateUpdateState() { }
        public virtual void ExitState() { }
    }
}
