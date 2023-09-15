using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Este script controla el movimiento del jugador.

    /// <smoothTransition>
    ///smoothTransition verifica si el movimiento "suave" esta activado, 
    ///en caso de ser falso el jugador se mueve instantaneamente a la posicion final de movimiento.
    //transitionSpeed controla la velocidad del movimiento.
    //transitionRotationSpeed controla la velocidad a la que el jugador rota.
    public bool smoothTransition = false;
    public float transitionSpeed = 7f;
    public float transitionRotationSpeed = 700f;
    /// </smoothTransition>
    
    //targeGridPos indica la pocision a la que se movera el jugador
    Vector3 targetGridPos;
    //prevTargetGridPos indica la pocision en la que se encontraba el jugador, este metodo esta fuera de uso por el momento. 
    Vector3 prevTargetGridPos;
    //targetRotation indica la rotacion objetivo del jugador al ingresar el input adecuado
    public Vector3 targetRotation;
    //rotateEnd sirve para agregar al buffer de movimiento el siguiente giro.
    private bool rotateEndQ = false;
    private bool rotateEndE = false;
    public static bool playerIsMoving;
    //movementMultiplier indica la cantidad de espacios que se movera el jugador.
    public  int movementMultiplier;

    /// <Movimientos>
    /// Los movimientos que realizara el jugador, los dos primeros metodos hacen rotar al jugador 90 grados.
    /// los otros metodos lo hacen mover una cantidad de determinada de pasos hacia adelante. 
    public void RotateLeft() { if (AtRest) targetRotation -= Vector3.up * 90; }
    public void RotateRight() { if (AtRest) targetRotation += Vector3.up * 90; }
    public void MoveForward() { if (AtRest) targetGridPos += transform.forward * movementMultiplier; }
    public void MoveBack() { if (AtRest) targetGridPos -= transform.forward * movementMultiplier; }
    public void MoveRight() { if (AtRest) targetGridPos += transform.right * movementMultiplier; }
    public void MoveLeft () { if (AtRest) targetGridPos -= transform.right * movementMultiplier; }
    /// </Movimientos>

    private void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);
    }
    void Update()
    {
        //Buffer de giro, implementacion horrible pero simple, funciona asi que no lo miren mucho.
        if(!AtRest && Input.GetKeyDown(KeyCode.Q))
        {
            rotateEndQ = true;
        }
        else if(!AtRest && Input.GetKeyDown(KeyCode.E))
        {
            rotateEndE = true;
        }
        if(AtRest && rotateEndQ)
        {
            rotateEndQ = false;
            RotateLeft();
        }
        if(AtRest && rotateEndE)
        {
            rotateEndE = false;
            RotateRight();
        }
        //
    }

    

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (true)
        {

            prevTargetGridPos = targetGridPos;

            Vector3 targetPosition = targetGridPos;

            if (targetRotation.y > 270f && targetRotation.y < 361f) targetRotation.y = 0f;
            if (targetRotation.y < 0f) targetRotation.y = 270f;
            
            if (!smoothTransition)
            {
                transform.position=targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);

            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * transitionRotationSpeed);
                
            }

        } else
        {
            //targetGridPos = prevTargetGridPos;
        }
    }
    ///AtRest detecta si el jugador esta detenido, mide la distancia entre la pocision actual del jugador a la posicion objetivo de movimiento.
    //si esta es menor a 0.05f entoces detiene el movimiento y la pocision del jugador se redondea.
    public bool AtRest {
        get {
            if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
                return true;
            else
                return false;
        }
    }
}
