using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
	Add,
	Speed
}
public class PowerUp : MonoBehaviour
{
	private float maxHeight = 0;
	private float cameraWidth;
	private PowerUpType type;


	//public  float minSpeed;

	//public float maxSpedd;

	[SerializeField] private float maxSpeed;
	[SerializeField] private float speed = 1f; //amount of powerup acceleration;
	[SerializeField] private float maxAmplitude = 0.5f; // Set the amplitude of the up-down movement
	[SerializeField] private float maxSpeedY = 1.0f; // Set the speed of the up-down movement
	private float startY; // Store the object's initial Y position

	public Animator pickUp;


	public Rigidbody rb;
	[SerializeField] private PowerUpSpawner powerUpSpawner;
	[SerializeField] private GameObject pickUpEffect;
	[SerializeField] private Color AddPowerUpColor;
	[SerializeField] private Color SpeedPowerUpColor;
	[SerializeField] private GameObject PowerUpMesh;



	// Start is called before the first frame update
	private void Start()
	{

		startY = transform.position.y;
		maxAmplitude = Random.Range(0.5f, maxAmplitude);
		maxSpeedY = Random.Range(1f, maxSpeedY);

		//speed = FCalculateRandomNumbers(minSpeed, maxSpedd);

	}

	private void Awake()
	{
		Camera MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		float height = 2f * MainCamera.orthographicSize;
		//maxHeight =  MainCamera.orthographicSize;
		maxHeight = FCalculateRandomNumbers(3, MainCamera.orthographicSize / 1.5f);
		speed = FCalculateRandomNumbers(1, maxSpeed);
		//maxHeight = MainCamera.orthographicSize/1.5f;
		//maxSpeed = 125f;
		cameraWidth = height * MainCamera.aspect;


	}

	private void FixedUpdate()
	{

		Move();

		CheckPosition();


	}



	protected void Move()
	{
		transform.position += transform.right *  Time.deltaTime * speed;

		float newY = startY + maxAmplitude * Mathf.Sin(speed * Time.time);


		transform.position = new Vector3(transform.position.x, newY, transform.position.z);


	}



	public void OnClick()
	{
		Instantiate(pickUpEffect, gameObject.transform.position, Quaternion.identity);
		powerUpSpawner.DecreasePowerUp(gameObject);
		gameObject.SetActive(false);
		//Destroy(gameObject);
	}
    protected virtual void DoTheThing(Collider2D collision)
	{

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			//pickUp.gameObject.SetActive(true);
			//pickUp.enabled = true;
			//gameObject.GetComponent<Collider2D>().enabled = false;
			DoTheThing(collision);
			gameObject.SetActive(false);
			//Destroy(gameObject, 1f);
		}



	}

	int CalculateRandomNumbers(int rangeStart, int rangeEnd)
	{
		int rnd = Random.Range(rangeStart, rangeEnd);
		return rnd;
	}

	float FCalculateRandomNumbers(float rangeStart, float rangeEnd)
	{
		float rnd = Random.Range(rangeStart, rangeEnd);
		return rnd;
	}



	void CheckPosition()
	{
		if (transform.position.x > cameraWidth + 30f)
		{
			powerUpSpawner.DecreasePowerUp(gameObject);
			gameObject.SetActive(false);
			
		}


	}

	public PowerUpType GetType()
	{
		return type;
	}

	public void Setup()
	{
		if (Random.value > 0.5)
		{
			PowerUpMesh.GetComponent<SpriteRenderer>().color = SpeedPowerUpColor;
			type = PowerUpType.Speed;
		}
		else
		{
			PowerUpMesh.GetComponent<SpriteRenderer>().color = AddPowerUpColor;
			type = PowerUpType.Add;
		}
			

	}



}
