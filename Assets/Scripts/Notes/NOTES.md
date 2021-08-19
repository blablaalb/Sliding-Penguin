## Platform Child
### Structure
- _PlatformChildOrigin_
    -  _PlatformChild_

# Terrain generation
## How to generate terrain randomly?
- How long the generated terrain should be?
- How we make sure that obstacles don't go one after another.
## Notes
- Obstacles are either at 65 rotation (right), -65 rotation (left) or at 0 rotation (in middle).
- We should left the last platform in the chunk empty so the road is not comletely blocked.

## Obstacles Diffculty:
- 0 - No obstacle
- 1 - Sliding trampoline
- 2 - One single obstacle on one side
- 3 - One single obstacle on each side
- 4 - Two obstacles on one side and one obstacle on another side.
- 5 - One full obstacle
- 6 - One single obstacle on each side and one full obstacle

# Power-ups
## Ice cream
Makes the player invincible for few seconds

# Fly after player swiped
The platform rotate to begger radius after player swipes on the screen.
