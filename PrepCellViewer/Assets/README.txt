In case of adding new robot you need to find it model in obj format, or in .stp file and convert with freeCAD. 

Other way is to download it in .jt (siemens) from Kuka wbesite and convert .jt->.stp->.obj (converting .jt file might be a chllenge)

In case of adding new robot you need to:
1. Import it to project in .obj file
2. [disable other robots]
3. Put in on scene as a tree

 (Joint1)
  ||  \\  
  ||   \\
  ||    \\
  ||     \\
  \/      \/
(Joint2)  (Grphic part)
  ||  \\
  ||   \\
  ||    \\
  ||     \\
  \/      \/
(Joint3)  (Grphic part)

Efector must start from empty "Game Object" in your tree - otherwise it will nor be recolored

In case of Agilus R6 Joint are called k1, k2, ..., k7

Tree should start in object Robots

4. In Every joint Z axis must be a pivot (enable Pivot view in editor)
5. Apply RotateJoint script to every joint.
6. Add Mesh Collider to all graphic parts and set Convex and "Is Trigger"
 Add Rigid Body to all graphic parts, set Use Gravity off and Is Kinematic on
7. Use materials RoboOrange on robot, and RoboBlack on efector and base
 To make colision work properly you need to add tags to graphic part:
 - Efector for all Efector parts
 - RobotFront1 and RobotFront2 for parts that might touch robot
 - RobotSafe for parts that can not hit any parent 
 - RobotBase for robot Base
8. Change Joint on all sliders in OnValueChanged field and pick RotateJoint.OnValueChange.ChangeZ

From this point Your robot might work and detect collision poperly. Hopefully I will make Colision more intuitiwe, but we all know how it works


Remeber to check all joint axis - class RotateJoint schould work if every joint is a chlid of prewious joint, and have Z axis as a pivot

COLISION SCRIPT

(Colision script is messy. It will paint all Robot### tags orange, efector tag black etc.)
(If something is untagged it looks for ColiBound in parent for name of this obj)
(If this name checks out it changes material accordingly to name of obj) 
(All surroundings should be a child of Fence, or Machines)
(I advise agains any changes is colision script. Rebouliding it from scrach maight be a better idea (It ain't that hard - just couter intuitive))

usefull files:
- file:///C:/Users/ZEUS/Downloads/Spez_KR_AGILUS_en.pdf

Disclamer

RTS_Camera is a free asset I used for this poject and it is completely buchered and changed to fit needs of this project. (It is pretty cool asset(and free!))
I STRONGLY adwise agains ANY changes in this script. It is a real mess.

For now Files contain FastIK Scripts but do not use them. This funkcionality isn't ready  yet.

Important!

2nd Joint (K3) of robot Agilus R6 is tilted 0.3 degree to avoid numeric error caused by 90,0,0 angles 

This joint is wery weird. His transform doesn't match what is displayed in UnityEditor. It coused me as lot of trubles, but now looks solid.