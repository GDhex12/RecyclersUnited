using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanWater : MonoBehaviour
{

    public float minScale = 0.95f;
    public float maxScale = 1.05f;


    public float scaleSpeed = 0.1f;

    public Color edgeColor = Color.blue;

    private Material waterMaterial;

    private float currentScale = 1.0f;

    private Color currentEdgeColor;

    // Start is called before the first frame update
    void Start()
    {

        waterMaterial = GetComponent<Renderer>().material;
        currentEdgeColor = edgeColor;
    }

    // Update is called once per frame
    void Update()
    {

        float scaleAmount = Mathf.Sin(Time.time * scaleSpeed) * 0.5f + 0.5f;
        currentScale = Mathf.Lerp(minScale, maxScale, scaleAmount);
        transform.localScale = new Vector3(currentScale, 1.0f, currentScale);

    }
}
