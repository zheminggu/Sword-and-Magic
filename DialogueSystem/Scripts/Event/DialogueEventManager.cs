using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Adam.DialogueSystem.Event;


namespace Adam.DialogueSystem
{
    /// <summary>
    /// Npc dialogue EventManager
    /// </summary>
    public class DialogueEventManager : MonoBehaviour
    {

       
        /// <summary>
        ///   Get suitable Npc Dialogue 
        /// </summary>
        /// <remarks>
        /// player can talk to NPC once per person, call this to get suitable Dialogue
        /// </remarks>
        public static DialogueEvent m_DialogueEvent=new DialogueEvent();
        /// <summary>
        /// Set Npc Dialogue State and Talk State
        /// </summary>
        public static MissionEvent m_MissionEvent=new MissionEvent();
        public static MissionFinishedEvent m_ChangeNpcMissionStateEvent=new MissionFinishedEvent();
  

        [HideInInspector]
        public bool m_DuringTalk;
        private bool haveNpcToTalk;
        
        private string currentNpcName;
        private float DistanceBetweenPlayerAndNPC;
        private NpcDialogueManager currentNpcState;
        private void Update()
        {
            if (GameManager.gameState!=HawkQ.GameState.Gaming)
            {
                DialogueManager.Instance.GetUIController().ChangeDialogueTipsState(false);
                return;
            }
            OnUpdate();
       
        }

        private void OnUpdate()
        {
            haveNpcToTalk = false;
           
            foreach (var item in DialogueManager.GetNPCs())
            {
                if (GetSqrDistanceToPlayer(item)< DialogueManager.Instance.GetUIController().m_TriggerDistance*DialogueManager.Instance.GetUIController().m_TriggerDistance)
                {
                    //Debug.Log("in");
                    currentNpcState = item.GetComponent<NpcDialogueManager>();
                    if (currentNpcState.m_HaveDialogueWithPlayer)
                    {
                      
                        currentNpcName = item.transform.parent.name;
                        haveNpcToTalk = true;
                        break;
                    }
                                        
                }
            }
            if (haveNpcToTalk&&!m_DuringTalk)
            {
                DialogueManager.Instance.GetUIController().ChangeDialogueTipsState(true);
            }
            else
            {
                DialogueManager.Instance.GetUIController().ChangeDialogueTipsState(false);

            }
        }

        public float GetSqrDistanceToPlayer(GameObject _item)
        {
            return (_item.transform.position - GameManager.Instance.player.transform.position).sqrMagnitude;
        }


        public bool HaveNpcToTalk()
        {
            return haveNpcToTalk;
        }

        public NpcDialogueManager GetCurrentNpcState()
        {
            return currentNpcState;
        }

        public string GetCurrentNpcName()
        {
            return currentNpcName;
        }
    }

}
