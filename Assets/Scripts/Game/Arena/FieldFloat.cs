using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFloat : MonoBehaviour {
    public Playfield playfield;
    public Vector2 startingPosition;
    public Vector2 p1Position;
    public Vector2 p2Position;
    public Vector2 destinationPosition;
    public float progress = 0f;
    public bool isFloating = false;
    public void StartFloating(float speed, Vector2 _destination) {
        if (!playfield)
            playfield = GetComponent<Playfield>();
        startingPosition = transform.position;
        destinationPosition = _destination;
        playfield.isAnimating = true;
        progress = 0f;
        isFloating = true;
        (p1Position, p2Position) = ArenaCoordinator.GetIntermitentPoints(startingPosition, destinationPosition);
        //Debug.Log("will flaot via: p0:" + startingPosition + " p1: " + p1Position + " p2:" + p2Position + " p4:" + destinationPosition);
    }
    void FixedUpdate() {
        if (isFloating) {
            progress += Time.deltaTime / ConstantsBucket.PlayfieldFloatTime;
            if (progress <= 1f)
                transform.position = Curves.CubeBezier3(startingPosition, p1Position, p2Position, destinationPosition, progress);
            else {
                isFloating = false;
                transform.position = destinationPosition;
                PostMoveAction();
            }
        }
    }
    public void PostMoveAction() {
        //Debug.Log("float eneded, stopping at:" + (Vector2) (transform.position));
        playfield.isAnimating = false;
        EventCoordinator.TriggerEvent(EventName.System.Environment.PlayfieldAnimated(), GameMessage.Write().WithPlayfield(playfield));
    }
}