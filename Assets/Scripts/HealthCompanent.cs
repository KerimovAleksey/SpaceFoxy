using UnityEngine;
using UnityEngine.Events;

public class HealthCompanent : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    
    public event UnityAction<float> HealthChanged;
    public event UnityAction OnGetDamage;
    public event UnityAction OnHalfOfHealth;
    public event UnityAction OnShieldCollision;

	protected int _health;
	protected bool _isProtected = false;

    private bool _secondPhaseEnabled = false;

	protected virtual void OnEnable()
	{
		_health = _maxHealth;
	}

	public void GetDamage(float damageValue)
    {
        if (_isProtected == false)
        {
            _health -= (int) damageValue;
            HealthChanged?.Invoke(-damageValue);
            OnGetDamage?.Invoke();

            if (_secondPhaseEnabled == false && _health <= _maxHealth / 2) // Переход на 2 фазу при хп:
            {
                OnHalfOfHealth?.Invoke();
                _secondPhaseEnabled = true;
            }

            if (_health <= 0)
            {
                OnTheEnd();
            }
        }
        else
        {
            OnShieldCollision?.Invoke();
        }
    }

    protected virtual void OnTheEnd()
    {
        Destroy(gameObject);
    }
     
    public void GetHeal(int healValue)
    {
        _health += healValue;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            HealthChanged?.Invoke(healValue);
        }
	}

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public void ChangeProtectionStatus(bool state)
    {
        _isProtected = state;
    }
}
