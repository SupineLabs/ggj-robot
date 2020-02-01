using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisonTracker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _vision;
    [SerializeField, Range(0,1)]
    private float _visionPercentage;

    public float VisionPercentage { get => _visionPercentage; set => _visionPercentage = value; }

    private void Update()
    {
        _vision.color = new Color(_vision.color.r, _vision.color.g, _vision.color.b, 1f * _visionPercentage);
    }
}
