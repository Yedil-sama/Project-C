using System.Collections;
using UnityEngine;

namespace Core.Cards.Spells
{
    public abstract class Spell : MonoBehaviour, ISpell
    {
        protected Player owner;

        public void Cast(Player owner, Vector3 targetPosition)
        {
            this.owner = owner;
            StartCoroutine(DoCast(targetPosition));
        }

        protected abstract IEnumerator DoCast(Vector3 targetPosition);
    }
}
