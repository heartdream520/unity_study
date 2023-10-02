using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsBase : MonoBehaviour
{
	public Shader shader;

	private Material material;
	public Material Material
	{
		get
		{
			if (material == null && shader != null)
				material = new Material(shader);
			return material;
		}
	}

	private void OnValidate()
	{
		if (shader != null)
			material = new Material(shader);
		else
			material = null;
	}

	protected virtual void SetMaterialData()
	{

	}
}
