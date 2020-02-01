﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    // Components
    PlayerMove m_moveComponent;
    PlayerInputActions m_inputActions;

    InteractiveObject nearObject;

    // Internal
    Vector2 m_moveDir;
    float interact;
    float dash;


    // Start is called before the first frame update
    void Start()
    {
        m_moveDir = Vector2.zero;
        m_moveComponent = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        m_moveComponent.Move(m_moveDir, dash);
    }


    /// Unity Input Events -----------------------------------
    // Move Action
    public void Move(InputAction.CallbackContext context)
    {
        m_moveDir = context.ReadValue<Vector2>();
    }

    // Interact Action
    public void Interact(InputAction.CallbackContext context)
    {
        interact = context.ReadValue<float>();
        Debug.Log(this.nearObject);
    }

    // Dash Action
    public void Dash(InputAction.CallbackContext context)
    {
        dash = context.ReadValue<float>();
    }


    /// Collision Events --------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 otherPos = collision.gameObject.transform.position;
            Vector3 collisionDir = this.transform.position - otherPos;
            m_moveComponent.Move(collisionDir, 1.0f);
        }
    }

    /// Trigger Events --------------------------------------
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "InteractiveObject")
        {
            this.nearObject = other.gameObject.GetComponent<InteractiveObject>();
        }
   }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "InteractiveObject")
        {
            this.nearObject = null;
        }
    }

}