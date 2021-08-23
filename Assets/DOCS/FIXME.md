# Penguin 
## Jump Height
- Constraint penguin's jump height.
    ```csharp
    float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    ```
    
- We should check for the penguin's height in the `BellySlideHorizontal` state.

- Some icicles hit penguin when it flies.