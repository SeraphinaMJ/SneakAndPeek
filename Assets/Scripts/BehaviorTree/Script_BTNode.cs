using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Script_BTNode
{
    protected BT_NodeStates currentNodeState;

    public BT_NodeStates nodeState
    {
        get { return currentNodeState; }
    }


    public Script_BTNode() { }

    public abstract BT_NodeStates Evaluate();
}
