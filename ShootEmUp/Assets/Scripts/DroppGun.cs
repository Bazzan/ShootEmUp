using UnityEngine;

public class DroppGun : MonoBehaviour
{


    public GameObject WeaponPackPrefab;
    public float procentToDroppGun;
    
    private float randomProcentResult;


    private void OnDisable()
    {
        randomProcentResult = Random.Range(0, 100);
        
        if(randomProcentResult < procentToDroppGun)
        {
            Instantiate(WeaponPackPrefab, transform.position, Quaternion.identity);
        }
    
    
    }


}
