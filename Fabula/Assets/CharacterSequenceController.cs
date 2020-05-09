using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSequenceController : MonoBehaviour
{
    public void processSequence(MovementSequence _sequence){

    }

    private IEnumerator RunSequence(){
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator JumpSequence(){
        yield return new WaitForEndOfFrame();
    }
}

public enum MovementType
{
    run,
    jump
}

public enum NodeType
{
    StartNode,
    MiddleNode,
    EndNode
}
