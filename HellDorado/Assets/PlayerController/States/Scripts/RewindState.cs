using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Player/States/Rewind")]
public class RewindState : State {
	// -------------- Vi behöver inget här. All Rewind görs ändå i Ability Managern. Det här statet finns bara för att vi inte ska kunna göra nåt annat när vi rewindar. --------------------------- //

//    public bool rewinding;

	private SkinnedMeshRenderer[] originalSkinnedMeshRend;
	private Color[] orgColorSkinnedMeshRend;
	private Renderer[] originalMeshRend;
	private Color[] orgColorMeshRend;
    
    private PlayerController _controller;
//    private AbilityManager am;

    public override void Initialize(Controller owner) {
        _controller = (PlayerController)owner;
        
    }

	public override void Enter(){
		
		originalSkinnedMeshRend = _controller.gameObject.GetComponentsInChildren<SkinnedMeshRenderer> ();

		foreach (SkinnedMeshRenderer rend in originalSkinnedMeshRend) {

			int i = 0;
			Material[] materials = rend.materials;
			orgColorSkinnedMeshRend = new Color[materials.Length];
			foreach (Material mat in materials) {
				orgColorSkinnedMeshRend [i] = mat.color;
				Color temp = mat.color;
				temp.a = 0.5f;
				mat.color = temp;
				i++;
			}
		}

		originalMeshRend = _controller.gameObject.GetComponentsInChildren<Renderer> ();

		foreach (Renderer rend in originalMeshRend) {
			rend.material.renderQueue = 3000;
			int i = 0;
			Material[] materials = rend.materials;
			orgColorMeshRend = new Color[materials.Length];
			foreach (Material mat in materials) {
				orgColorMeshRend [i] = mat.color;
				Color temp = mat.color;
				temp.a = 0.5f;
				mat.color = temp;
				i++;
			}
		}

		Physics.IgnoreLayerCollision (0, 9, true);
//		Debug.Log ("Rewind State");
//        _controller.GetComponent<AbilityManager>().StartRewind ();
	}
	
	// Update is called once per frame
	public override void Update () {
//        rewinding = _controller.GetComponent<AbilityManager>().isRewinding; 
//
//		if (!rewinding) {
//			Debug.Log ("stop rewind");
//			_controller.TransitionTo<GroundState> ();
//		} else if (Input.GetAxisRaw ("Fire2") == 0) {
//			_controller.GetComponent<AbilityManager> ().StopRewind ();
//			_controller.TransitionTo<GroundState> ();
//		}
	}

	public override void Exit(){
		foreach (SkinnedMeshRenderer rend in originalSkinnedMeshRend) {
			rend.material.renderQueue = 2000;
			int i = 0;
			Material[] materials = rend.materials;
			foreach (Material mat in materials) {
//				Color temp = mat.color;
//				temp.a = 1f;
//				mat.color = temp;
				mat.color = orgColorSkinnedMeshRend[i];
				i++;
			}

		}

		foreach (Renderer rend in originalMeshRend) {
			rend.material.renderQueue = 2000;
			int i = 0;
			Material[] materials = rend.materials;
			foreach (Material mat in materials) {
				//				Color temp = mat.color;
				//				temp.a = 1f;
				//				mat.color = temp;
				mat.color = orgColorMeshRend[i];
				i++;
			}
		}


		Physics.IgnoreLayerCollision (0, 9, false);
	}
}
