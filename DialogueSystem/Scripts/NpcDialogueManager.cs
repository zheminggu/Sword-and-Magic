using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Adam.DialogueSystem.Event;
using UnityEngine.Events;

namespace Adam.DialogueSystem
{
    /// <summary>
    /// compared name is its parents name instead of its name
    /// </summary>
    public class NpcDialogueManager : MonoBehaviour
    {

        public bool m_HaveDialogueWithPlayer;
        public MissionState m_CurrentMissionState=MissionState.NoneMission;       
        public UnityAction<string,bool> m_DialogueAction;
        public UnityAction<string, string> m_MissionAction;
        public UnityAction<string,MissionState> m_FinishedMissionAction;

        private Animator m_animator;
        private DatabaseManager databaseManager;
        public UnityEvent OnTalkStart;
        public UnityEvent OnTalkEnd;
        private GameObject player;

        private void OnEnable()
        {
            DialogueManager.GetNPCs().Add(gameObject);
            m_DialogueAction += ChangeDialogueState;
            m_MissionAction += DialogueHandler;
            m_FinishedMissionAction += FinishedTask;

            //m_MissionAction += TestingEvent;
            DialogueEventManager.m_DialogueEvent.AddListener(m_DialogueAction);
            DialogueEventManager.m_MissionEvent.AddListener(m_MissionAction);
            DialogueEventManager.m_ChangeNpcMissionStateEvent.AddListener(m_FinishedMissionAction);
        }
        private void OnDisable()
        {
          
            foreach (var item in DialogueManager.GetNPCs())
            {
     
                if (item.transform.parent.name.Equals(gameObject.transform.parent.name))
                {
                    DialogueManager.GetNPCs().Remove(item);
                    //Debug.Log("delete NPC: " + gameObject.transform.parent.name);
                    break;
                }
            }
            DialogueEventManager.m_DialogueEvent.RemoveListener(m_DialogueAction);
            DialogueEventManager.m_MissionEvent.RemoveListener(m_MissionAction);
            DialogueEventManager.m_ChangeNpcMissionStateEvent.RemoveListener(m_FinishedMissionAction);

            //testing
            //TestingNPCList();


        }

      
        #region Debugging
        private void TestingNPCList()
        {
            //testing
           
            string names="";
            foreach (var item in DialogueManager.GetNPCs())
            {
               names+=item.transform.parent.name+"  ";
            }
            Debug.Log("current NPCs are:" +names);
        }

        private void TestingEvent(string _name,MissionState missionState)
        {
            Debug.Log("Called Npc:" + gameObject.transform.parent.name);
            if (_name.Equals(gameObject.transform.parent.name))
            {
                Debug.Log("Noticed"+ gameObject.transform.parent.name);
            }
            
        }
        #endregion

        private void Update()
        {
            ChangeMiniMapUI(m_CurrentMissionState);

            if (databaseManager==null)
            {
                databaseManager = GetComponent<DatabaseManager>();
            }
           
            if (databaseManager.m_CurrentDialogueData!=null)
            {
                if (databaseManager.m_CurrentDialogueData.IsAMission)
                {
                    ChangeCurrentMissionState(MissionState.HasMission);
                }
                else
                {
                    if (m_CurrentMissionState!=MissionState.FinishedMission)
                    {
                        ChangeCurrentMissionState(MissionState.NoneMission);
                    }
                }
            }
        }

        /// <summary>
        /// if npc is talking or not
        /// </summary>
        /// <param name="_name">npc name</param>
        /// <param name="_state">npc state</param>
        public void ChangeDialogueState(string _name,bool _state)
        {
            if (_name.Equals(gameObject.transform.parent.name))
            {
                //animator 
                m_animator = GetComponentInParent<Animator>();

                if (_state)//start a dialogue
                {
                    StartCoroutine(Rotate());
                    //call animators
                    m_animator.SetBool("NPCtalk", true);
                    OnTalkStart.Invoke();

                    databaseManager = GetComponent<DatabaseManager>();

                    if (m_CurrentMissionState!=MissionState.FinishedMission)
                    {
                        DialogueManager.Instance.GetUIController().m_CurrentDialogueData = databaseManager.GetDialogueData();
                    }
                    else
                    {
                        foreach (var item in DialogueManager.Instance.GetUIController().m_MissionList)
                        {
                            if (item.GetNpcName().Equals(gameObject.transform.parent.name))
                            {
                                DialogueManager.Instance.GetUIController().m_CurrentDialogueData = databaseManager.GetDialogueData(item.m_MissionName);
                                break;
                            }
                        }
                       
                    }
                }
                else
                {
                    //call animator
                    m_animator.SetBool("NPCtalk", false);
                    OnTalkEnd.Invoke();
                }
               
            }
          
        } 

        public void DialogueHandler(string _name,string _dialogueName)
        {
            if (_name.Equals(gameObject.transform.parent.name))
            {
                m_HaveDialogueWithPlayer = true;
                if (databaseManager == null)
                {
                    databaseManager = GetComponent<DatabaseManager>();
                }
                databaseManager.SetDialogueData(_dialogueName);
              
            }
          
        }      

        public void FinishedTask(string _name,MissionState _missionState)
        {
            if (_name.Equals(gameObject.transform.parent.name))
            {
                m_CurrentMissionState = _missionState;
                ChangeMiniMapUI(_missionState);
            }
        }

        private void ChangeCurrentMissionState(MissionState _missionState)
        {
            m_CurrentMissionState = _missionState;
        }

        private void ChangeMiniMapUI(MissionState _missionState)
        {
            //Debug.Log("Changing Mini Map UI to state" + _missionState);
            switch (_missionState)
            {
                case MissionState.HasMission:
                    GetComponent<bl_MiniMapItem>().SetIcon(DialogueManager.Instance.GetUIController().m_HaveMissionIcon);
                    break;
                case MissionState.DuringMisson:
                    GetComponent<bl_MiniMapItem>().SetIcon(DialogueManager.Instance.GetUIController().m_NullIcon);
                    break;
                case MissionState.FinishedMission:
                    GetComponent<bl_MiniMapItem>().SetIcon(DialogueManager.Instance.GetUIController().m_FinishedMissionIcon);
                    break;
                case MissionState.NoneMission:
                    GetComponent<bl_MiniMapItem>().SetIcon(DialogueManager.Instance.GetUIController().m_NullIcon);
                    break;
                default:
                    GetComponent<bl_MiniMapItem>().SetIcon(DialogueManager.Instance.GetUIController().m_NullIcon);
                    break;
            }
        }
        //public void LookAtPlayer()
        //{
        //    player = Camera.main.gameObject;
        //    Vector3 forward = player.transform.forward;
        //    forward.y = transform.forward.y;
        //    transform.forward = -forward;
        //}
        IEnumerator Rotate()
        {
            Debug.Log("Doing Rotate");
            player = Camera.main.gameObject;
            Vector3 forward = player.transform.forward;
            forward.y = transform.parent.forward.y;
            for (int i = 0; i < 100; i++)
            {
                yield return null;
                transform.parent.forward = Vector3.Lerp(transform.parent.forward, -forward, 10 * Time.deltaTime);
            }
            transform.parent.forward = -forward;

        }

    }

}
