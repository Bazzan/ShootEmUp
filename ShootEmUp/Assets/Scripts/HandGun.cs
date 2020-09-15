using System.Collections;
using UnityEngine;
public class HandGun : MonoBehaviour, IWeapon
{

    public float Damage;
    public float Range;
    public float SecondsToShoot;
    public LayerMask layerMask;
    public LineRenderer lineRenderer;



    private float coolDownTimer;
    private Transform playerTransform;
    private RaycastHit rayHit;
    private Vector3[] lineRendererPositions = new Vector3[2];
    private Vector3 lineRenderhitPos;
    private Ray ray;
    // TODO : gör så att hand gun är nice och skjuter rakt
    private Transform weaponTransform;
    private void Awake()
    {
        playerTransform = GameManager.instance.PlayerTransform;
        coolDownTimer = 0f;
        weaponTransform = transform;
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

        ray.origin = weaponTransform.position;
        ray.direction = weaponTransform.forward;

        if (Physics.Raycast(ray, out rayHit, Range, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("hit");
            lineRenderhitPos = rayHit.point;
            if (rayHit.collider.TryGetComponent<IKillabel>(out IKillabel enemyAttribute))
            {

                enemyAttribute.TakeDamage(Damage);
            }
        }
        else
        {
            lineRenderhitPos = ray.GetPoint(Range);

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
