using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// muc dich tao hieu ung thi giac
/// mat dat di chuyen nhanh nhat
/// may di chuyen nhanh thu 2
/// bau troi di chuyen cham nhat
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] Transform followTarget;

    private Vector2 startingPosition;
    private float startingZ;
    private Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    //update tren moi khung hinh ma khong can dua vao update

    private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
    private float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    
    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    private void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPositon = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPositon.x, newPositon.y, startingZ);
    }
}
