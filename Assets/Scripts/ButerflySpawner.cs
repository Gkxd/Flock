using UnityEngine;
using System.Collections;

public class ButerflySpawner : MonoBehaviour
{

    public GameObject butterflyPrefab;

    public int amount;

    public float minX, maxX, minY, maxY, minZ, maxZ;

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = Random.onUnitSphere * Random.Range(30, 70);
            position.y = Mathf.Abs(position.y);
            
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            position.z = Mathf.Clamp(position.z, minZ, maxZ);

            Instantiate(butterflyPrefab, position, Quaternion.identity, transform);
        }
    }

}
