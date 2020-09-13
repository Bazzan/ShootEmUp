
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour, IKillabel
{
    public string PoolTag;



    private float health;

    private Material material;


    private void Awake()
    {
        material = GetComponent<Material>();
    }
    private void OnEnable()
    {
        health = GameManager.instance.CurrentZombieHealth;
    }


    public void Die()
    {
        ObjectPool.Instance.EnQueueInPool(PoolTag, gameObject);


    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();


        }



    }

    IEnumerator HitVFX()
    {
        Material oldMaterial = material;
        material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        material.color = oldMaterial.color;

    }



}
