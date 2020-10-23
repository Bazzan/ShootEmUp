using UnityEngine;

public class PlayerAttribute : MonoBehaviour, IDamage
{
    public float Health;
    public bool IsShieldActive = false;
    
    public void Die()
    {
        GameManager.instance.OnPausGame();
        gameObject.SetActive(false);    
    }
    public void TakeDamage(float damage)
    {
        if (IsShieldActive) return;

        Health -= damage;

        if(Health <= 0f)
        {
            Die();
        }
    }
}
