using System.Collections;
using UnityEngine;

public class InvaderLogic : MonoBehaviour
{
    public float moveSpeed;
    private bool isChasedAway;
    public static float interval = 0.3f;
    private static float SMALL_CONSTANT = 0.3f;
    private ChickenInvaderManager manager;
    private Transform goal;

    public void Move(Vector3 direction, float scale) // Should be called per frame
    {
        // Ensure that chicken cannot "go" into the ground
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        direction *= moveSpeed * Time.deltaTime * SMALL_CONSTANT * scale;
        transform.position += direction;
    }

    public IEnumerator AttackTarget(Transform target, ChickenInvaderManager man)
    {
        goal = target;
        isChasedAway = false;
        Debug.Log("Invader attacking!");
        manager = man;
        while (!isChasedAway && !manager.isGameEnded)
        {
            MoveTo(moveSpeed/10);
            yield return new WaitForSeconds(interval);
        }
    }

    public void MoveAway()
    {
        //Vector3 heightlessPosition = new Vector3(target.position.x, 0, target.position.z);
        Vector3 direction = transform.position - goal.position;
        Move(direction, 2 * moveSpeed);
    }

    public void MoveTo(float speed)
    {
        //Vector3 heightlessPosition = new Vector3(target.position.x, 0, target.position.z);
        Vector3 direction = goal.position - transform.position;
        Move(direction, speed);
    }
}
