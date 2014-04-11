using UnityEngine;
using System.Collections;

public class KeyPair {

    public object m_obj1, m_obj2;

    public KeyPair(object obj1, object obj2)
    {
        m_obj1 = obj1;
        m_obj2 = obj2;
    }

    //Returns true if the corresponding value in both objects are equal
    public bool Compare(KeyPair compareObject)
    {
        return this.m_obj1.Equals(compareObject.m_obj1) 
            && this.m_obj2.Equals(compareObject.m_obj2);
    }
}
