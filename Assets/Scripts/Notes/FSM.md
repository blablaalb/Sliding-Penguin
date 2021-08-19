### Jump
```
jump
if descending
    if fly
        enter state fly
    else
        enter state descending
```

### Fly
```
fly
after N seconds
    enter state descending
```

### Descending
```
update loop
    if on ground
        enter state land
```

### Land
```
if land 
    if current state is land
        exit current state
```

### Sliding
```
if on ground
    if ground is jump area
        if penguin hasn't jumped from this jump area
            set fly to false
            enter state jump
    else if in front of the penguin is slide area
        if penguin hasn't slided from this slide area
            enter state slide
else if not on ground
    if on gap
        set gap
        enter onGap state
    else if above void
        set fly true
        jump
```

### OnGap
_Currently after entering the gap sate penguin just fall off the gap and dies. They may add some new `gaps` in the future. In that case make the gap class base class for other gaps._
```
disable gap's collider
```