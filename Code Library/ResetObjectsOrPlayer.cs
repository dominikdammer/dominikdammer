/// <copyright>(c) Dominik Dammer 2022.</copyright>
/// <author>Dominik Dammer</author>

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//save the transform, rotation and scale of gameobjects at start and resets them to default if needed
public class ResetObjectOrPlayer : MonoBehaviour
{
	public GameObject sound;
	public GameObject soundOff;
	int value;

	Vector3[] defaultNormalBoxPos;
	Vector3[] defaultNormalBoxScale;
	Quaternion[] defaultNormalBoxRot;
	Vector3[] defaultAtomBoxPos;
	Vector3[] defaultAtomBoxScale;
	Quaternion[] defaultAtomBoxRot;

	Transform[] nModels;
	Transform[] aModels;

	void Start()
	{
		//Call to back up the Transform before moving, scaling or rotating the GameObject
		backUpTransform();
		Time.timeScale = 1f;
	}

	void backUpTransform()
	{
		//Find all GameObjects
		GameObject[] normalBoxModels = GameObject.FindGameObjectsWithTag("Box");
		GameObject[] atomBoxModels = GameObject.FindGameObjectsWithTag("Atom");

		//Resources.FindObjectsOfTypeAll<GameObject>()

		//Create pos, scale and rot, Transform array size based on sixe of Objects found
		defaultNormalBoxPos = new Vector3[normalBoxModels.Length];
		defaultNormalBoxScale = new Vector3[normalBoxModels.Length];
		defaultNormalBoxRot = new Quaternion[normalBoxModels.Length];
		defaultAtomBoxPos = new Vector3[atomBoxModels.Length];
		defaultAtomBoxScale = new Vector3[atomBoxModels.Length];
		defaultAtomBoxRot = new Quaternion[atomBoxModels.Length];

		nModels = new Transform[normalBoxModels.Length];
		aModels = new Transform[atomBoxModels.Length];

		//Get original the pos, scale and rot of each Object on the transform
		for (int i = 0; i < normalBoxModels.Length; i++)
		{
			nModels[i] = normalBoxModels[i].GetComponent<Transform>();

			defaultNormalBoxPos[i] = nModels[i].position;
			defaultNormalBoxScale[i] = nModels[i].localScale;
			defaultNormalBoxRot[i] = nModels[i].rotation;
		}
		for (int i = 0; i < atomBoxModels.Length; i++)
		{
			aModels[i] = atomBoxModels[i].GetComponent<Transform>();

			defaultAtomBoxPos[i] = aModels[i].position;
			defaultAtomBoxScale[i] = aModels[i].localScale;
			defaultAtomBoxRot[i] = aModels[i].rotation;
		}
	}

	public void ResetSceneObjects()
	{
		//Restore the all the original pos, scale and rot of each GameObject
		for (int i = 0; i < nModels.Length; i++)
		{
			nModels[i].position = defaultNormalBoxPos[i];
			nModels[i].localScale = defaultNormalBoxScale[i];
			nModels[i].rotation = defaultNormalBoxRot[i];
		}
		for (int i = 0; i < aModels.Length; i++)
		{
			aModels[i].position = defaultAtomBoxPos[i];
			aModels[i].localScale = defaultAtomBoxScale[i];
			aModels[i].rotation = defaultAtomBoxRot[i];
		}
	}


    public void ResetScene ()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
