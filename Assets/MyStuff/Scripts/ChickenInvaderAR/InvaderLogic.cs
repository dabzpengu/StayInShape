using System.Collections;
using UnityEngine;

public class InvaderLogic : MonoBehaviour
{
    public float moveSpeed;
    private bool isChasedAway;
    public static float interval = 0.3f;
    private static float SMALL_CONSTANT = 0.3f;
    private ChickenInvaderManager manager;

    public void Move(Vector3 direction, int scale) // Should be called per frame
    {
        // Ensure that chicken cannot "go" into the ground
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        direction *= moveSpeed * Time.deltaTime * SMALL_CONSTANT * scale;
        transform.position += direction;
    }

    public IEnumerator AttackTarget(Transform target, ChickenInvaderManager man)
    {
        isChasedAway = false;
        Debug.Log("Invader attacking!");
        manager = man;
        while (!isChasedAway && !manager.isGameEnded)
        {
            MoveTowardsTarget(target);
            yield return new WaitForSeconds(interval);
        }
    }

    public void MoveTowardsTarget(Transform target)
    {
        //Vector3 heightlessPosition = new Vector3(target.position.x, 0, target.position.z);
        Vector3 direction = target.position - transform.position;
        Move(direction, 1);
    }
}
