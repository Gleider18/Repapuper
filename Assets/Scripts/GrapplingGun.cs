using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    [SerializeField] private LineRenderer lr;
    [SerializeField] private Animator animator;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    private int currentItemIndex = 0;
    [SerializeField] private List<GameObject> items;


    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentItemIndex == 0) return;
            currentItemIndex = 0;
            items[0].SetActive(true);
            items[1].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopGrapple();
            if (currentItemIndex == 1) return;
            currentItemIndex = 1;
            items[0].SetActive(false);
            items[1].SetActive(true);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            if (currentItemIndex == 0)
            {
                StartGrapple();
            }
            else
            {
                animator.Play("Hit");
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (currentItemIndex == 0)
            {
                StopGrapple();
            }
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
