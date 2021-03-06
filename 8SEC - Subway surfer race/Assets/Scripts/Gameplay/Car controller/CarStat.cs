﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStat : MonoBehaviour
{

	public Rigidbody RB;
	CarController carController;
	public bool IsDead;
	public GameObject Car;
	public GameOver GameOver;
	public Score score;
	//public Collider collider;
	public CarParticleEmmiter CarParticleEmmiter;
	public ParticlePlayer Explosion;
	public AudioSource EngineSource;
	public CarCinematiqueMovement carCinematiqueMovement;
	GameManager GM;
	public void Start()
	{
		//RB = GetComponent<Rigidbody>();
		GM = GameManager.Instance();
		carController = GetComponent<CarController>();
		carCinematiqueMovement.CallTransitionIn();
	}

	public void SetCarInvicible()
	{
		
	}
	public void InitCar()
	{
		score.Multiplicator = 1;
		carController.ResetPosition();
		EnableCar();
	}

	public void EnableCar()
	{
		RB.useGravity = true;
		IsDead = false;
		Car.SetActive(true);
		//collider.enabled = true;
	}

	public void DiseableCar()
	{
		RB.useGravity = false;
		IsDead = true;
		Car.SetActive(false);
	//	collider.enabled = false;
		RB.velocity = Vector3.zero;

	}

	public void Respawn()
	{
		InitCar();
		GM.SetPause(false);
		carController.LaneID = 2;
		//EngineSource.Play();
	}

	public IEnumerator PlayerGameOver()
	{
		GM.SetPause(true);
		KillCar();
		yield return StartCoroutine(PlayExplosion());
		EnableEndGameScreen();
	}
	
	public IEnumerator PlayExplosion()
	{
		ParticlePlayer emmiter = CarParticleEmmiter.InstantiateExplosionFX();
		yield return StartCoroutine(emmiter.PlayOneShoot());
	}

	public void CallKillCar()
	{
		CarParticleEmmiter.StopAllCoroutines();
		StartCoroutine(PlayerGameOver());
	}

	public void KillCar()
	{
		score.Multiplicator = 0;
		DiseableCar();
		//EngineSource.Stop();
	}

	public void EnableEndGameScreen()
	{
		GameOver.EnableGameOver();
	}
}
