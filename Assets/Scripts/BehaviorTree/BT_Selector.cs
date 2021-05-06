using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Selector : Script_BTNode
{

    protected List<Script_BTNode> m_Nodes = new List<Script_BTNode>();

    public BT_Selector(List<Script_BTNode> nodes)
    {
        m_Nodes = nodes;
    }

    public override BT_NodeStates Evaluate()
    {
        foreach (Script_BTNode node in m_Nodes)
        {
            switch (node.Evaluate())
            {
                case BT_NodeStates.RUNNING:
                    currentNodeState = BT_NodeStates.RUNNING;
                    return currentNodeState;

                case BT_NodeStates.SUCESS:
                    currentNodeState = BT_NodeStates.SUCESS;
                    return currentNodeState;

                case BT_NodeStates.FAILURE:
                    break;

                default:
                    break;
            }
        }

        currentNodeState = BT_NodeStates.FAILURE;
        return currentNodeState;
    }
}
