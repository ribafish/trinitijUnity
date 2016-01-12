using UnityEngine;
using System.Collections;

public class NebulaCreator : MonoBehaviour {

    public Vector3 areaSize = Vector3.zero;

    public GameObject[] nebulas = null;
    public int[] nebulasAmount = null;


	void Start ()
    {
	    if (nebulas != null && nebulasAmount != null)
        {
            float x;
            float y;
            float z;

            for(int i = 0; i < nebulas.Length; i++)
            {
                for(int j = 0; j < nebulasAmount[i]; j++)
                {
                    x = areaSize.x * Mathf.Pow(-1, Random.Range(1, 2));
                    y = areaSize.y * Mathf.Pow(-1, Random.Range(1, 2));
                    z = areaSize.z * Mathf.Pow(-1, Random.Range(1, 2));
                    Instantiate(nebulas[i], new Vector3(Random.value * x, Random.value * y, Random.value * z), Random.rotation);
                }
            }
        }
	}

    //Za lazjo vizualizacijo in izbire

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}