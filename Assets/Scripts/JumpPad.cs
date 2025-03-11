using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce;

    private void OnCollisionEnter(Collision collision)
    {
        {
            

            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);

                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            }
        }
    }
}
