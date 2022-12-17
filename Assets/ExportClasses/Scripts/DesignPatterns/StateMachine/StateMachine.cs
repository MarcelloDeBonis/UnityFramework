using UnityEngine;

namespace Utils.StateMachine
{
    /// <summary>
    /// State Machine base class
    /// </summary>
    public abstract class StateMachine : MonoBehaviour
    {
        /// <summary>
        /// Current available state
        /// </summary>
        private BaseState _currentState;
        
#region MonoBehaviour

        // Start is called before the first frame update
        private void Start()
        {
            _currentState = GetInitialState();
            
            if (_currentState != null)
                _currentState.EnterState();
        }
        
        // FixedUpdate *should* be called when dealing with physics
        private void FixedUpdate()
        {
            if (_currentState != null)
                _currentState.UpdatePhysics();
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (_currentState == null) return;
            _currentState.CaptureInput();
            _currentState.UpdateLogic();
            _currentState.ChangeState();
        }

        // LateUpdate is called once per frame and it *should* be called when dealing with later updates in the same frame
        private void LateUpdate()
        {
            if (_currentState != null)
                _currentState.LateUpdateLogic();
        }

        #endregion

#region Methods
        
        /// <summary>
        /// Returns the initial state (to be defined in child class)
        /// </summary>
        /// <returns>The initial state</returns>
        protected virtual BaseState GetInitialState()
        {
            return null;
        }
        
        /// <summary>
        /// Change states at any time
        /// </summary>
        /// <param name="newState">New current state</param>
        public void SetState(BaseState newState)
        {
            _currentState.ExitState();

            _currentState = newState;
            newState.EnterState();
        }
        
#endregion
    
    }
}