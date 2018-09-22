using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class DataCtrl : MonoBehaviour {
	public GameData data;
	string dataFilePath;
	BinaryFormatter bf;

	public static DataCtrl instance=null;
	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} 
		else {
			Destroy (gameObject);
		}
		bf = new BinaryFormatter ();
		dataFilePath=Application.persistentDataPath + "/game.bat";
	}
	public void SaveData(){
		FileStream fs = new FileStream (dataFilePath , FileMode.Create);
		bf.Serialize (fs,data);
		fs.Close ();
	}
	public void RefreshData(){
		if (File.Exists (dataFilePath)) {
			FileStream fs = new FileStream (dataFilePath,FileMode.Open);
			data = (GameData)bf.Deserialize (fs);
			fs.Close ();
		}
	}

	void OnEnable(){
		CheckDB ();
	}

	public void CheckDB(){
		if (!File.Exists (dataFilePath)) {
			#if UNITY_ANDROID
			string srcFile = System.IO.Path.Combine (Application.streamingAssetsPath, "game.bat");
			WWW downloader = new WWW (srcFile);
			while (!downloader.isDone) {
			}
			File.WriteAllBytes (dataFilePath, downloader.bytes);
			RefreshData ();
			#endif
		} else {
			RefreshData ();
		}
	}
}
