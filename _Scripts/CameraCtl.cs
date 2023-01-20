using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtl : MonoBehaviour
{
    //lay nhan vat
    public Transform target;

    public float SmoothSpeed = .25f;

    // offset la khoang cach tu camera den player
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // khoang cach cam den player = toa do nhan vat - toa do camera
        offset = target.position - transform.position;
    }

    // player chuyen dong truoc nen dung lam lateupdate de control camera
    private void LateUpdate()
    {
        Vector3 desiredPos = target.position - offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, SmoothSpeed);
        //transform.position = toa do camera
        if(target.position.y < 1f) // toa do cao nhat cua player
        {
            smoothPos.y = transform.position.y;
        }

        transform.position = smoothPos;

    }
}
