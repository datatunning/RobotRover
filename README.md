# Robot Rover ![CI](https://github.com/datatunning/RobotRover/workflows/Continuous-Integration/badge.svg) [![Coverage](https://codecov.io/gh/datatunning/RobotRover/branch/master/graph/badge.svg)](https://codecov.io/gh/datatunning/RockPaperScissors)
A robotic rover is to be landed by NASA on a rectangular plateau of Mars. The rover can navigate the plateau using a set of simple commands.


** SetPosition(int x, int y, string direction) **

Deploys the rover to an initial grid location [x y], where direction is the initial compass direction.
For example, [0 0 N] means the rover is situated at the bottom left corner, facing North. Assume that the square directly North from (x, y) is (x, y+1).
You may assume that the plateau size has no upper bound.

** Move(string commands) **
Moves the rover by accepting a command string in the form "L1R2...".
  *	L rotates the rover 90 degrees left
  * R rotates the rover 90 degrees right
  * The number represents the grid positions to move in the direction it is facing

The command string may be of any length. This function may be called many times per mission.
