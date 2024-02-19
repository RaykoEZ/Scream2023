using UnityEngine;

public class DraggableHanger : DraggableObject 
{
    protected override Transform OnDragParent => transform.parent;
}

