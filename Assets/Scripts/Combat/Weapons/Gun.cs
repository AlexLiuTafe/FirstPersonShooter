using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public int maxReserve = 300, maxClip = 30;
    public float spread = 2f, recoil = 1f;
    public Transform shotOrigin;
    public GameObject projecilePrefab;

    private int currentReserve = 0, currentClip = 0;


    // Start is called before the first frame update
    void Start()
    {
        Reload();
    }

    public void Reload()
    {
        //*NOTE
        // if(currentReserve >0)
        //   If(curClip < maxClip)
        //      int difference = maxClip - currentClip
        //      curClip += difference
        //      curReserve -= difference
        //          if(currentReserve < currentClip)
        //              if(difference <= curReserve)
        //              currentClip += difference
        //              currentReserve -= difference
        //          if(difference > currentReserve)
        //              currentClip += currentReserve
        //              currentReserve = 0
        // If there is ammo in reserve
        if (currentReserve >0)
        {
     
                // If reserve is greater than max clip
                if (currentReserve>=maxClip)
                {
                    //Remove diffrence from current reserve
                    int difference = maxClip - currentClip;
                    currentReserve -= difference;
                    //Replenish entire clip with max clip
                    currentClip = maxClip;
                }
                //if reserve < max clip
                if(currentReserve<maxClip)
                {
                    //Set entire clip to reserve
                    currentClip += currentReserve;
                    currentReserve -= currentReserve;
                }

        }
    }

   
    public override void Attack()
    {
        //Attack logic
        //Reduce the clip
        currentClip--; //currentClip = current clip -1/currentClip -=1
        //Get origin + direction for bullet
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;
        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;
        //Spawn Bullet
        GameObject clone = Instantiate(projecilePrefab, camTransform.position, camTransform.rotation);
        Projectile projectile = clone.GetComponent<Projectile>();
        projectile.Fire(lineOrigin, direction);
        // Reset ability to attack
        base.Attack();
    }
    
}
