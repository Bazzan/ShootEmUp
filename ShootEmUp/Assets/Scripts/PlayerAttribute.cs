using UnityEngine;

public class PlayerAttribute : MonoBehaviour, IKillabel
{
    public float Health;



    public void Die()
    {
        gameObject.SetActive(false);    


    }

    public void TakeDamage(float damage)
    {
        Health -= damage;

        if(Health <= 0f)
        {
            Die();
        }

    }


}
