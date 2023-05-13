using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public enum UpdateMode
    {
        Update, FixedUpdate, LateUpdate
    }
    public UpdateMode updateMode;
    public Transform target;
    public Vector3 initialOffset;
    public float speed = .5f;
    private Vector3 additionalOffset = Vector3.zero;

    private void FixedUpdate()
    {
        if (target == null) return;
        if (updateMode != UpdateMode.FixedUpdate) return;
        Follow();
    }

    private void Update()
    {
        if (target == null) return;
        if (updateMode != UpdateMode.Update) return;
        Follow();
    }

    void LateUpdate()
    {
        if (target == null) return;
        if (updateMode != UpdateMode.LateUpdate) return;
        Follow();
    }

    private void Follow()
    {
        // Get the direction vector from the current object to the target.
        Vector3 direction = (target.position + initialOffset + additionalOffset) - transform.position;

        // Move the object in the direction vector, at the specified speed.
        transform.position += direction * speed * Time.deltaTime;
    }

    public void InitializeAdditionalOffset(Vector3 additionalOffset)
    {
        this.additionalOffset = additionalOffset;
    }
}
