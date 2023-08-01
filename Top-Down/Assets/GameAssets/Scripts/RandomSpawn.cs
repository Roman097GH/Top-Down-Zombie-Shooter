using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace TopDown
{
    public class RandomSpawn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemySpawnPoint = new List<GameObject>();

        private void Start()
        {
            NavMeshTriangulation navMeshTriangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0, navMeshTriangulation.vertices.Length);
            Debug.Log(vertexIndex);
            
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(navMeshTriangulation.vertices[vertexIndex], out navMeshHit, 2f, -1))
            {
                Instantiate(_enemySpawnPoint[0], navMeshHit.position, Quaternion.identity);
            }
        }
    }
}
