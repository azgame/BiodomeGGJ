using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    // Private
    Vector3 offset;
    float yaw;
    float pitch;

    // Public
    public GameObject target;
    Player player;
    //public float pitchMinClamp;
    //public float pitchMaxClamp;
    public float dampen;
    public float rotateSpeed;

    void Start()
    {
        player = target.gameObject.GetComponent<Player>();
        //if (pitchMinClamp <= -20 || pitchMinClamp >= 0)
        //{
        //    pitchMinClamp = -10;
        //    Debug.LogWarning("Pitch Minimum not set properly. Changed to: " + pitchMinClamp.ToString());
        //}

        //if (pitchMaxClamp <= -10 || pitchMaxClamp >= 10)
        //{
        //    pitchMaxClamp = 0;
        //    Debug.LogWarning("Pitch Maximum not set properly. Changed to: " + pitchMaxClamp.ToString());
        //}

        if (dampen <= 0)
        {
            dampen = 8;
            Debug.LogWarning("Dampen not set properly. Changed to: " + dampen.ToString());
        }

        if (rotateSpeed <= 0)
        {
            rotateSpeed = 5;
            Debug.LogWarning("RotateSpeed not set properly. Changed to: " + rotateSpeed.ToString());
        }

        yaw = 0;
        pitch = -5;
        offset = target.transform.position - transform.position;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        yaw += player.lookDir.x;
        pitch -= player.lookDir.y;
        //pitch = Mathf.Clamp(pitch, pitchMinClamp, pitchMaxClamp);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        transform.position = target.transform.position - (rotation * offset);
        transform.LookAt(target.transform.position);
    }

    
}