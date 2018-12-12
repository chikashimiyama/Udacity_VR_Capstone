using UnityEngine;
using Magicolo;

public class PureDataPatch : MonoBehaviour{

	void Start ()
	{
		PureData.OpenPatch("main.pd");
	}

	public void OnNoteChanged(bool val)
	{
		Debug.Log("pressing");
		var value = val ? 1.0f : 0.0f;
		PureData.Send<float>("note", value);
	}
	
}
