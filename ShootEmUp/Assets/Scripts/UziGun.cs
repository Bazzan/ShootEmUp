using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziGun : MonoBehaviour, IWeapon
{
    public float Damage;
    public float Range;
    public float SecondsToShoot;
    public LayerMask LayerMask;
    public LineRenderer LineRenderer;
    public float SpreadAmount;


    private float coolDownTimer;
    private Vector3 shotDirection;
    private RaycastHit rayHit;
    private Vector3[] lineRendererPositions = new Vector3[2];
    private Vector3 lineRenderhitPos;
    private Transform playerTransform;
    Ray ray;

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


        ray.origin = transform.position;
        ray.direction = GetSpreadDirection();

        //shotDirection = GetSpreadDirection();
        //if (Physics.Raycast(playerTransform.position, shotDirection, out rayHit, Range, layerMask, QueryTriggerInteraction.Ignore))
        if (Physics.Raycast(ray, out rayHit, Range, LayerMask, QueryTriggerInteraction.Ignore))
        {
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

    private Vector3 GetSpreadDirection()
    {

        float Spread = Random.Range(-SpreadAmount, SpreadAmount);

        Vector3 dir = transform.forward;
        dir.x += Spread;
        dir.z += Spread;
        return dir;
    }



    private void LineRendererEffect(Vector3 hitPosition)
    {
        lineRendererPositions[0] = playerTransform.position;
        lineRendererPositions[1] = hitPosition;
        LineRenderer.SetPositions(lineRendererPositions);

        StartCoroutine(Line());
    }

    IEnumerator Line()
    {
        LineRenderer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        LineRenderer.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shotDirection * Range);
    }
}
