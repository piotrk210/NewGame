using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    const string HPCANVAS = "HPCanvas";
    Slider slider;
    Unit unit;
    Transform cameraTransform;

    [SerializeField] Vector3 offset;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        unit = GetComponentInParent<Unit>();
        GameObject canvas = GameObject.FindGameObjectWithTag(HPCANVAS);
        if (canvas!=null) transform.SetParent(canvas.transform);
        cameraTransform =  Camera.main.transform;
    }

    private void Update()
    {
        if(!unit)
        {
            Destroy(gameObject);
            return;
        }

        slider.value = unit.HealthPrecent;
        transform.position = unit.transform.position + offset;
        transform.LookAt(cameraTransform);
        var rotation = transform.localEulerAngles;
        rotation.y = 180;
        transform.localEulerAngles = rotation;
    }
}
