using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Adam.Game.Event;

namespace Adam.Game.Event {
    /// <summary>
    /// serialize it in order to show in inspector
    /// </summary>
    [System.Serializable]
    public class MyEvent : UnityEvent<MyUnityEvent.PlayerStates> { }
}


namespace Adam.Game{

    public class MyUnityEvent : MonoBehaviour
    {
        /// <summary>
        /// zombie States which represents different animation
        /// </summary>
        public enum PlayerStates                                            //模型动画
        {
            idle = 0,
            attackReady,
            walk,
            skill1,
            skill2,
            sitDown,
        }
        /// <summary>
        /// Defined My new event in order to pass parameter "PlayerStates" to the function 
        /// which has defined in Animation Utility
        /// </summary>
        public MyEvent m_Event;

        [SerializeField]
        //use to debug
        private PlayerStates nextState = new PlayerStates();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                nextState = PlayerStates.attackReady;
                m_Event.Invoke(nextState);//Invoke the Event and notice the function which subscribe it and pass nextState to the function
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                nextState = PlayerStates.walk;
                m_Event.Invoke(nextState);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                nextState = PlayerStates.idle;
                m_Event.Invoke(nextState);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                nextState = PlayerStates.sitDown;
                m_Event.Invoke(nextState);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nextState = PlayerStates.skill1;
                m_Event.Invoke(nextState);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nextState = PlayerStates.skill2;
                m_Event.Invoke(nextState);
            }
        }


    }

}

