using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class CameraControl : MonoBehaviour
{
    Camera camera;

    [SerializeField] float distMin;
    [SerializeField] float distMax;
    [SerializeField] Vector3 midPoint;


    [SerializeField] GameObject hand;


    public Transform target;
    Transform target2;// O objeto que a câmera irá seguir

    public float smoothSpeed = 0.125f; // A velocidade de suavização do movimento da câmera
    public Vector3 offset; // A distância entre a câmera e o objeto
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        
       if (target.GetComponent<PackageControler>().wtPackage == true)
        {
            target2 = target.GetComponent<PackageControler>().dropedPackege.transform;
            float distance = Vector3.Distance(target.position, target2.position);
            if (distance >= distMin && distance <= distMax)
            {
                LockCam(target, target2);
            }
            else
            {
                FreeCam();
            }
           
        }
        else
        {
            FreeCam();
        }
        
           /*  if (target != null)
              {


                  if (target.GetComponent<HandControl>().dropedHand != null)
                  {
                      float distCam = Vector2.Distance(target.position, target.GetComponent<HandControl>().dropedHand.transform.position);
                      print(distCam);
                      if (distCam > distMin && distCam < distMax)
                      {
                          Vector3 midPoint = (target.position + target.GetComponent<HandControl>().dropedHand.transform.position) / 2f;
                          offset.x = Mathf.Lerp(transform.position.x, midPoint.x, 2f * Time.deltaTime);

                      }
                      else
                      {
                          Vector3 desiredPosition = target.position + offset;
                          Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                          transform.position = smoothedPosition;
                          offset.x = Mathf.Lerp(offset.x,0 , 2f * Time.deltaTime );
                      }


                  }
                  else
                  {
                      Vector3 desiredPosition = target.position + offset;
                      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                      transform.position = smoothedPosition;
                  }
              }
              else
              {
                  return;
              }*/
    
    }

    void FreeCam()
    {
       
        Transform R_target = target;
        Vector3 desiredPosition = R_target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
    void LockCam( Transform target1, Transform target2)
    {

        // Calcula a distância entre os dois alvos
        float distance = Vector3.Distance(target1.position, target2.position);
        // Calcula o ponto médio entre os dois alvos
        midPoint = (target.position + target2.position) / 2f;

        // Move a câmera para o ponto médio com suavização
        Vector2 desiredPosition = midPoint + offset;
        Vector2 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        // Se a distância estiver entre minDistance e maxDistance
       
    }
}
