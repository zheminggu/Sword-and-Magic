using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Adam.Game
{
    public class AnimationUility : MonoBehaviour
    {
        public Animator m_Animator;
        private MyUnityEvent.PlayerStates currentPlayerStates;
        private void Start()
        {
            currentPlayerStates = MyUnityEvent.PlayerStates.idle;
        }


  
        /// <summary>
        /// subscriber
        /// </summary>
        /// <param name="_playerstates">parameter where my Unity Event will pass the state to </param>
        public void OnStateChanged(MyUnityEvent.PlayerStates _playerstates)
        {
            //if the character dead or something else need to be dealt first add code here
            if (currentPlayerStates == _playerstates)
            {
                return;
            }
            //if the state needs to be changed
            switch (_playerstates)
            {
                case MyUnityEvent.PlayerStates.idle:
                    m_Animator.SetBool("attackReady", false);
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("skill1", false);
                    m_Animator.SetBool("skill2", false);
                    m_Animator.SetBool("sitDown", false);
                    break;
                case MyUnityEvent.PlayerStates.attackReady:
                    m_Animator.SetBool("attackReady", true);
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("skill1", false);
                    m_Animator.SetBool("skill2", false);
                    m_Animator.SetBool("sitDown", false);
                    break;
                case MyUnityEvent.PlayerStates.walk:
                    m_Animator.SetBool("attackReady", false);
                    m_Animator.SetBool("walk", true);
                    m_Animator.SetBool("skill1", false);
                    m_Animator.SetBool("skill2", false);
                    m_Animator.SetBool("sitDown", false);
                    break;
                case MyUnityEvent.PlayerStates.skill1:
                    m_Animator.SetBool("attackReady", true);
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("skill1", true);
                    m_Animator.SetBool("skill2", false);
                    m_Animator.SetBool("sitDown", false);
                    break;
                case MyUnityEvent.PlayerStates.skill2:
                    m_Animator.SetBool("attackReady", true);
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("skill1", false);
                    m_Animator.SetBool("skill2", true);
                    m_Animator.SetBool("sitDown", false);
                    break;
                case MyUnityEvent.PlayerStates.sitDown:
                    m_Animator.SetBool("attackReady", false);
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("skill1", false);
                    m_Animator.SetBool("skill2", false);
                    m_Animator.SetBool("sitDown", true);
                    break;
                default:
                    Debug.LogError("Can't choose an undefined state");
                    break;
            }
            currentPlayerStates = _playerstates;
        }


    }

}
