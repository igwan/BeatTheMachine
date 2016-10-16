using UnityEngine;
using System.Collections;

public class ProximityScanner : MonoBehaviour
{
    float tileLength;

    void Awake()
    {
        tileLength = MapManager.Instance.tileLength;
    }


    bool SomethingInDirection(Vector2 direction, int howManyTile = 1, Vector2? possibleOffset = null)
    {
        Vector2 offset;
        if(possibleOffset.HasValue)
            offset = possibleOffset.Value;
        else
            offset = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(
                (Vector2)transform.position + (offset * tileLength),
                direction,
                (direction * tileLength).magnitude * howManyTile
        );
        return hit.collider != null;
    }

    public bool SomethingFarDown()
    {
        return SomethingInDirection(new Vector2(1, -1), 1, Vector2.right);
    }

    public bool SomethingFarFront()
    {
        return SomethingInDirection(Vector2.right, 2);
    }

    public bool SomethingUp()
    {
        return SomethingInDirection(Vector2.up);
    }

    public bool SomethingUpFront()
    {
        return SomethingInDirection(Vector2.one);
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
