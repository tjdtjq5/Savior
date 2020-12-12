using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("카메라")] public Transform theCam_transform; Camera theCam;
    [Header("이동속도")] public float move_speed;
    [Header("화살표")] public Transform arrow;
    bool leftMoveFlag;
    bool rightMoveFlag;
    Vector2 DestinationPos;

    private void Start()
    {
        theCam = theCam_transform.GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        theCam_transform.position = new Vector3(this.transform.position.x, theCam_transform.position.y, theCam_transform.position.z);

        if (leftMoveFlag)
        {
            this.transform.position = new Vector2(this.transform.position.x - (move_speed * Time.deltaTime), this.transform.position.y);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (this.transform.position.x - DestinationPos.x < Mathf.Abs(0.1f))
            {
                leftMoveFlag = false;
                rightMoveFlag = false;
                arrow.position = new Vector2(1000, arrow.position.y);
            }
        }

        if (rightMoveFlag)
        {
            this.transform.position = new Vector2(this.transform.position.x + (move_speed * Time.deltaTime), this.transform.position.y);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (DestinationPos.x - this.transform.position.x < Mathf.Abs(0.1f))
            {
                leftMoveFlag = false;
                rightMoveFlag = false;
                arrow.position = new Vector2(1000, arrow.position.y);
            }
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = theCam.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100);
            if (hit && hit.transform.tag == "길")
            {
                if (mousePos.x - this.transform.position.x < 0)
                {
                    leftMoveFlag = true;
                    rightMoveFlag = false;
                }
                else
                {
                    leftMoveFlag = false;
                    rightMoveFlag = true;
                }
                DestinationPos = mousePos;
                arrow.position = new Vector2(DestinationPos.x, arrow.position.y);
            }
        }
    }


    public void LeftMove_PointDownBtn()
    {
    }
    public void LeftMove_PointUpBtn()
    {
        
    }

    public void RightMove_PointDownBtn()
    {
    }
    public void RightMove_PointUpBtn()
    {
    }
}
