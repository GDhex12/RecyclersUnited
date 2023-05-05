using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private Vector3 offset;
    public void OnClick()
    {
        Instantiate(effect, new Vector3(gameObject.transform.position.x + offset.x, 
            gameObject.transform.position.y + offset.y,
            gameObject.transform.position.z + offset.z),
            Quaternion.identity);
    }
}
