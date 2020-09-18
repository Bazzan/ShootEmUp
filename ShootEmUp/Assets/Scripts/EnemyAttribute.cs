
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyAttribute : MonoBehaviour, IKillabel
{
    public string PoolTag;
    public Material hitMaterial;
    public Material zombieTexture;
    private Material material;
    
    private EnemyMovement enemyMovement;
    private SkinnedMeshRenderer meshRenderer;

    private float health;


    private void Awake()
    {

        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        material = meshRenderer.sharedMaterial;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void OnEnable()
    {
        health = GameManager.instance.CurrentZombieHealth;
        //meshRenderer.sharedMaterial = material;
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
            return;
        }

        enemyMovement.StaggerEnemy();
        StartCoroutine(HitVFX());
    }

    IEnumerator HitVFX()
    {
        meshRenderer.sharedMaterial = zombieTexture;

        //meshRenderer.sharedMaterial = hitMaterial;

        yield return new WaitForSeconds(0.2f);


        meshRenderer.sharedMaterial = material;
    }


}
