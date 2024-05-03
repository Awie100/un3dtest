using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{

    Mesh m;
    MeshFilter mf;
    MeshCollider mc;
    float[,] nMap;
    Vector3Int dims = new Vector3Int(50,50,50);

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        mc = GetComponent<MeshCollider>();

        m = new Mesh();
        mf.mesh = m;
        nMap = Noise.Generate(dims.x, dims.z, 0.5f, 4, 0.5f, 2);
        Draw();
    }

    // Update is called once per frame
    void Draw()
    {
        float[,,] grid = new float[dims.x,dims.y,dims.z];
        List<Vector3> tris = new List<Vector3>();

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int k = 0; k < grid.GetLength(2); k++)
                {
                    grid[i, j, k] = (float)j / grid.GetLength(1) - nMap[i, k];
                }
            }
        }

        for (int i = 0; i < grid.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < grid.GetLength(1) - 1; j++)
            {
                for (int k = 0; k < grid.GetLength(2) - 1; k++)
                {
                    Cube cube = new Cube(
                        new Vector4(i, j, k, grid[i,j,k]),
                        new Vector4(i, j, k+1, grid[i, j, k+1]),
                        new Vector4(i+1, j, k+1, grid[i+1, j, k+1]),
                        new Vector4(i+1, j, k, grid[i+1, j, k]),
                        new Vector4(i, j+1, k, grid[i, j+1, k]),
                        new Vector4(i, j+1, k+1, grid[i, j+1, k+1]),
                        new Vector4(i+1, j+1, k+1, grid[i+1, j+1, k+1]),
                        new Vector4(i+1, j+1, k, grid[i+1, j+1, k])
                        );

                    tris.AddRange(cube.GetTris());
                }
            }
        }

        List<Vector3> vs = new List<Vector3>();
        List<int> vInd = new List<int>();
        List<Vector3> norms = new List<Vector3>();

        for (int i = 0; i < tris.Count / 3; i++)
        {
            Vector3 norm = Vector3.Cross(tris[3 * i] - tris[3 * i + 1], tris[3 * i] - tris[3 * i + 2]).normalized;

            for (int j = 0; j < 3; j++)
            {
                vs.Add(tris[3 * i + j]);
                vInd.Add(vs.Count - 1);
                norms.Add(norm);
            }

        }

        //vInd.Reverse();

        m.vertices = vs.ToArray();
        m.triangles = vInd.ToArray();
        m.normals = norms.ToArray();
        mc.sharedMesh = m;
    }

}