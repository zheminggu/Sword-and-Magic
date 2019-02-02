using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QIPro;

namespace Adam.DialogueSystem
{
    public class ItemInfomation
    {

        public int m_Number;
        [Tooltip("choose to use Prefab or Name to define item")]
        public bool m_UsePrefab = true;

        [ShowIf("m_UsePrefab")]
        public Item m_Item;


        [HideIf("m_UsePrefab")]
        public string m_ItemName=string.Empty;
        /// <summary>
        /// used when judging if Mission complete
        /// </summary>
        private int obtainedNumber=0;

        #region Obtained Number Method
        public int GetObtainedNumber()
        {
            return obtainedNumber;
        }

        public void AddObtainedNumber()
        {
           obtainedNumber++;
        }

        public void AddObtainedNumber(int _number)
        {
            obtainedNumber += _number;
        }

        public void AddObtainedNumberWithNameJudge(string _name,int _number)
        {
            if (m_UsePrefab)
            {
                if (m_Item.itemData.itemName.Equals(_name))
                {
                    obtainedNumber += _number;
                }
              
            }
            else
            {
                if (m_ItemName.Equals(_name))
                {
                    obtainedNumber += _number;
                }
            }
        }

        public void AddObtainedNumberWithNameJudge(string _name)
        {
            if (m_UsePrefab)
            {
                if (m_Item.itemData.itemName.Equals(_name))
                {
                    obtainedNumber ++;
                }
            }
            else
            {
                if (m_ItemName.Equals(_name))
                {
                    obtainedNumber ++;
                }
            }
        }

        public void MinusObtainedNumber()
        {
            obtainedNumber--;
        }

        public void MinusObtainedNumber(int _number)
        {
            obtainedNumber -= _number;
        }

        public void MinusObtainedNumberWithNameJudge(string _name, int _number)
        {
            if (m_UsePrefab)
            {
                if (m_Item.itemData.itemName.Equals(_name))
                {
                    obtainedNumber -= _number;
                }
            }
            else
            {
                if (m_ItemName.Equals(_name))
                {
                    obtainedNumber -= _number;
                }
            }
        }

        public void MinusObtainedNumberWithNameJudge(string _name)
        {
            if (m_UsePrefab)
            {
                if (m_Item.itemData.itemName.Equals(_name))
                {
                    obtainedNumber--;
                }
            }
            else
            {
                if (m_ItemName.Equals(_name))
                {
                    obtainedNumber--;
                }
            }
        }

        public bool ObtainedEnough()
        {
            if (obtainedNumber>=m_Number)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }
        #endregion
    }

}
