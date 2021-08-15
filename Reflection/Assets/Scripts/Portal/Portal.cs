using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {


    /*
     * INPORTANT DETAILS
     * 1. Main camera "Near" clipping planes should be set to 0.03, not 0.3
     * 2. The screen should be a cube, not a plane
     * 3. The screens should not have colliders. The collider should be on the parent, and should be a trigger. 
     * 4. 
     */

    public Portal linkedPortal;
    public MeshRenderer screenRenderer; // <-----


    private RenderTexture texture;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera portalCamera;


    //list of things trying to teleport
    [SerializeField] private List<Teleporter> potentialTeleporters = new List<Teleporter>();
    private void Awake() {
        playerCamera = Camera.main;
        portalCamera = this.gameObject.GetComponentInChildren<Camera>();
        portalCamera.enabled = false;
        screenRenderer.material.SetInt("displayMask", 1);
    }

    private void LateUpdate() {
        for(int i = 0; i < potentialTeleporters.Count; i++) {
            Teleporter obj = potentialTeleporters[i];
            var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * obj.transform.localToWorldMatrix;
            Vector3 offset = obj.transform.position - transform.position;
            int oldPortalSide = System.Math.Sign(Vector3.Dot(obj.previousOffset, transform.forward));
            int newPortalSide = System.Math.Sign(Vector3.Dot(offset, transform.forward));

            if(newPortalSide != oldPortalSide) {
                
                obj.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);

                linkedPortal.EnteredPortal(obj);
                potentialTeleporters.RemoveAt(i);
                i--;
            } else {
                obj.previousOffset = offset;
            }
        }
    }
    private Vector3 portalCamPos {
        get {
            return portalCamera.transform.position;
        }
    }
    private bool SameSideOfPortal(Vector3 posA, Vector3 posB) {
        return SideOfPortal(posA) == SideOfPortal(posB);
    }
    private int SideOfPortal(Vector3 pos) {
        return System.Math.Sign(Vector3.Dot(pos - transform.position, transform.forward));
    }
    public void PostRender() {
        ScreenThicknessClipping(playerCamera.transform.position);
    }

    public void ScreenThicknessClipping(Vector3 viewPoint) {
        float halfHeight = playerCamera.nearClipPlane * Mathf.Tan(playerCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth = halfHeight * playerCamera.aspect;
        float dstToNearClipPlaneCorner = new Vector3(halfWidth, halfHeight, playerCamera.nearClipPlane).magnitude;
        float screenThickness = dstToNearClipPlaneCorner;

        Transform screenT = screenRenderer.transform;
        bool camFacingSameDirAsPortal = Vector3.Dot(transform.forward, transform.position - viewPoint) > 0;
        //screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y, screenThickness);
        screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y, screenThickness);
        screenT.localPosition = Vector3.forward * screenThickness * ((camFacingSameDirAsPortal) ? 0.5f : -0.5f);
        //screenT.localPosition = Vector3.forward * screenThickness;
        //return screenThickness;
    }
    private void CreateViewTexture() {
        if (texture == null || texture.width != Screen.width || texture.height != Screen.height) {
            if (texture != null) {
                texture.Release();
            }

            texture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCamera.targetTexture = texture;
            linkedPortal.screenRenderer.material.SetTexture("_MainTex", texture);
        }
    }

    private bool IsVisible(Renderer rend, Camera cam) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, rend.bounds);
    }
    public void Render() {
        if (!IsVisible(linkedPortal.screenRenderer, playerCamera)) {
            Texture2D defaultTexture = new Texture2D(1, 1);
            defaultTexture.SetPixel(0, 0, Color.black);
            defaultTexture.Apply();
            linkedPortal.screenRenderer.material.SetTexture("_MainTex", defaultTexture);
            return;
        }
        linkedPortal.screenRenderer.material.SetTexture("_MainTex", texture);
        //print("Hi i am working");

        this.screenRenderer.enabled = false;
        CreateViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        portalCamera.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        /*if(Vector3.Distance(transform.position, playerCamera.transform.position) > 2f) {
            SetNearClip();
        } else {
            
            linkedPortal.portalCamera.ResetProjectionMatrix();
        } */
            
        portalCamera.Render();
        this.screenRenderer.enabled = true;
    }

    private void SetNearClip() {
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalCamera.transform.position));
        Vector3 camSpacePosition = portalCamera.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCamera.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDistance = -Vector3.Dot(camSpacePosition, camSpaceNormal) + 0.01f;
        if (Mathf.Abs(camSpaceDistance) > 0.2f) {
            Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDistance);
            portalCamera.projectionMatrix = playerCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        } else {
            portalCamera.projectionMatrix = playerCamera.projectionMatrix;
        }
    }

    public void EnteredPortal(Teleporter traveller) {
        if (!potentialTeleporters.Contains(traveller)) {
            //if this object is not already in the list of teleporters
            traveller.EnterPortal();
            traveller.previousOffset = traveller.transform.position - transform.position;
            potentialTeleporters.Add(traveller);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Teleporter>() != null) {
            //if this object can teleport through portals
            Teleporter obj = other.GetComponent<Teleporter>();
            if (obj) { 
                EnteredPortal(obj);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<Teleporter>() != null) {
            Teleporter obj = other.GetComponent<Teleporter>();
            if (obj && potentialTeleporters.Contains(obj)) {
                obj.ExitPortal();
                potentialTeleporters.Remove(obj);
            }
        }
    }
}
