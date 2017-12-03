using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
	void DestroyGameObject ()
	{
		// Destroy this gameobject, this can be called from an Animation Event.
		Destroy (gameObject);
	}
}
