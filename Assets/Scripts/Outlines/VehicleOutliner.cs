using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Highlighters_URP;
using Highlighters;
using UnityEditor;

public class VehicleOutliner : MonoBehaviour
{
    [Header("Colors")]
    Highlighters.Highlighter _highlighter;
    [ColorUsage(true, true)]
    [SerializeField] Color defaultColor = new();
    [ColorUsage(true, true)]
    [SerializeField] Color fullColor = new();

    [Header("Vehicle parts (meshes)")]
    [SerializeField] List<HighlighterRenderer> truckParts = new List<HighlighterRenderer>();
    [SerializeField] List<HighlighterRenderer> miniParts = new List<HighlighterRenderer>();
    

    // Start is called before the first frame update
    void Start()
    {
        _highlighter = GetComponent<Highlighters.Highlighter>();

        UpdateOutlineOnFullness();
    }

    public void UpdateOutline()
    {

        _highlighter.GetRenderersInChildren();
    }
    public void UpdateOutlineOnTruck()
    {
        _highlighter.Renderers.Clear();
        foreach (HighlighterRenderer part in truckParts)
        {
            //_highlighter.GetRenderersInChildren;
        }
    }
    
    public void UpdateOutlineOnMini()
    {
        _highlighter.Renderers.Clear();
        foreach (HighlighterRenderer part in miniParts)
        {
            _highlighter.Renderers.Add(part);
        }
    }
    
    public void UpdateOutlineOnFullness()
    {
        if (GameManager.Instance.vehicle.IsFull())
        {
            _highlighter.Settings.MeshOutlineFront.Color = fullColor;
            _highlighter.Settings.OuterGlowColorFront = fullColor;
        }
        else
        {
            _highlighter.Settings.MeshOutlineFront.Color = defaultColor;
            _highlighter.Settings.OuterGlowColorFront = defaultColor;
        }
    }
}
