using UnityEngine;

public class PlayerAttribute : MonoBehaviour, IKillabel
{
    public float Health;
    public bool IsShieldActive = false;


    public void Die()
    {
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
