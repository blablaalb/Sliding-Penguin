# Penguin 
## Jump Height
- Constraint penguin's jump height.
    ```csharp
    float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    ```
- We should check for the penguin's height in the `BellySlideHorizontal` state.
- Move objects behind player back to pool.
- Fix touch input. We should rotate platofrm by swipe of a finger.