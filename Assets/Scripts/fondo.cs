using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondo : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB;
    
    

    // Start is called before the first frame update
    private void Start()
    {
        //material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void Update()
    {
        //offset = (jugadorRB.velocity.x*0.1f)* velocidadMovimiento*Time.deltaTime;
        //material.mainTextureOffset += offset;
    }
}
