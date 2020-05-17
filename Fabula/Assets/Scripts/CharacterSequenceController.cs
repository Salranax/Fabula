using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SequenceCallback();

public class CharacterSequenceController : MonoBehaviour
{
    public GameObject character;

    protected int playingNodeIndex = 0;
    protected float runSpeed = 3f;
    protected float jumpStartSpeed = 2.5f;
    protected float jumpMaxSpeed = 3.5f;
    protected float jumpSpeedIncrease = 1f;
    protected int zoneNumber = 0;


    public void setCharacterToZone(GridZone _zone, SequenceNode _startNode){
        character.transform.rotation = _zone.transform.rotation;
        character.transform.position = _startNode.transform.position;
        zoneNumber = _zone.zoneIndex;
    }

    public void processSequence(MovementSequence _sequence, SequenceCallback _completionCallback){
        playingNodeIndex = 0;
        if(_sequence.sequences.Length > 0){
            if(_sequence.sequences[playingNodeIndex].movementType == MovementType.run){
                StartCoroutine(RunSequence(_sequence, _sequence.sequences[playingNodeIndex], _completionCallback));
            }
            else if(_sequence.sequences[playingNodeIndex].movementType == MovementType.jump){
                StartCoroutine(JumpSequence(_sequence, _sequence.sequences[playingNodeIndex], _completionCallback));
            }
        }
    }

    private void moveNextNode(MovementSequence _sequence, SequenceCallback _completionCallback){
        playingNodeIndex ++;
        if(_sequence.sequences.Length > playingNodeIndex){
            if(_sequence.sequences[playingNodeIndex].movementType == MovementType.run){
                StartCoroutine(RunSequence(_sequence, _sequence.sequences[playingNodeIndex], _completionCallback));
            }
            else if(_sequence.sequences[playingNodeIndex].movementType == MovementType.jump){
                StartCoroutine(JumpSequence(_sequence, _sequence.sequences[playingNodeIndex], _completionCallback));
            }
        }
        else{
            endSequence(_completionCallback);
        }
    }

    private void endSequence(SequenceCallback _completionCallback){
        _completionCallback();
    }

    private IEnumerator RunSequence(MovementSequence _sequence, SequenceNode _node, SequenceCallback _completionCallback){
        
        while (Vector2.Distance(character.transform.position, _node.transform.position) > 0.1f)
        {
            character.transform.position = Vector2.MoveTowards(character.transform.position, _node.transform.position, runSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        character.transform.position = _node.transform.position;

        moveNextNode(_sequence, _completionCallback);

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator JumpSequence(MovementSequence _sequence, SequenceNode _node, SequenceCallback _completionCallback){
        float _jumpSpeed = jumpStartSpeed;
        Vector2 _peak;
        if(zoneNumber == 0){
            _peak = new Vector2(
                (character.transform.position.x + _node.transform.position.x) / 2,
                Mathf.Max(character.transform.position.y, _node.transform.position.y) + 1f
            );
        }
        else if(zoneNumber == 1){
            _peak = new Vector2(
                Mathf.Min(character.transform.position.x, _node.transform.position.x) - 1f,
                (character.transform.position.y + _node.transform.position.y) / 2f
            );
        }
        else if(zoneNumber == 2){
            _peak = new Vector2(
                (character.transform.position.x + _node.transform.position.x) / 2,
                Mathf.Min(character.transform.position.y, _node.transform.position.y) - 1f
            );
        }
        else{
            _peak = new Vector2(
                Mathf.Max(character.transform.position.x, _node.transform.position.x) + 1f,
                (character.transform.position.y + _node.transform.position.y) / 2f
            );
        }

        while (Vector2.Distance(character.transform.position, _peak) > 0.1f)
        {
            character.transform.position = Vector2.MoveTowards(character.transform.position, _peak, _jumpSpeed* Time.deltaTime);
            if(_jumpSpeed < jumpMaxSpeed){
                _jumpSpeed += jumpSpeedIncrease * Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        while (Vector2.Distance(character.transform.position, _node.transform.position) > 0.1f)
        {
            character.transform.position = Vector2.MoveTowards(character.transform.position, _node.transform.position, _jumpSpeed* Time.deltaTime);   
            if(_jumpSpeed < jumpMaxSpeed){
                _jumpSpeed += jumpSpeedIncrease * Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        moveNextNode(_sequence, _completionCallback);

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
