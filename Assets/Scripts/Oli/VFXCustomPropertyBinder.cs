using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXCustomPropertyBinder : MonoBehaviour
{
    public string Property;
    public GameObject Target;

    private VisualEffect visualEffect;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        visualEffect.SetVector3(Property, Target.transform.position);
    }
}
