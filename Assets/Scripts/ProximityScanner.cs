using UnityEngine;
using System.Collections;

public class ProximityScanner : MonoBehaviour
{
    float tileLength;

    void Awake()
    {
        tileLength = MapManager.Instance.tileLength;
    }

    bool SomethingInDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, (direction * tileLength).magnitude);
        return hit.collider != null;
    }

    public bool SomethingUp()
    {
        return SomethingInDirection(Vector2.up * tileLength);
    }

    public bool SomethingUpFront()
    {
        return SomethingInDirection(Vector2.one * tileLength);
    }

    public bool SomethingFront()
    {
        return SomethingInDirection(Vector2.right * tileLength);
    }

    public bool SomethingDownFront()
    {
        return SomethingInDirection(Vector2.down * tileLength);
    }

    [ContextMenu("DebugSomethingUp")]
    void DebugSomethingUp()
    {
        Debug.Log(SomethingUp());
    }

    [ContextMenu("DebugSomethingUpFront")]
    void DebugSomethingUpFront()
    {
        Debug.Log(SomethingUpFront());
    }

    [ContextMenu("DebugSomethingFront")]
    void DebugSomethingFront()
    {
        Debug.Log(SomethingFront());
    }

    [ContextMenu("DebugSomethingDownFront")]
    void DebugSomethingDownFront()
    {
        Debug.Log(SomethingDownFront());
    }
}
