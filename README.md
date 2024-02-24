<img src = "https://github.com/swyang0/Lsystem3DTrees/assets/70448679/0386573f-6ef2-432d-b3cd-eaadb7718660" width = 350>

# Procudural Modeling 3D Trees

This project aims to simulate the growth of a tree in a three-dimensional environment using an L-system. 
The L-system helps in generating intricate tree structures based on certain rules and parameters. 
Users have the flexibility to customize various aspects of the tree, including its branches, leaves, 
and environmental conditions such as wind.

<img src = "https://github.com/swyang0/Lsystem3DTrees/assets/70448679/f43778b8-71ef-427d-b88f-41bb307b1321" width = 240>
<img src = "https://github.com/swyang0/Lsystem3DTrees/assets/70448679/513ae787-b96e-489b-8085-058e3f3e06e5" width = 240>


## User Interface
Below is a list of parameters to modify.
- Branch Control
  - angle - how much to turn for each generation of branches/leaves
  - maximum branch length
  - maximum branch width
  - branch reduction rate - how fast to reduce the branch width
- Wind Control
  - wind intensity - the strength of the wind
  - wind direction
    - no wind
    - positive/negative x
    - positive/negative z
- Leaf Control
  - maximum leaf size
  - leaf shape by number of sides
  - leaf color
- Tree Style
  - 3 predefined rules for generating different style of trees
  - custom L-system rules
 
## L-System Rules
- ‘F’ create a branch
- ‘L’ create a leaf
- ‘+’ clockwise rotate around x axis
- ‘-’  counterclockwise rotate around x axis
- ‘$’ clockwise rotate around y axis
- ‘%’ counterclockwise rotate around y axis
- ‘^’ clockwise rotate around z axis
- ‘&’ counterclockwise rotate around z axis
- ‘[‘ save current state and push to stack
- ‘]’ pop stack
