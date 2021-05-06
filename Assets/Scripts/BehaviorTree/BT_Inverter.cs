using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Inverter : Script_BTNode
{

    protected Script_BTNode m_Node;

    public BT_Inverter(Script_BTNode node)
    {
        m_Node = node;
    }

    public override BT_NodeStates Evaluate()
    {

        switch (m_Node.Evaluate())
        {
            case BT_NodeStates.RUNNING:
                currentNodeState = BT_NodeStates.RUNNING;
                return currentNodeState;

            case BT_NodeStates.SUCESS:
                currentNodeState = BT_NodeStates.FAILURE;
                return currentNodeState;

            case BT_NodeStates.FAILURE:
                currentNodeState = BT_NodeStates.SUCESS;
                return currentNodeState;

            default:
                break;
        }

        return currentNodeState;
    }
}
