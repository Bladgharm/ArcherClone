using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    [SerializeField]
    private float _width;
    [SerializeField]
    private float _heigth;
    [SerializeField]
    private int _separations;

    private MeshFilter _meshFilter;
    private PolygonCollider2D _polygonCollider2D;

    private Vector3[] _vertices;
    private Vector2[] _uvs;
    private int[] _triangles;

    private bool _canRandomizeNextPoint = false;

    private List<Vector2> _topPoints = new List<Vector2>();

	private void Start ()
	{
	    _meshFilter = GetComponent<MeshFilter>();
	    _polygonCollider2D = GetComponent<PolygonCollider2D>();

        _vertices = CreateVertices(_width, _heigth, _separations);
	    _polygonCollider2D.points = _topPoints.ToArray();

	    _uvs = CreateUv(_vertices);
	    _triangles = CreateTriangles(_separations);
        var mesh = CreateMesh(_vertices, _uvs, _triangles);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
	    _meshFilter.mesh = mesh;
	}

    private Mesh CreateMesh(Vector3[] vertices, Vector2[] uvs, int[] triangles)
    {
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";
        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;
        m.RecalculateNormals();

        return m;
    }

    private Vector3[] CreateVertices(float width, float height, int separations)
    {
        _width = width;
        _heigth = height;

        List<Vector3> vertices = new List<Vector3>();

        Vector3 lastBottom = new Vector3(0,-10f);
        Vector3 lastTop = new Vector3(0, height, 0);
        vertices.Add(lastBottom);
        vertices.Add(lastTop);
        //_topPoints.Add(lastBottom);
        _topPoints.Add(lastTop);
        var partLength = width / separations;
        for (int i = 0; i < separations; i++)
        {
            int randomY = 0;
            if (_canRandomizeNextPoint)
            {
                randomY = UnityEngine.Random.Range(-2, 2);
                _canRandomizeNextPoint = !_canRandomizeNextPoint;
            }
            else
            {
                _canRandomizeNextPoint = !_canRandomizeNextPoint;
            }

            lastTop = new Vector3(lastTop.x + partLength, lastTop.y + randomY, lastTop.z);
            vertices.Add(lastTop);
            _topPoints.Add(lastTop);
            lastBottom = new Vector3(lastBottom.x + partLength, lastBottom.y, lastBottom.z);
            vertices.Add(lastBottom);
            //_topPoints.Add(lastBottom);
        }
        _topPoints.Add(lastBottom);
        return vertices.ToArray();
    }

    private Vector2[] CreateUv(Vector3[] vertices)
    {
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / _width, vertices[i].y / _heigth);
        }

        return uvs;
    }

    private int[] CreateTriangles(int separations)
    {
        var trianglesCount = (separations * 2) * 3;
        int[] triangles = new int[trianglesCount];
        int index = 6;
        int verticesIndex = 3;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        for (int i = 0; i < separations-1; i++)
        {
            triangles[index] = verticesIndex;
            index ++;
            triangles[index] = verticesIndex - 1;
            index++;
            triangles[index] = verticesIndex + 1;
            index++;
            triangles[index] = verticesIndex;
            index++;
            triangles[index] = verticesIndex + 1;
            index++;
            triangles[index] = verticesIndex + 2;
            index++;
            verticesIndex += 2;
        }

        return triangles;
    }
}