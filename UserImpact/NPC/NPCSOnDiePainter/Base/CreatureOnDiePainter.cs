using UnityEngine;

using DrawShooter.Core.Gameplay;

namespace DrawShooter.Core.UserImpact
{
    public class CreatureOnDiePainter : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Color m_colorOfDiedCreature;
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            ObserveForNPCSDeath();
        }
        #endregion

        #region CreatureOnDiePainter
        private void ObserveForNPCSDeath() 
        {
            foreach (var creature in FindObjectsOfType<Creature>()) 
            {
                creature.OnDie += () => ChangeColorOfCreature(creature.gameObject);
            }
        }

        private void ChangeColorOfCreature(GameObject creatureGameObject) 
        {
            if (creatureGameObject.GetComponentsInChildren<SkinnedMeshRenderer>().Length == 0)
                return;

            foreach (var materialOfCreature in creatureGameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials)
            {
                materialOfCreature.shader = Shader.Find("Standard");

                materialOfCreature.mainTexture = null;

                materialOfCreature.color = m_colorOfDiedCreature;
            }
        }
        #endregion
    }
}