using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Highlighters_URP;
using Highlighters;
using UnityEditor;

public class VehicleOutliner : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] Color defaultColor = new();
    [SerializeField] Color fullColor = new();

    Outline _outline;
    

    // Start is called before the first frame update
    void Start()
    {
        _outline = GetComponent<Outline>();
        VehicleSystem.OnFullUpdate += UpdateOutlineOnFullness;

        UpdateOutlineOnFullness();  
    }
    
    
    public void UpdateOutlineOnFullness()
    {
        if (GameManager.Instance.vehicle.IsFull())
        {
            _outline.OutlineColor = fullColor;
        }
        else
        {
            _outline.OutlineColor = defaultColor;
        }
    }

    private void OnDestroy()
    {
        VehicleSystem.OnFullUpdate -= UpdateOutlineOnFullness;
    }
}
