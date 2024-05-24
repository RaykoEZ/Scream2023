using UnityEngine;
using UnityEngine.UI;

public class SnapshotWatch : DraggableObject 
{
    protected override Transform OnDragParent => transform.parent;

}
