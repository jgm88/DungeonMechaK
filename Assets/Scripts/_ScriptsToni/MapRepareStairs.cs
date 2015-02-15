using UnityEngine;
using System.Collections;

/// <summary>
/// Repare stairs on Map.
/// </summary>
/// 
[ExecuteInEditMode]
public class MapRepareStairs : MonoBehaviour 
{
	private const string kNAME_NEW_COLLIDER = "CubeCollider";

	public Transform originalTransform;

	private int _countEscaleras = 0;
	private int _countDungeonStairs = 0;
	private int _countOldCubeCollider = 0;
	private int _countNewCubeCollider = 0;

	public void __PLEASE_REMOVE___Start ()
	{
		// Construir rampas
		ApplyNewCubeCollider (true, false);

		// Mostrar info
		Debug.Log (string.Format (
			"Escaleras reparadas!! \n" +
			" - Escaleras: {0} \n" +
			" - dungeon_stairs: {1} \n" +
			" - CubeCollider destruidos: {2} \n" +
			" - CubeCollider creados: {3}",
			_countEscaleras, _countDungeonStairs, _countOldCubeCollider, _countNewCubeCollider));
	}

	/// <summary>
	/// Applies the new cube collider.
	/// </summary>
	/// <param name="destroyOldCubeCollider">If set to <c>true</c> destroy old plane collider.</param>
	/// <param name="createNewCubeCollider">If set to <c>true</c> create new collider.</param>
	private void ApplyNewCubeCollider (bool destroyOldCubeCollider = true, bool createNewCubeCollider = true)
	{
		// Recorrer Map
		foreach (Transform childMap in this.transform)
		{
			// Buscar Escaleras
			if (childMap.name == "Escaleras")
			{
				_countEscaleras++;

				foreach (Transform childEscaleras in childMap.transform)
				{
					// Buscar dungeon_stairs
					if (childEscaleras.name == "dungeon_stairs")
					{
						_countDungeonStairs++;

						// Quitar mesh collider de Cube
						childEscaleras.FindChild ("Cube").GetComponent<MeshCollider> ().enabled = false;

						// Destruir rampas antiguas si se pide
						if (destroyOldCubeCollider)
						{
							Transform oldCubeCollider;
							while (oldCubeCollider = childEscaleras.Find (kNAME_NEW_COLLIDER))
							{
								GameObject.DestroyImmediate (oldCubeCollider.gameObject);
								_countOldCubeCollider++;
							}
						}

						// Añadir nueva rampa si antigua no existe
						if (createNewCubeCollider && childEscaleras.Find (kNAME_NEW_COLLIDER) == null)
						{
							GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
							cube.name = kNAME_NEW_COLLIDER;
							cube.transform.parent = childEscaleras;

							if (originalTransform == null)
							{
								cube.transform.localScale = new Vector3 (0.01f, 2.86f, 1.86f); 
								cube.transform.Rotate (0f, 0f, 45f);
								cube.transform.position = new Vector3 (0.003f, 1f, 0f);
							}
							else
							{
								cube.transform.localScale = originalTransform.localScale;
								cube.transform.rotation = originalTransform.rotation;
								cube.transform.position = originalTransform.position;
							}
							
							_countNewCubeCollider++;
						}
					}
				}
			}
		}
	}
}
