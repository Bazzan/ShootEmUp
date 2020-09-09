using System.Collections;
using System.Xml.Schema;
using UnityEngine;

public class HandGun : MonoBehaviour, IWeapon
{

    public float Damage;
    public float Range;
    public float spreadDegrees;
    public LayerMask layerMask;
    public LineRenderer lineRenderer;


    private Transform playerTransform;
    private Vector3 spread;
    private RaycastHit rayHit;
    private Vector3 [] lineRendererPositions = new Vector3 [2] ;



    // TODO : gör så att hand gun är nice och skjuter rakt

    private void Start()
    {
        playerTransform = GameManager.instance.PlayerTransform;
    }



    private void Update()
    {
        if (PlayerInputManager.inputActions.Player.Fire.triggered)
        {
            Shoot();
        }
    }
    public void Shoot()
    {

        spread.y = Random.Range(-spreadDegrees, spreadDegrees);
        
        if(Physics.Raycast(playerTransform.position,playerTransform.forward , out rayHit,Range,layerMask, QueryTriggerInteraction.Ignore))
        {
            lineRendererPositions[0] = playerTransform.position;
            lineRendererPositions[1] = rayHit.point;
            lineRenderer.SetPositions(lineRendererPositions);

            StartCoroutine(Line());
        }




    }

    IEnumerator Line()
    {
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.1f);

        lineRenderer.enabled = false;
    }



}
