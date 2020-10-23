using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour, IWeapon
{
    [HideInInspector] public static Queue<GameObject> rocketQueue = new Queue<GameObject>();

    [SerializeField] private float numberOfRocketsStored;
    [SerializeField] private float cooldownToShoot;
    [SerializeField] private GameObject rocketPrefab;
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
            //prefab.GetComponent<Rocket>().rocketLauncher = this;
            prefab.SetActive(false);
            rocketQueue.Enqueue(prefab);        
        }    
    }
    public void Shoot()
    {
        if (cachedTime > Time.time) return;
        cachedTime = Time.time + cooldownToShoot;
        

        rocketToShoot = rocketQueue.Dequeue();
        rocket = rocketToShoot.GetComponent<Rocket>();

        rocket.SetPositionAndRotation(transform.position + transform.forward, transform.rotation);
        rocketToShoot.gameObject.SetActive(true);
        //rocket.MoveProjectile();

    }
    public static void DeActivateRocket(GameObject rocket)
    {
        rocketQueue.Enqueue(rocket);
        rocket.SetActive(false);   
        
    }


}
