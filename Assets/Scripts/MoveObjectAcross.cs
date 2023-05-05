using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MoveObjectAcross : MonoBehaviour
{
    [SerializeField] private float speedTo = 1f;
    [SerializeField] private float speedfrom = 0.5f;
    [SerializeField] private MoveDirection moveDirection = MoveDirection.Right;
    private float speed = 1;

    private enum MoveDirection
    {
        Right,
        Left
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        switch(moveDirection)
        {
            default:
            case MoveDirection.Left:
                transform.position += speed * Time.deltaTime * (transform.right * -1);
                break;
            case MoveDirection.Right:
                transform.position += speed * Time.deltaTime * transform.right;
                break;
        }
       
    }
    private void OnEnable()
    {
        speed = Random.Range(speedfrom, speedTo);
        gameObject.SetActive(true);
        StartCoroutine(DespawnAfter(20f));
    }
    IEnumerator DespawnAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
