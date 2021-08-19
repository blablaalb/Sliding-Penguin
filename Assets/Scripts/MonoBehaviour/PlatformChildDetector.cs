using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System.Linq;
using UnityEditor;

public class PlatformChildDetector : MonoBehaviour
{
    [SerializeField]
    private Vector3 _position;
    [SerializeField]
    private Transform _positionTransform;
    [SerializeField]
    private Vector3 _halfExtend;

    // delete me
    private List<PlatformChild> childs;

    public bool PositionOccupied(out PlatformChild[] children)
    {
        bool occupied = false;
        children = null;
        _position = _positionTransform.position;

        List<PlatformChild> occupyingPlatofrmChildren = new List<PlatformChild>();
        childs = occupyingPlatofrmChildren;

        Collider[] overlapColliders = Physics.OverlapBox(_position, _halfExtend);

        if (overlapColliders.Length > 0)
        {
            foreach (Collider col in overlapColliders)
            {
                //Debug.Log(col);

                if (col.gameObject.GetComponent<PlatformChild>() is PlatformChild occupyingChild)
                {
                    occupyingPlatofrmChildren.Add(occupyingChild);
                    occupied = true;
                }
            }
        }
        children = occupyingPlatofrmChildren.ToArray();
        return occupied;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_position, _halfExtend * 2);

        if (childs != null)
        {
            foreach (var pc in childs)
            {
                Gizmos.DrawWireCube(pc.GetBounds().center, pc.GetBounds().size);
            }
        }
    }
}

