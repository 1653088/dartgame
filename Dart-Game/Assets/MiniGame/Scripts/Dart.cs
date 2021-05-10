using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    Rigidbody rb;
    public static float xCollide =0;
    public static float yCollide =0;
    public static bool _isCollideDartBoard = false;

    public bool IsCollideDartBoard
    {
        get { return _isCollideDartBoard; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _isCollideDartBoard = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8 && !_isCollideDartBoard)
        {
            _isCollideDartBoard = true;
            gameObject.GetComponent<Collider>().enabled = false;
            rb.velocity = transform.forward * 0;
            xCollide = transform.position.x;
            yCollide = transform.position.y;
            rb.isKinematic = true;
        }
    }
}
