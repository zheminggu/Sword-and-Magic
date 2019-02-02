using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QIPro;
using Sirenix.OdinInspector;

namespace Adam.DialogueSystem
{
    public class AdamUIController : MonoBehaviour
    {
        [FoldoutGroup("Dialogue")]
        public GameObject m_DialogueMainBody;
        [FoldoutGroup("Dialogue")]
        public GameObject m_DialogueBranch;
        [FoldoutGroup("Dialogue")]
        public Text m_DialogueText;
        public Text m_DialoguePersonName;
     
        [FoldoutGroup("Tips")]
        public GameObject m_Tips;
        [FoldoutGroup("Tips")]
        public float m_TriggerDistance = 1f;

   

        
        [FoldoutGroup("Task Bar Setting")]
        public GameObject m_TaskBar;
        [FoldoutGroup("Task Bar Setting")]
        public GameObject m_RightWindow;
        [FoldoutGroup("Task Bar Setting")]
        public GameObject m_FinishMissionGiveObject;

     
        [FoldoutGroup("NPC Images")]
        public Sprite m_HaveMissionIcon;
        [FoldoutGroup("NPC Images")]
        public Sprite m_FinishedMissionIcon;
        [FoldoutGroup("NPC Images")]
        public Sprite m_NullIcon;
        
        [Space]
        [ShowInInspector]
        public List<MissionInformation> m_MissionList = new List<MissionInformation>();

        [InfoBox("Debug only")]
        public DialogueData m_CurrentDialogueData;
 


        private void Start()
        {
            InitializeMissionList();
        
        }

        /// <summary>
        /// initialized mission list with stored data
        /// </summary>
        private void InitializeMissionList()
        {

        }
       

        public void ChangeDialogueTipsState(bool _state)
        {
            m_Tips.SetActive(_state);
        }


        #region Dialogue UI 
        public void ChangeDialogueMainBodyState(bool _state)
        {
            m_DialogueMainBody.SetActive(_state);
        }

        public void ChangeDialogueBranchState(bool _state)
        {
            m_DialogueBranch.SetActive(_state);
        }

        public void OpenDialogueMainBody()
        {
            Debug.Log("called method");
            if (DialogueManager.Instance.GetDialogueEventManager().HaveNpcToTalk())
            {
                Debug.Log("Have Dialogue");
                m_DialogueMainBody.GetComponent<BaseWindow>().OpenWindow();
                CloseBranch();
                DialogueManager.Instance.GetDialogueEventManager().m_DuringTalk = true;
                DialogueEventManager.m_DialogueEvent.Invoke(DialogueManager.Instance.GetDialogueEventManager().GetCurrentNpcName(),true);
                ChangeDialogueContent();
            }
        }

        public void CloseDialogueMainBody()
        {
            m_DialogueMainBody.GetComponent<BaseWindow>().CloseWindow();
            DialogueManager.Instance.GetDialogueEventManager().m_DuringTalk = false;
            DialogueEventManager.m_DialogueEvent.Invoke(DialogueManager.Instance.GetDialogueEventManager().GetCurrentNpcName(), false);//change current NPC dialogue state 
        }

        public void ChangeDialogueContent()
        {
            if (m_CurrentDialogueData != null)
            {
                if (m_CurrentDialogueData.HaveNextDialogue())
                {
                    Debug.Log("changing dialogue");
                    m_DialogueText.text = m_CurrentDialogueData.GetNextDialogue();
                    m_DialoguePersonName.text = m_CurrentDialogueData.GetDialoguingPersonName();
                }
                else
                {
                    Debug.Log("finished talking");
                    DealSettingWhenTalkFinished();
                }
            }

        }

        private void DealSettingWhenTalkFinished()
        {
            if (m_CurrentDialogueData.IsAMission&& !m_CurrentDialogueData.m_Mission.m_FinishedMission)//current Dialogue is a mission and have not finished
            {
                ShowBranch();
                //waiting for choose
            }
            else if (m_CurrentDialogueData.IsAMission && m_CurrentDialogueData.m_Mission.m_FinishedMission)//finished mission
            {
                GiveReward();
                CloseDialogueMainBody();
            }
            else
            {
                DealWithSettings();
                CloseDialogueMainBody();
            }
        }

        private void GiveReward()
        {
        

            foreach (var _giveItem in m_CurrentDialogueData.m_Mission.m_Give)
            {
                if (_giveItem.m_UsePrefab)
                {
                    Debug.Log("obtained " + _giveItem.m_Item.itemData.itemName + " number is " + _giveItem.m_Number);
                    GameManager.Instance.player.GetComponent<InventoryParticipant>().inventoryParticipantInformation.AddItem(_giveItem.m_Item, _giveItem.m_Number);
                }
                else
                {
                    Debug.LogError("Giving Item must be a prefab");
                }
            }

            foreach (var _mission in m_MissionList)
            {
                if (_mission.m_MissionName.Equals(m_CurrentDialogueData.m_Mission.m_MissionName))
                {
                    m_MissionList.Remove(_mission);
                    break;
                }
            }
            DialogueEventManager.m_ChangeNpcMissionStateEvent.Invoke(m_CurrentDialogueData.m_Mission.GetNpcName(), Event.MissionState.NoneMission);
            DealWithSettings();
        }

        private void ShowBranch()
        {
            m_DialogueBranch.SetActive(true);
            m_DialogueText.gameObject.SetActive(false);
        }

        private void CloseBranch()
        {
            m_DialogueBranch.SetActive(false);
            m_DialogueText.gameObject.SetActive(true);
        }

