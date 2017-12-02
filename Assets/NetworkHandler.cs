﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHandler : MonoBehaviour {

	public string serverURL = "http://localhost:61586/api";

	void Start()
	{
		StartCoroutine(GetText());
	}

	IEnumerator GetText()
	{
		using (UnityWebRequest www = UnityWebRequest.Get(serverURL + "/property/two"))
		{
			yield return www.Send();

			// Show results as text
			Debug.Log(www.downloadHandler.text);

			// Or retrieve results as binary data
			byte[] results = www.downloadHandler.data;
		}
	}
}
