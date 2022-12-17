namespace Utils.StateMachine
{
    /// <summary>
    /// State base class
    /// </summary>
    public abstract class BaseState
    {
        private readonly string _name;
        protected StateMachine stateMachine;
        
        /// <summary>
        /// BaseState constructor capable of binding a list of states to a state machine
        /// </summary>
        /// <param name="name">State name</param>
        /// <param name="stateMachine">State Machine to bind with</param>
        protected BaseState(string name, StateMachine stateMachine)
        {
            _name = name;
            this.stateMachine = stateMachine;
        }
        
        /// <summary>
        /// Returns the name of a specific state
        /// </summary>
        /// <returns>The state name as a string</returns>
        public static string GetStateName(BaseState state)
        {
            return state._name;
        }
        
        public virtual void EnterState() {}
        
        public virtual void UpdatePhysics() {}
        public virtual void CaptureInput() {}
        public virtual void UpdateLogic() {}
        public virtual void ChangeState() {}
        public virtual void LateUpdateLogic() {}
        
        public virtual void ExitState() {}
    }
}