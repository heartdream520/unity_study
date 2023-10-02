using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoSingleton<PlayerInputManager>
{
    public Vector2 GetPlayerXYInput()
    {
        
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //return new Vector2(horizontalInput, verticalInput).normalized;
        return playerInput.normalized;
    }
    bool hasTouch=false;
    Vector2 firstTouchPos;
    Vector2 playerInput;
    private void Update()
    {
        // ����Ƿ��д�������
        if (Input.touchCount > 0)
        {
            // ��ȡ��һ���������λ��
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            if (!hasTouch)
            {
                hasTouch = true;
                firstTouchPos = touchPosition;
                playerInput = Vector2.zero;
            }
            else
            {
                playerInput = touchPosition - firstTouchPos;
            }
            //Debug.Log("Touch Position: " + touchPosition + "  playerInput" + playerInput);
        }
        else
        {
            playerInput = Vector2.zero;
            hasTouch = false;
        }
    }
}


