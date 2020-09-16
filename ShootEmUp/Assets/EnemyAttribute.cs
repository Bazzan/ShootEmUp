
using System.Collections;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour, IKillabel
{
    public string PoolTag /*= "Zombie"*/;
    public Material hitMaterial;

    private EnemyMovement enemyMovement;
    private MeshRenderer meshRenderer;
    private Material material;

    private float health;


    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.sharedMaterial;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void OnEnable()
    {
        health = GameManager.instance.CurrentZombieHealth;
        meshRenderer.sharedMaterial = material;
    }


    public void Die()
    {

        ObjectPool.Instance.EnQueueInPool("Zombie", gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
            return;
        }
        
        enemyMovement.StaggerEnemy();
        StartCoroutine(HitVFX());
    }

    IEnumerator HitVFX()
    {

        meshRenderer.sharedMaterial = hitMaterial;

        yield return new WaitForSeconds(0.2f);

        meshRenderer.sharedMaterial = material;
    }


}
