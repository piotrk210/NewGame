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
    Rect selectionRect, boxRect;

    List<Unit> selectedUnits = new List<Unit>();

    private void Awake()
    {
        selectionBox = (RectTransform)GetComponentInChildren<Image>(true).transform;
        camera = GetComponent<Camera>();
        selectionBox.gameObject.SetActive(false);
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
        raySelected = camera.ViewportPointToRay(mousePosScreen);
        //Debug.Log(raySelected.ToString());
        if (Input.GetMouseButtonDown(0) && !Physics.Raycast(raySelected, out rayHitSelected, 1000, MonsterLayerMask))
        {

            selectionBox.gameObject.SetActive(true);
            selectionRect.position = mousePos;
            
        }
        if (Input.GetMouseButtonUp(0) && !Physics.Raycast(raySelected, out rayHitSelected, 1000, MonsterLayerMask))
        {
            selectionBox.gameObject.SetActive(false);
        }
        if (Input.GetMouseButton(0) && !Physics.Raycast(raySelected, out rayHitSelected, 1000, MonsterLayerMask))
        {

            selectionRect.size = mousePos - selectionRect.position;
                boxRect = AbsRect(selectionRect);
                selectionBox.anchoredPosition = boxRect.position;
                selectionBox.sizeDelta = boxRect.size;
                UpdateSelectinginRect();
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            GiveCommand();
        }
        if(Input.GetMouseButton(0) && Physics.Raycast(raySelected, out rayHitSelected, 1000, MonsterLayerMask))
        {
            var selectedMonster = (rayHitSelected.collider.gameObject.GetComponent<Unit>());

            UpdateSelectingonClick(selectedMonster);
        }
    }
    void UpdateSelectingonClick(Unit monster)
    {
        selectedUnits.Clear();
        foreach (Unit unit in Unit.SeletableUnit)
        {
            if (!unit || !unit.IsAlive) continue;
            (unit as ISeletable).SetSelected(false);
            if( unit==monster)
            {
            (unit as ISeletable).SetSelected(true);
            selectedUnits.Add(unit);
            }
        }
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
    Rect AbsRect(Rect rect)
    {
        if(rect.width < 0)
        {
            rect.x += rect.width;
            rect.width *= -1;
        }
        if (rect.height< 0)
        {
            rect.y += rect.height;
            rect.height *= -1;
        }
        return rect;
    }

    void UpdateSelectinginRect()
    {
        selectedUnits.Clear();
        foreach(Unit unit in Unit.SeletableUnit)
        {
            if(!unit || !unit.IsAlive) continue;
            var pos = unit.transform.position;
            var posScreen = camera.WorldToScreenPoint(pos);
            bool inRect = IsPointInRect(boxRect, posScreen);
            (unit as ISeletable).SetSelected(inRect);
            if(inRect)
            {
                selectedUnits.Add(unit);
            }
        }
    }


    bool IsPointInRect(Rect rect, Vector2 point)
    {
        return point.x >= rect.position.x && point.x <= (rect.position.x + rect.size.x) &&
            point.y >= rect.position.y && point.y <= (rect.position.y + rect.size.y);
    }

    Ray ray, raySelected;
    RaycastHit rayHit, rayHitSelected;
    [SerializeField] LayerMask commandLayerMask = -1, MonsterLayerMask; 

    void GiveCommand()
    {
        ray = camera.ViewportPointToRay(mousePosScreen);
        if(Physics.Raycast(ray,out rayHit, 1000, commandLayerMask))
        {
            object commandData = null;
            if(rayHit.collider is TerrainCollider)
            {
                //Debug.Log("Terrain: " + rayHit.point.ToString());
                commandData = rayHit.point;
            }
            else
            {
                //Debug.Log(rayHit.collider);
                commandData = rayHit.collider.gameObject.GetComponent<Unit>();
            }
            GiveCommmands(commandData);
        }
    }

    void GiveCommmands(object dataCommand)
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.SendMessage("Command", dataCommand, SendMessageOptions.DontRequireReceiver);
        }
    }

    void SelectByClick()
    {
        ray = camera.ViewportPointToRay(mousePosScreen);
        if (Physics.Raycast(ray, out rayHit, 1000, MonsterLayerMask))
        {
            var monster = (rayHit.collider.gameObject.GetComponent<Unit>());
            if (monster && monster.IsAlive) selectedUnits.Add(monster);
        }
    } 
}
