/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * A base class for movable objects (bridges that can open etc)
 */

using UnityEngine;
using System.Collections;

public class TriggedObject : MonoBehaviour {

    protected bool isActive = false;
    public bool m_isActive
    {
        get { return isActive; }
    }

    protected virtual void Start(){}
    protected virtual void Update(){}
    protected virtual void ActivateObject(){}
    protected virtual void DeactivateObject(){}

    public void Activate() 
    {
        isActive = true;

        ActivateObject();
    }

    public void Deactivate() 
    {
        isActive = false;

        DeactivateObject();
    }

}
