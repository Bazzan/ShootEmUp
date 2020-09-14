using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziGun : MonoBehaviour, IWeapon
{
    public float Damage;
    public float Range;
    public float SecondsToShoot;
    public float spreadDegrees;
    public LayerMask layerMask;
    public LineRenderer lineRenderer;
    public float spreadAngel;



    private bool isShooting;
    private float coolDownTimer;

    private float randomSpreadDirection;
    public Vector3 shotDirection;

    private Quaternion spreadQuaternion;

    private RaycastHit rayHit;
    private Vector3[] lineRendererPositions = new Vector3[2];
    private Vector3 lineRenderhitPos;
    private Transform playerTransform;
    

    private void Awake()
    {
        playerTransform = GameManager.instance.PlayerTransform;
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
        randomSpreadDirection = Random.Range(-spreadAngel, spreadAngel);
        spreadQuaternion = transform.rotation;
        shotDirection = transform.forward;
        shotDirection.x += randomSpreadDirection;
        shotDirection.z += randomSpreadDirection;
        shotDirection = shotDirection.normalized;

        if (Physics.Raycast(playerTransform.position, shotDirection, out rayHit, Range, layerMask, QueryTriggerInteraction.Ignore))
        {
            lineRenderhitPos = rayHit.point;
            if (rayHit.collider.TryGetComponent<IKillabel>(out IKillabel enemyAttribute))
            {
                enemyAttribute.TakeDamage(Damage);
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shotDirection * 50);
    }
}
