﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

	public GameObject m_fogOfWarPlane;
	[SerializeField]
	public Transform [] m_player;
	public LayerMask m_fogLayer;
	public float m_radius = 5f;
	private float m_radiusSqr { get { return m_radius * m_radius; } }

	private Mesh m_mesh;
	private Vector3[] m_vertices;
	private Color[] m_colors;
	private static int amountOfMonsters = 2;
	Vector3[] actualPosition = new Vector3[amountOfMonsters];
	Vector3[] lastPosition = new Vector3[amountOfMonsters];

	void Start()
	{
		Initialize();
	}

	void Update()
	{
		for (int j=0;j<m_player.Length;j++)
        {
			if (IsMonsterMoving(m_player[j].position))
			{
				Vector3 upperPosition = m_player[j].position;
				upperPosition.y += 100;
				Ray r = new Ray(upperPosition, m_player[j].position - upperPosition);
				RaycastHit hit;
				if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
				{
					for (int i = 0; i < m_vertices.Length; i++)
					{
						Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
						float dist = Vector3.SqrMagnitude(v - hit.point);
						if (dist < m_radiusSqr)
						{
							float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
							m_colors[i].a = alpha;
						}
					}
					UpdateColor();
				}
			}
		}
	}

	void Initialize()
	{
		m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		m_vertices = m_mesh.vertices;
		m_colors = new Color[m_vertices.Length];
		for (int i = 0; i < m_colors.Length; i++)
		{
			m_colors[i] = Color.black;
		}
		UpdateColor();
	}

	void UpdateColor()
	{
		m_mesh.colors = m_colors;
	}

	bool IsMonsterMoving(Vector3 monsterTransform)
    {
		actualPosition[0] = monsterTransform;
		if (actualPosition[0] == lastPosition[0]) return false;
		else
        {
			lastPosition[0] = actualPosition[0];
			return true;
		}
    }
}
