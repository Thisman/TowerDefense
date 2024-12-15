using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class BufferTowerController : MonoBehaviour
{
    [SerializeField]
    private BufferTowerModel _bufferTowerModel;

    [SerializeField]
    private Color _watchRadiusColor = Color.red;

    private LineRenderer _lineRenderer;
    private int _segments = 50;

    public void Start()
    {
        //CreateWatchRadiusCircle();
    }

    private void CreateWatchRadiusCircle()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();

        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = _segments;
        _lineRenderer.startColor = _watchRadiusColor;
        _lineRenderer.endColor = _watchRadiusColor;

        DrawCircle();
    }

    private void DrawCircle()
    {
        float angleStep = 360f / _segments;

        Vector3[] positions = new Vector3[_segments];

        for (int i = 0; i < _segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * _bufferTowerModel.WatchRadius;
            float y = Mathf.Sin(angle) * _bufferTowerModel.WatchRadius;
            positions[i] = new Vector3(x, y, 0f);
        }

        _lineRenderer.SetPositions(positions);
    }
}
