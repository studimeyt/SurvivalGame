using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smooth_steps =10;

    [SerializeField]
    private float smooth_weight = 0.4f;

    [SerializeField]
    private float roll_angle = 10f;

    [SerializeField]
    private float roll_speed = 3f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70f, 70f);

    private Vector2 look_angle;

    private Vector2 current_mouse_look;

    private Vector2 smooth_move;

    private float current_roll_angle;

    private int last_look_frame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CursorLockandUnlock();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void CursorLockandUnlock()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }// cursor lock and unlock

    void LookAround()
    {
        current_mouse_look = new Vector2((Input.GetAxis(MouseAxis.Mouse_Y)), (Input.GetAxis(MouseAxis.Mouse_X)));
        look_angle.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
        look_angle.y += current_mouse_look.y * sensitivity ;

        look_angle.x = Mathf.Clamp(look_angle.x, default_Look_Limits.x, default_Look_Limits.y);//clamping i.e. limiting values

        /*
         * it makes your character drunk i.e. allows roation on z-axis
         * 
         * current_roll_angle = Mathf.Lerp(current_roll_angle, Input.GetAxisRaw(MouseAxis.Mouse_X) * roll_angle, Time.deltaTime * roll_speed);
         */

        lookRoot.localRotation = Quaternion.Euler(look_angle.x, 0f, 0f);

        playerRoot.localRotation = Quaternion.Euler(0f, look_angle.y, 0f);
    }//look around


}//class
