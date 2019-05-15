using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Weapon currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWeapon && currentWeapon.canAttack)
        {
            //if fire button is pressed
            if(Input.GetButton("Fire1"))
            {
                //Attack
                currentWeapon.Attack();
            }
        }
    }
}
