using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Sequence : Script_BTNode
{
    private List<Script_BTNode> m_Nodes = new List<Script_BTNode>();

    public BT_Sequence(List<Script_BTNode>nodes)
    {
        m_Nodes = nodes;
    }

    public override BT_NodeStates Evaluate()
    {
        bool isChildRunning = false;

        foreach(Script_BTNode node in m_Nodes)
        {
            switch (node.Evaluate())
            {
                case BT_NodeStates.RUNNING:
                    isChildRunning = true;
                    break;

                case BT_NodeStates.SUCESS:
                    break;

                case BT_NodeStates.FAILURE:
                    currentNodeState = BT_NodeStates.FAILURE;
                    return currentNodeState;

                default:
                    break;
            }
        }
        currentNodeState = isChildRunning ? BT_NodeStates.RUNNING : BT_NodeStates.SUCESS;
        return currentNodeState;
    }
}
