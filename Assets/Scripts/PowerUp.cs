using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	private float maxHeight = 0;
	private float cameraWidth;
	//public  float minSpeed;

	//public float maxSpedd;

	public float maxSpeed;
	public float speed = 1f; //amount of powerup acceleration;

	public Animator pickUp;

	private bool up = true;
	private bool down = false;
	public Rigidbody rb;




	// Start is called before the first frame update
	private void Start()
	{



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
		//rb.AddForce(transform.right * maxSpeed);

	}


	protected void Rotate()
	{
		if (up)
		{

			if (transform.position.y >= maxHeight)
			{
				speed -= 10f;
				if (speed <= 0)
				{
					up = false;
					down = true;
				}


			}

			else if (speed < maxSpeed)
			{
				speed += 40f;
			}
			GoUp();

		}
		else if (down)
		{

			if (transform.position.y <= -maxHeight)
			{
				speed -= 10f;
				if (speed <= 0)
				{
					up = true;
					down = false;
				}


			}

			else if (speed < maxSpeed)
			{
				speed += 40f;
			}
			GoDown();

		}


	}

	void GoUp()
	{
		rb.AddForce(transform.up * speed);
	}



	void GoDown()
	{
		rb.AddForce(-transform.up * speed);
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

			Destroy(gameObject, 1f);
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
		//if (transform.position.x < player.position.x - cameraWidth / 2f)
		//{
		//	Destroy(gameObject);
		//}


	}



}
