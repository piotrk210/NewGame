using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speakingText : MonoBehaviour
{
    const string HPCANVAS = "HPCanvas";
    Text text;
    Unit unit;
    Transform cameraTransform;

    [SerializeField] Vector3 offset;

    private void Awake()
    {
        text = GetComponent<Text>();
        unit = GetComponentInParent<Unit>();
        GameObject canvas = GameObject.FindGameObjectWithTag(HPCANVAS);
        if (canvas != null) transform.SetParent(canvas.transform);
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (!unit)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = unit.transform.position + offset;
        transform.LookAt(cameraTransform);
        var rotation = transform.localEulerAngles;
        rotation.y = 0;
        rotation.x = 30;
        transform.localEulerAngles = rotation;
    }

    void ChangeTextValue (string newText)
    {
        text.text = newText;
    }
}
