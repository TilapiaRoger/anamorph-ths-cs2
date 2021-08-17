Model								|	# of 		|	# of Times	|	Distance between	|	Fully	|	MSAD	|	Fully
									|	Triangles	|	The Model 	|	winningPoint and	|	Manual	|			|	Automatic
									|				|				|	modelSpawnPoint		|			|			|
									|				|				|	(in meters)			|			|			|
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------									
Suzanne								|	968			|	6 			|	15					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Empire State Building			|	2066		|	6 			|	15					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
Dolphin								|	14672		|	8 			|	15					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Utah Teapot						|	9438		|	8 			|	15					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Eiffel Tower					|	17060		|	8 			|	30					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
Buddha								|	72910		|	10 			|	30					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Stanford Dragon					|	100000		|	12 			|	30					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Stanford Armadillo				|	112402		|	14 			|	45					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Stanford Bunny					|	112414		|	16 			|	45					|	✓		|	✓		|	✓
------------------------------------+---------------+---------------+-----------------------+-----------+-----------+-------------
The Phlegmatic Dragon (Smoothed)	|	480076		|	18 			|	45					|	✓		|	✓		|	✓	

MSAD - Manually Sliced Automatically Distributed

Concrete algorithm steps:
01. Pick the modelSpawnPoint along the axis between the origin and the game limits
02. Pick the winningPoint along the axis between the modelSpawnPoint and the game limits
03. Initialize the origin, winningPoint, and modelSpawnPoint as invisible spheres
04. Initialize the model at modelSpawnPoint
05. Slice the model such that a slice is sliced individually each time
    * Fill the faces of the sliced side
    * Parent the slices to modelSpawnPoint
06. Distribute the slices
    * Translate the slice along the axis
    * Scale the slice by newDistance / oldDistance
07. Parent modelSpawnPoint to winningPoint
08. Rotate the winningPoint at random angles
09. Parent winningPoint to origin
10. Rotate origin 
11. (Optional) Add random slices