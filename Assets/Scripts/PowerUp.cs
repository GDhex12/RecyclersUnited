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

	[SerializeField] private float maxSpeed;
	[SerializeField] private float speed = 1f; //amount of powerup acceleration;
	[SerializeField] private float maxAmplitude = 0.5f; // Set the amplitude of the up-down movement
	[SerializeField] private float maxSpeedY = 1.0f; // Set the speed of the up-down movement
	private float startY; // Store the object's initial Y position

	[SerializeField] float flyDuration = 40f;




    [SerializeField] private Rigidbody rb;
	[SerializeField] private PowerUpSpawner powerUpSpawner;
	[SerializeField] private GameObject pickUpEffect;
	[SerializeField] private Material AddPowerUpColor;
	[SerializeField] private Material SpeedPowerUpColor;
	[SerializeField] private GameObject PowerUpMesh;
	private bool isActive = false;

	Vector3 cameraPosition;

    // Start is called before the first frame update
    private void Start()
	{
		startY = transform.position.y;
		maxAmplitude = Random.Range(0.5f, maxAmplitude);
		maxSpeedY = Random.Range(1f, maxSpeedY);
	}

	private void Awake()
	{
        cameraPosition = Camera.main.transform.position;
        Camera MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		float height = 2f * MainCamera.orthographicSize;
		maxHeight = FCalculateRandomNumbers(3, MainCamera.orthographicSize / 1.5f);
		speed = FCalculateRandomNumbers(1, maxSpeed);
		cameraWidth = height * MainCamera.aspect;
	}

	private void FixedUpdate()
	{
		Move();
        LookAtCamera();
     

    }



	protected void Move()
	{
		transform.position += speed * Time.deltaTime * transform.right;
		float newY = startY + maxAmplitude * Mathf.Sin(speed * Time.time);
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);

     
    }

	private void LookAtCamera()
	{
        Vector3 directionToCamera = cameraPosition - transform.position;

        // Ignore the Y component of the direction
        directionToCamera.y = 0;

        // Calculate the rotation needed to face the camera
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Calculate the angle between the power up's face and the camera's point of view
        float angleToCamera = Vector3.Angle(transform.forward, directionToCamera);

        // If the angle is less than the facing angle, rotate the power up towards the camera
        if (angleToCamera < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }



	public void OnClick()
	{
		if (isActive)
		{
			isActive = false;
		}
		UnlockPowerupAchievements();

        Instantiate(pickUpEffect, gameObject.transform.position, Quaternion.identity);
		//powerUpSpawner.DecreasePowerUp(gameObject);
		
		PowerUpMesh.GetComponent<PowerUpPickUp>().PickUp();

	}
   
	private void UnlockPowerupAchievements()
	{
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_1);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_2);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_3);

    }


	float FCalculateRandomNumbers(float rangeStart, float rangeEnd)
	{
		float rnd = Random.Range(rangeStart, rangeEnd);
		return rnd;
	}



	

	private void DisablePowerUp()
	{
		isActive = false;
        powerUpSpawner.DecreasePowerUp(gameObject);
        gameObject.SetActive(false);
    }

	public new PowerUpType GetType()
	{
		return type;
	}

	public void Setup(int direction)
	{
		FunctionTimer.Create(DisablePowerUp, flyDuration);
		if(direction == 0)
		{
			speed = speed > 0 ? speed : speed * -1;

		}
		else
		{
            speed = speed > 0 ? speed * -1 : speed;
        }

		if (Random.value > 0.5)
		{
			PowerUpMesh.GetComponent<MeshRenderer>().material = SpeedPowerUpColor;
			type = PowerUpType.Speed;
		}
		else
		{
			PowerUpMesh.GetComponent<MeshRenderer>().material = AddPowerUpColor;
			type = PowerUpType.Add;
		}

		isActive = true;
	}

	public bool IsPowerUpActive()
	{
		return isActive;
	}

}
