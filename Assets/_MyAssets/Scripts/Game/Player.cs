using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : Entity
{

    [SerializeField] private float _delai = 0.5f;

    [SerializeField] private float m_speed;

    
    private float _cadenceFire = -1; // modifier avec l'animation


    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f); // a modifier pour spawn player
    }

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > _cadenceFire)
        {
            Fire();
        }

        Dash();
    }
    
    private void Fire()
    {
        _cadenceFire = Time.time + _delai;
        // Instantiate(_laserPrefab, (transform.position + new Vector3(0f, 0.9f, 0f)), Quaternion.identity); // Changer nom prefab en fonction du nom
    }
    
    private void Movement()
    {
        float positionX = Input.GetAxis("Horizontal");
        float positionY = Input.GetAxis("Vertical");

        Vector3 direction = new(positionX, positionY, 0f);

        transform.Translate(m_speed * Time.fixedDeltaTime * direction.normalized);
    }

    private void Dash()
    {
       //TODO
    }

    private void Rotation()
    {

    }

}
