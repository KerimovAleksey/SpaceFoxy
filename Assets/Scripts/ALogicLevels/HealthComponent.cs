using UnityEngine;

namespace LogicLevels
{
    public class HealthComponent : MonoBehaviour
    {
        public virtual void GetDamage()
        {
            Destroy(gameObject);
        }
    }
}
