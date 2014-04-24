/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Template class to represent a pair of objects.
 */

using UnityEngine;
using System.Collections;

public class KeyPair {

    public object m_obj1, m_obj2;

    bool sortBySecondElement;

    public KeyPair(object obj1, object obj2, bool sortBySecondElement=false)
    {
        m_obj1 = obj1;
        m_obj2 = obj2;

        this.sortBySecondElement = sortBySecondElement;
    }

    //Returns true if the corresponding value in both objects are equal
    public bool Compare(KeyPair compareObject)
    {
        return this.m_obj1.Equals(compareObject.m_obj1) 
            && this.m_obj2.Equals(compareObject.m_obj2);
    }


    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }

        if ((KeyPair)other == null)
        {
            return false;
        }

        return Compare((KeyPair)other);
    }

}
