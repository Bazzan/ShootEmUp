using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IWeapon
{
    [SerializeField] private float numberOfRocketsStored;
    [SerializeField] private float cooldownToShoot;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField]  private Queue<GameObject> rocketQueue = new Queue<GameObject>();
    private GameObject rocketToShoot;
    private Rocket rocket;
    private float cachedTime;

    private void OnEnable()
    {
        PlayerInputManager.shootDelegate = Shoot;
    }
    private void OnDisable()
    {
        PlayerInputManager.shootDelegate -= Shoot;
    }



    private void Start()
    {
        cachedTime = Time.time;

        for (int i = 0; i < numberOfRocketsStored; i++)
        {
            GameObject prefab = Instantiate(rocketPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<Rocket>().rocketLauncher = this;
            prefab.SetActive(false);
            rocketQueue.Enqueue(prefab);        
        }    
    }


    public void Shoot()
    {
        if (cachedTime > Time.time) return;
        cachedTime = Time.time + cooldownToShoot;
        

        rocketToShoot = rocketQueue.Dequeue();
        rocketToShoot.gameObject.SetActive(true);
        rocket = rocketToShoot.GetComponent<Rocket>();

        rocket.SetPositionAndRotation(transform.position + transform.forward, transform.rotation);
        //rocket.MoveProjectile();

    }


    public void DeActivateRocket(GameObject rocket)
    {
        rocketQueue.Enqueue(rocket);
        rocket.SetActive(false);   
        
    }


}