        public void DealWithMission()
        {
            if (AddInformationToTaskBar())//succeeded in add Information to task bar
            {
                DealWithSettings();  
            }
            CloseDialogueMainBody();
        }

        /// <summary>
        /// Set Npc Dialogue After dialogue finished
        /// </summary>
        private void DealWithSettings()
        {
            if (m_CurrentDialogueData.m_SetNpcDialogue != null)
            {
                //announce NPC to change Dialogue
                for (int i = 0; i < m_CurrentDialogueData.m_SetNpcDialogue.Count; i++)
                {
                    DialogueEventManager.m_MissionEvent.Invoke(m_CurrentDialogueData.m_SetNpcDialogue[i].m_Name, m_CurrentDialogueData.m_SetNpcDialogue[i].m_DialogueName);
                }
            }
        }
        #endregion

        #region Task Bar UI

        public void InitializeTaskBar()
        {
            Debug.Log("Initializing Task Bar");
            for (int i = 0; i < m_TaskBar.transform.childCount; i++)
            {
                m_TaskBar.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = string.Empty;
            }
            for (int i = 0; i < m_MissionList.Count; i++)
            {
                m_TaskBar.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = m_MissionList[i].m_MissionName;
            }

            ChangeRightWindowContent("0");
        }

        /// <summary>
        /// Change right window content
        /// </summary>
        /// <param name="_order">number only</param>
        public void ChangeRightWindowContent(string _order)
        {
          
            int _index = int.Parse(_order);
            if (_index < m_MissionList.Count)
            {
                //change 任务名称
                m_RightWindow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "任务名称：" + m_MissionList[_index].m_MissionName;


                //change 详细描述
                Debug.Log("Changing right window content");
                Text _text = m_RightWindow.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                _text.text = "任务描述： \n";
                _text.text += m_MissionList[_index].m_DetailDiscription+"\n";

                _text.text += "\n任务需求：";
                foreach (var item in m_MissionList[_index].m_Need)
                {
                    _text.text += "\n" + item.m_ItemName+":  "+item.GetObtainedNumber().ToString() + "/" + item.m_Number.ToString();
                }

                //change完成奖励
                //initiate 完成奖励

                for (int i = 0; i < m_RightWindow.transform.GetChild(2).GetChild(2).childCount; i++)
                {
                    Destroy(m_RightWindow.transform.GetChild(2).GetChild(2).GetChild(i).gameObject);
                }

                foreach (var item in m_MissionList[_index].m_Give)
                {
                    GameObject _missionGiveItem = Instantiate(m_FinishMissionGiveObject, m_RightWindow.transform.GetChild(2).GetChild(2));
                    _missionGiveItem.transform.GetChild(1).GetComponent<Text>().text = item.m_Item.itemData.itemName;
                    _missionGiveItem.transform.GetChild(0).GetComponent<Image>().sprite = item.m_Item.itemData.icon;
                }
                m_RightWindow.transform.GetChild(2).gameObject.SetActive(true);

            }
            else
            {
                m_RightWindow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "任务名称：";

                m_RightWindow.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "任务描述： ";

                for (int i = 0; i < m_RightWindow.transform.GetChild(2).GetChild(2).childCount; i++)
                {
                    Destroy(m_RightWindow.transform.GetChild(2).GetChild(2).GetChild(i).gameObject);
                }
                m_RightWindow.transform.GetChild(2).gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// need a event to call this method to add number to mission "Need obtain item"
        /// </summary>
        /// <param name="_itemName">item name such as moster's name or items name</param>
        /// <param name="_number">obtained number </param>
        public void AddItem(string _itemName, int _number)
        {
            Debug.Log("Adding item: " + _itemName + "added Numbers is " + _number.ToString());
            foreach (var mission in m_MissionList)
            {
                foreach (var item in mission.m_Need)
                {
                    item.AddObtainedNumberWithNameJudge(_itemName, _number);
                }
            }
            JudgeIfMissionFinished();
        }

        public void MinusItem(string _itemName, int _number)
        {
            foreach (var mission in m_MissionList)
            {
                foreach (var item in mission.m_Need)
                {
                    item.MinusObtainedNumberWithNameJudge(_itemName, _number);
                }
            }
            JudgeIfMissionFinished();
        }

        /// <summary>
        /// Add information to task bar
        /// </summary>
        /// <returns>if succeeded in add task  </returns>
        private bool AddInformationToTaskBar()
        {
            foreach (var item in m_MissionList)
            {
                if (item.m_MissionName.Equals(m_CurrentDialogueData.m_Mission.m_MissionName))
                {
                    return false;
                }
            }
            if (m_MissionList.Count>10)
            {
                return false;
            }
            else
            {
                //add mission to task bar 
                m_CurrentDialogueData.m_Mission.SetMissionNpcName(DialogueManager.Instance.GetDialogueEventManager().GetCurrentNpcName());
                m_MissionList.Add(m_CurrentDialogueData.GetMission());
                //change task bar

                return true;
            }
           
        }

        private void JudgeIfMissionFinished()
        {
            foreach (var item in m_MissionList)
            {
                if (item.MissionComplete())
                {
                    Debug.Log("Mission " + item.m_MissionName + " finished");
                    item.m_FinishedMission = true;
                    DialogueEventManager.m_ChangeNpcMissionStateEvent.Invoke(item.GetNpcName(),Event.MissionState.FinishedMission);
                }
                else
                {
                    item.m_FinishedMission = false;
                    DialogueEventManager.m_ChangeNpcMissionStateEvent.Invoke(item.GetNpcName(), Event.MissionState.DuringMisson);
                }
            }
        }

       
        #endregion
    }

}
