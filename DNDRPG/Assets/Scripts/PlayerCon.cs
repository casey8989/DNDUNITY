using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerCon : MonoBehaviour
{
    public LayerMask movemask;
    public Interact focus;

    Camera cam;
    PlayerMotor motor;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,100,movemask))
            {
                motor.MoveToPoint(hit.point);

                //Move player to what hit
                removeFocus();

                //stop focus
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
               Interact interact = hit.collider.GetComponent<Interact>();
                if(interact != null)
                {
                    SetFocus(interact);
                }
            }

        }
    }

    private void removeFocus()
    {
        if (focus != null)
        {
            focus.onDefocused();
        }
        
        focus = null;
        motor.StopFollowingTarget();
    }

    private void SetFocus(Interact newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.onDefocused();
            }   
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.onFocused(transform);
        
    }
}
