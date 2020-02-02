using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    // Components
    PlayerMove m_moveComponent;
    PlayerInputActions m_inputActions;
    public Camera playerCam;
    public Camera cameraPrefab;

    Rigidbody rb;

    public GameObject holdSpace;
    IInteractable nearInteractable;

    IInteractable inventory;

    // Internal
    Vector2 m_moveDir;
    public Vector2 lookDir;
    Vector3 cameraForward;
    bool isPushed;
    float interact;
    float dash;
    int dashTimer;


    void Start()
    {
        m_moveDir = Vector2.zero;
        m_moveComponent = GetComponent<PlayerMove>();
        isPushed = false;
        dashTimer = 0;
        Camera camera = Instantiate(cameraPrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 15, this.gameObject.transform.position.z - 75), Quaternion.identity);
        camera.GetComponent<CameraController>().target = this.gameObject;
        playerCam = camera;
        this.gameObject.GetComponent<PlayerInput>().camera = playerCam;
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {
        if (playerCam != null)
            cameraForward = playerCam.transform.forward;

        // Movement
        if (!isPushed && playerCam != null)
        {
            Vector2 moveVec = new Vector2((cameraForward.x * m_moveDir.y) + (cameraForward.z * m_moveDir.x), (cameraForward.z * m_moveDir.y) - (cameraForward.x * m_moveDir.x));
            Debug.Log(m_moveDir);
            m_moveComponent.Move(moveVec, dash);
        }
        else
        {
            // Hardcoded timer for if player has collided with another player,
            //      they can't do inputs for n frames, allowing the push back 
            //      force to apply
            dashTimer++;
            if (dashTimer > 20)
            {
                isPushed = false;
                dashTimer = 0;
            }
        }
    }

    public void SetForward(Vector3 forward_)
    {
        cameraForward = forward_;
    }

    /// Unity Input Events -----------------------------------

    // Move Action
    public void Move(InputAction.CallbackContext context)
    {
        m_moveDir = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();
    }
    // Interact Action
    public void Interact(InputAction.CallbackContext context)
    {
        if (this.interact == 0) {
            if (this.nearInteractable != null && this.nearInteractable != this.inventory) {
                IInteractable pickup = this.nearInteractable.activated(this.inventory);
                this.Drop();
                this.Pickup(pickup);
            } else if (this.inventory != null) {
                this.Drop();
            }
        }
        this.interact = context.ReadValue<float>();
    }

    // Dash Action
    public void Dash(InputAction.CallbackContext context)
    {
        dash = context.ReadValue<float>();
    }


    /// Collision Events --------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0, rb.angularVelocity.z);
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (collision.gameObject.tag == "Player")
        {
            Vector3 otherPos = collision.gameObject.transform.position;
            Vector3 collisionDir = this.transform.position - otherPos;
            m_moveComponent.Move(collisionDir, 1.0f);
            isPushed = true;
            
            
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0, rb.angularVelocity.z);
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    public void OnCollisionStay(Collision collision)
    {
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0, rb.angularVelocity.z);
        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    /// Trigger Events --------------------------------------
    public void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null && interactable.wasTriggered(inventory)) {
            this.nearInteractable = other.gameObject.GetComponent<IInteractable>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (this.nearInteractable == interactable)
        {
            this.nearInteractable = null;
        }
    }

    private void Pickup(IInteractable item) {
        if (item != null) {
            this.inventory = item;
            GameObject go = ((MonoBehaviour)item).gameObject;
            go.transform.parent = this.holdSpace.transform;
        }
    }

    private void Drop() {
        if (this.inventory != null) {
            bool consumed = this.inventory.deactivated();
            if (!consumed) {
                GameObject go = ((MonoBehaviour)this.inventory).gameObject;
                go.transform.SetParent(null);
            }
        }
        this.inventory = null;
    }
}
