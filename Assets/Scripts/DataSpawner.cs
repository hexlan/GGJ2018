using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSpawner : MonoBehaviour
{

    public GameObject dataSource;
    public GameObject[] data;
    public Vector3[] spawnLocations;

    private void Start()
    {
        data = new GameObject[]{
            null,
            null,
            null,
            null,
            null,
            null
        };
        spawnLocations = new Vector3[]
        {
            new Vector3(130.0f, 20.0f, -95.0f),
            new Vector3(-20.0f, 20.0f, -95.0f),
            new Vector3(-20.0f, 20.0f, 150.0f),
            new Vector3(130.0f, 20.0f, 150.0f),
            new Vector3(-198.0f, 20.0f, 16.0f),
            new Vector3(300.0f, 20.0f, 16.0f)
        };
    }

    bool ContainsNull()
    {
        for (var i = 0; i < data.Length; i++)
        {
            if (data[i] == null) return true;
        }

        return false;
    }

    void Update()
    {
        if (ContainsNull())
        {
            var value = Random.value * 1000;
            if (value > 992)
            {
                var index = (int)(Random.value * data.Length);
                while (data[index] != null)
                {
                    index = (int)(Random.value * data.Length);
                }
                data[index] = Instantiate(dataSource, spawnLocations[index], dataSource.transform.rotation);
            }
        }

    }
}
