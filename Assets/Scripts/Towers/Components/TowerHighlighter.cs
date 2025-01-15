using UnityEngine;

namespace Game.Towers
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(TowerStatsModel))]
    public class TowerHighlighter : MonoBehaviour
    {
        [SerializeField]
        private TowerStatsModel _towerStateModel;

        [SerializeField]
        private int segments = 50;

        [SerializeField]
        private Color borderColor = Color.red;

        [SerializeField]
        private Color fillColor = new Color(1f, 0.5f, 0.5f, 0.5f);

        [SerializeField]
        private LineRenderer _lineRenderer;

        [SerializeField]
        private GameObject _fillObject;

        private Mesh _mesh;

        public void Start()
        {
            _lineRenderer.positionCount = segments + 1;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = borderColor;
            _lineRenderer.endColor = borderColor;

            var meshFilter = _fillObject.AddComponent<MeshFilter>();
            var meshRenderer = _fillObject.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = fillColor };
            meshRenderer.sortingLayerName = "Default";
            meshRenderer.sortingOrder = 5;

            _mesh = new Mesh();
            meshFilter.mesh = _mesh;

            DrawCircleBorder();
            DrawCircleFill();
        }

        public void ShowEffectArea() {
            Debug.Log("ShowEffectArea");
            _lineRenderer.enabled = true;
            _fillObject.SetActive(true);
        }

        public void HideEffectArea() {
            Debug.Log("HideEffectArea");
            _lineRenderer.enabled = false;
            _fillObject.SetActive(false);
        }

        private void DrawCircleBorder()
        {
            float angle = 0f;
            for (int i = 0; i <= segments; i++)
            {
                float x = Mathf.Cos(angle) * _towerStateModel.EffectArea;
                float y = Mathf.Sin(angle) * _towerStateModel.EffectArea;
                _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
                angle += 2 * Mathf.PI / segments;
            }
        }

        private void DrawCircleFill()
        {
            Vector3[] vertices = new Vector3[segments + 2];
            int[] triangles = new int[segments * 3];

            vertices[0] = Vector3.zero; // Центр круга

            float angle = 0f;
            for (int i = 1; i <= segments + 1; i++)
            {
                vertices[i] = new Vector3(Mathf.Cos(angle) * _towerStateModel.EffectArea, Mathf.Sin(angle) * _towerStateModel.EffectArea, 0);
                angle += 2 * Mathf.PI / segments;
            }

            for (int i = 0; i < segments; i++)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

            _mesh.vertices = vertices;
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
        }
    }
}
