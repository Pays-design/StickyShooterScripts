using System;
using System.Collections.Generic;
using UnityEngine;

using DrawShooter.Core.UnityExtensions;

namespace DrawShooter.Core.Gameplay
{
    public class NPCGroupDeathObserver : ClassicNotCreatableMonoBehaviourSingletone<NPCGroupDeathObserver>
    {
        #region SerializeFields
        [SerializeField] private List<NPCGroup> m_npcNetwork;
        #endregion

        #region Fields
        private IEnumerator<NPCGroup> m_observeChain;
        private uint m_countOfDeadNpcsInCurrentGroup;

        public Action<NPCGroup> OnGroupDeath;
        public Action OnNetworkDeath;
        #endregion

        #region NPCGroupChangeObserver
        private void TryChangeObservableGroup() 
        {
            m_countOfDeadNpcsInCurrentGroup += 1;

            if (m_countOfDeadNpcsInCurrentGroup == m_observeChain.Current.Npcs.Length)
            {
                ChangeGroupToObserve();
            }
        }

        private void ChangeGroupToObserve() 
        {
            if(m_observeChain.MoveNext())
            {
                m_countOfDeadNpcsInCurrentGroup = 0;

                NPCGroup currentGroup = m_observeChain.Current;

                foreach (var npc in currentGroup.Npcs)
                {
                    npc.OnDie += TryChangeObservableGroup;
                }

                OnGroupDeath?.Invoke(currentGroup);
            } 
            else
            {
                OnNetworkDeath?.Invoke();
            }
        }

        public void StartObserving()
        {
            m_observeChain = m_npcNetwork.GetEnumerator();

            ChangeGroupToObserve();
        }
        #endregion
    }
}