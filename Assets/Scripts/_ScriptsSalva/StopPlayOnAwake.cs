using UnityEngine;
using System.Collections;

/// <summary>
/// Stop play on awake.
/// </summary>
[ExecuteInEditMode]
public class StopPlayOnAwake : MonoBehaviour {

	private int _countPlayAwakes = 0;
#if UNITY_EDITOR
	public void Update ()
	{
		DisablePlaySounds();

		Debug.Log(string.Format(
			"Finalizado Escaneo: \n"+
			"- AudioSources deshabilitado: {0}",
			_countPlayAwakes
			));
	}
	/// <summary>
	/// Disables the play sounds.
	/// </summary>
	private void DisablePlaySounds()
	{
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		AudioSource objectAudio = null;

		foreach (GameObject obj in allObjects)
		{
			objectAudio = obj.GetComponent<AudioSource>();
			if(objectAudio && objectAudio.playOnAwake)
			{
				objectAudio.playOnAwake = false;
				_countPlayAwakes++;
			}
		}
	}
#endif
}
