
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyAttribute : MonoBehaviour, IKillabel
{
    public string PoolTag;
    public Material hitMaterial;
    public Material zombieHitTexture;
    public ParticleSystem HitParticelVFX;
    public float timeToDestryParticles;

    private Material material;
    private EnemyMovement enemyMovement;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshRenderer meshRenderer;
    private float health;
    private ParticleSystem spawnedParticle;

    private void Awake()
    {

        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        material = skinnedMeshRenderer.sharedMaterial;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    private void OnEnable()
    {
        health = GameManager.instance.CurrentZombieHealth;
        //meshRenderer.sharedMaterial = material;
    }

    public void SpawnAndDestroyHitParticels(RaycastHit rayHit)
    {
        spawnedParticle = Instantiate(HitParticelVFX, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        Destroy(spawnedParticle.gameObject, timeToDestryParticles);
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
        skinnedMeshRenderer.sharedMaterial = zombieHitTexture;

        //meshRenderer.sharedMaterial = hitMaterial;

        yield return new WaitForSeconds(0.2f);


        skinnedMeshRenderer.sharedMaterial = material;
    }

}
