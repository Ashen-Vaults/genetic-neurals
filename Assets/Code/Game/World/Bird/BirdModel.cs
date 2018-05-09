using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class BirdModel
{
	public  Rigidbody2D rigidBody;

	public Action onControl;

	public Action onDeath;

	public float tiltSmooth = 5f;

	public float force;

	public float score;

	public Coroutine scoreRoutine;
	public BirdModel()
	{

		
	}

	public IEnumerator UpdateScore()
	{
		score += 100;
		yield return new WaitForSeconds(0.1f);
	}

	public void Death()
	{
		if(onDeath != null)
			onDeath();

	}
	


}
