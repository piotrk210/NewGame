using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public float cameraSpeed, zoomSpeed, groundHight;
    public Vector2 cameraHightMinMax;
    public Vector2 cameraRotationMinMax;
    [Range(0, 1)] public float zoomLerp = 0.1f;
    [Range(0, 0.2f)] public float cursorThreshold;


    new Camera camera;
    RectTransform selectionBox;

    Vector2 mousePos, mousePosScreen, keyBoardInput;
    bool isCursorInGameScreen;

    private void Awake()
    {
        selectionBox = (RectTransform)GetComponentInChildren<Image>(true).transform;
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        UptadeClick();
        UptadeZoom();
        UptadeMovement();
    }

    void UptadeMovement()
    {
        keyBoardInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        mousePos = Input.mousePosition;
        mousePosScreen = camera.ScreenToViewportPoint(mousePos);
        isCursorInGameScreen = mousePosScreen.x>=0 && mousePosScreen.x <=1 && mousePosScreen.y >= 0 && mousePosScreen.y <= 1;

        Vector2 movmentDirection = keyBoardInput;


        if(isCursorInGameScreen)
        {
            if (mousePosScreen.x < cursorThreshold)
            {
                movmentDirection.x -= 1 - mousePosScreen.x / cursorThreshold;
            }

            if (mousePosScreen.x > 1 - cursorThreshold)
            {
                movmentDirection.x += 1 - ((1 - mousePosScreen.x) / cursorThreshold);
            }
            if (mousePosScreen.y < cursorThreshold)
            {
                movmentDirection.y -= 1 - mousePosScreen.y / cursorThreshold;
            }

            if (mousePosScreen.y > 1 - cursorThreshold)
            {
                movmentDirection.y += 1 - ((1 - mousePosScreen.y) / cursorThreshold);
            }

        }
        var deltaPosition = new Vector3(movmentDirection.x, 0, movmentDirection.y);
        deltaPosition *= cameraSpeed * Time.deltaTime;
        transform.position += deltaPosition;
    }

    void UptadeClick()
    {
        //todo
        //selectionBox.anchoredPosition = mousePos;
    }
    void UptadeZoom()
    {
        var mouseScroll = Input.mouseScrollDelta;
        float zoomDelta = mouseScroll.y * zoomSpeed * Time.deltaTime;
        zoomLerp = Mathf.Clamp(zoomLerp + zoomDelta, 0, 1);

        var position = transform.localPosition;
        position.y = Mathf.Lerp(cameraHightMinMax.y, cameraHightMinMax.x, zoomLerp)+ groundHight;
        transform.localPosition = position;

        var rotaction = transform.localEulerAngles;
        rotaction.x = Mathf.Lerp(cameraRotationMinMax.y, cameraRotationMinMax.x, zoomLerp);
        transform.localEulerAngles = rotaction;

    }


}
