using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
	public AudioClip[] audioClips;
	private List<AudioSource> sources = new List<AudioSource>();
	private List<int> removeList = new List<int>();

	void Update(){
		for(int i = 0; i < sources.Count; i++){
			if (!sources[i].isPlaying) {
				removeList.Add (i);
				print ("remove");
			}
		}
		foreach (int i in removeList) {
			Component.Destroy(sources[i]);
			sources.Remove (sources [i]);
		}
		removeList.Clear ();
	}

	public void PlayAudioClip(int index){
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		sources.Add (newSource);
		newSource.PlayOneShot (audioClips [index]);
	}

	public void PlayRandomAudioClip(){
		PlayAudioClip (Random.Range (0, audioClips.Length));
	}
}
