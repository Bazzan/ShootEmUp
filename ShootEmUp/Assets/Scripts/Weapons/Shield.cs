using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ShieldDuration;
    public float ShieldCooldown;
    public float PlayerSizeMultiplier;
    public Material ShieldMaterial;

    private Material playerMaterial;
    private float timeOnEnable;

    private PlayerAttribute playerAttribute;
    private GameObject ShieldMesh;
    private Transform playerTransform;
    private GameObject MeshGO;
    private Vector3 normalScale;
    private MeshRenderer meshRenderer;
    private CapsuleCollider collider;
    private float initialColliderRaidus;


    private void Awake()
    {
        playerAttribute = GetComponentInParent<PlayerAttribute>();
        ShieldMesh = transform.GetChild(0).gameObject;
        playerTransform = GameManager.instance.PlayerTransform;
        normalScale = playerTransform.localScale;
        meshRenderer = playerTransform.GetComponentInChildren<MeshRenderer>();
        collider = playerTransform.GetComponent<CapsuleCollider>();
        initialColliderRaidus = collider.radius;
        playerMaterial = meshRenderer.material;
    }





    public IEnumerator ActivateShield()
    {
        if (Time.time < timeOnEnable) yield break;
        timeOnEnable = ShieldCooldown + Time.time;

        meshRenderer.transform.localScale *= PlayerSizeMultiplier;
        collider.radius = meshRenderer.transform.localScale.x / 2f;
        meshRenderer.sharedMaterial = ShieldMaterial;
        //collider.radius +
        playerAttribute.IsShieldActive = true;

        yield return new WaitForSeconds(ShieldDuration);

        meshRenderer.transform.localScale = normalScale;
        collider.radius = initialColliderRaidus;
        meshRenderer.sharedMaterial = playerMaterial;
        playerAttribute.IsShieldActive = false;



    }






}
