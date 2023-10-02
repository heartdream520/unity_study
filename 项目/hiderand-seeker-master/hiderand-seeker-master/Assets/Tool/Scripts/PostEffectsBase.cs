using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
public class PostEffectsBase : EffectsBase
{
	private Camera cam;
	public Camera Cam
	{ 
		get 
		{ 
			if (cam == null) 
			{ 
				cam = GetComponent<Camera>(); 
			}
			return cam;
		} 
	}

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (Material != null)
		{
			SetMaterialData();
			Graphics.Blit(source, destination, Material);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}
}
