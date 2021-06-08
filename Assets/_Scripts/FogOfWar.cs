using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

	public GameObject fogOfWar;
	[SerializeField]
	public Transform [] transformOfMonsters;
	public LayerMask fogLayerMask;
	public float discoverRadius = 5f;
	private float discoverField { get { return discoverRadius * discoverRadius; } }

	private Mesh fogMesh;
	private Vector3[] fogVerticles;
	private Color[] fogColor;
	private static int amountOfMonsters = 3;
	Vector3[] actualPosition = new Vector3[amountOfMonsters];
	Vector3[] lastPosition = new Vector3[amountOfMonsters];

	void Start()
	{
		Initialize();
	}

	void Update()
	{
		for (int j=0;j<transformOfMonsters.Length;j++)
        {
			if (IsMonsterMoving(transformOfMonsters[j].position, j))
			{
				Vector3 upperPosition = transformOfMonsters[j].position;
				upperPosition.y += 100;
				Ray rayAtMonster = new Ray(upperPosition, transformOfMonsters[j].position - upperPosition);
				RaycastHit hit;
				if (Physics.Raycast(rayAtMonster, out hit, 1000, fogLayerMask, QueryTriggerInteraction.Collide))
				{
					for (int i = 0; i < fogVerticles.Length; i++)
					{
						Vector3 v = fogOfWar.transform.TransformPoint(fogVerticles[i]);
						float dist = Vector3.SqrMagnitude(v - hit.point);
						if (dist < discoverField)
						{
							float alpha = Mathf.Min(fogColor[i].a, dist / discoverField);
							fogColor[i].a = alpha;
						}
					}
					UpdateColor();
					//Debug.Log("nie stoi "+ j.ToString());
				}
			}// else Debug.Log("stoi " + j.ToString());
		}
	}

	void Initialize()
	{
		fogMesh = fogOfWar.GetComponent<MeshFilter>().mesh;
		fogVerticles = fogMesh.vertices;
		fogColor = new Color[fogVerticles.Length];
		for (int i = 0; i < fogColor.Length; i++)
		{
			fogColor[i] = Color.black;
		}
		UpdateColor();
	}

	void UpdateColor()
	{
		fogMesh.colors = fogColor;
	}

	bool IsMonsterMoving(Vector3 monsterTransform, int indexOfMonster)
    {
		actualPosition[indexOfMonster] = monsterTransform;
		if (actualPosition[indexOfMonster] == lastPosition[indexOfMonster]) return false;
		else
        {
			lastPosition[indexOfMonster] = actualPosition[indexOfMonster];
			return true;
		}
    }
}
