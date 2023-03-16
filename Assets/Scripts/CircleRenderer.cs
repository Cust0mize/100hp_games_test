using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    [SerializeField] private int _segments = 64;
    [SerializeField] private LineRenderer _lineRenderer;
    private float _radius;

    public void Init(float radius) {
        _radius = radius;
        UpdateRender();
    }

    private void UpdateRender() {
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / _segments;
        float theta = 0f;

        for (int i = 0; i < _segments + 1; i++) {
            float x = _radius * Mathf.Cos(theta);
            float z = _radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, 0f, z);
            _lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}