using System.Collections;
using UnityEngine;
public class HandGun : MonoBehaviour, IWeapon
{

    public float Damage;
    public float Range;
    public float SecondsToShoot;
    public float spreadDegrees;
    public LayerMask layerMask;
    public LineRenderer lineRenderer;


    private bool isShooting;
    private float coolDownTimer;
    private float lineRendererTimer;
    private Transform playerTransform;
    private Vector3 spread;
    private RaycastHit rayHit;
    private Vector3[] lineRendererPositions = new Vector3[2];
    private PlayerInputManager inputManager;
    private ParticleSystem shootVFX;
    private Vector3 lineRenderhitPos;

    // TODO : gör så att hand gun är nice och skjuter rakt

    private void Start()
    {
        playerTransform = GameManager.instance.PlayerTransform;
        isShooting = false;
        shootVFX = GetComponentInChildren<ParticleSystem>();
        coolDownTimer = 0f;

    }


    private void OnEnable()
    {
        PlayerInputManager.shootDelegate = Shoot;
    }


    private void OnDisable()
    {
        PlayerInputManager.shootDelegate -= Shoot;
    }




    public void Shoot()
    {
        if (Time.time < coolDownTimer) return;


        //spread.y = Random.Range(-spreadDegrees, spreadDegrees);

        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out rayHit, Range, layerMask, QueryTriggerInteraction.Ignore))
        {
            lineRenderhitPos = rayHit.point;
            rayHit.collider.GetComponent<EnemyAttribute>().TakeDamage(Damage);
        }

        LineRendererEffect(lineRenderhitPos);
        coolDownTimer = Time.time + SecondsToShoot;


    }

    private void LineRendererEffect(Vector3 hitPosition)
    {
        lineRendererPositions[0] = playerTransform.position;
        lineRendererPositions[1] = hitPosition;
        lineRenderer.SetPositions(lineRendererPositions);

        StartCoroutine(Line());
    }

    IEnumerator Line()
    {
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.1f);

        lineRenderer.enabled = false;
    }


}
