using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopableView : MonoBehaviour
{
    public abstract void Pop();
    public abstract Vector3 GetPopCenterPoint();
}
