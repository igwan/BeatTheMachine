using UnityEngine;
using System.Collections;

public class ProximityScanner : MonoBehaviour
{
    float tileLength;

    void Awake()
    {
        tileLength = MapManager.Instance.tileLength;
    }

    bool SomethingInDirection(Vector2 direction, float howManyTile = 1f, Vector2? possibleOffset = null)
    {
        Vector2 rayStart = transform.position;
        if(possibleOffset.HasValue)
        {
            var offset = possibleOffset.Value;
            rayStart = (Vector2)transform.position + (offset * tileLength);
            #if DEBUG
                Debug.DrawLine(transform.position, rayStart, Color.cyan, 1);
            #endif
        }

        var distance = (direction * tileLength).magnitude * howManyTile;
        #if DEBUG
            Debug.DrawRay(rayStart, direction * distance, Color.red, 1);
        #endif
        RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, distance);
        return hit.collider != null;
    }

    public bool SomethingFarDown()
    {
        return SomethingInDirection(new Vector2(1, -1), 1, Vector2.right);
    }

    public bool SomethingFarFront()
    {
        return SomethingInDirection(Vector2.right, 1, Vector2.right);
    }

    public bool SomethingUp()
    {
        return SomethingInDirection(Vector2.up);
    }

    public bool SomethingUpFront()
    {
        return SomethingInDirection(Vector2.one, 0.5f, Vector2.one * 0.5f);
    }

    public bool SomethingFront()
    {
        return SomethingInDirection(Vector2.right);
    }

    public bool SomethingDown()
    {
        return SomethingInDirection(Vector2.down);
    }

    public bool SomethingFrontDown()
    {
        return SomethingInDirection(new Vector2(1, -1));
    }

    [ContextMenu("DebugSomethingFarDown")]
    void DebugSomethingFarDown()
    {
        Debug.Log(SomethingFarDown());
    }

    [ContextMenu("DebugSomethingFarFront")]
    void DebugSomethingFarFront()
    {
        Debug.Log(SomethingFarFront());
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

    [ContextMenu("DebugSomethingDown")]
    void DebugSomethingDown()
    {
        Debug.Log(SomethingDown());
    }

    [ContextMenu("DebugSomethingFrontDown")]
    void DebugSomethingFrontDown()
    {
        Debug.Log(SomethingFrontDown());
    }
}
