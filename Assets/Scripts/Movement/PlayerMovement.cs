using UnityEngine;
using VContainer;

public class PlayerMovement : Movement
{
    public void Update()
    {
        movementDirection = MovementInput();
    }

    private Vector2 MovementInput()
    {
        float horizontalInput = SimpleInput.GetAxis("Horizontal");
        float verticalInput = SimpleInput.GetAxis("Vertical");

        return new Vector2(horizontalInput, verticalInput);
    }
}
