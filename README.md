# anamorph-ths-cs2
Repository for the thesis Automatic Splitting and Distribution of 3D Models For Puzzles Using Anamorphosis

## Thesis Group 
- Hans Dylan Lao
- Jessamyn Kristi Lapan

## Models List and Their Stats

| Model | # of Triangles | # of Times the Model is Sliced | Average Distance between Slices (in meters) | Fully Manual | Half Half | Fully Automatic | 
| :---- | :-------------- | :------------------------------ | :------------------------------------------- | :------------ | :------------ | :--------------- |
| **Suzanne** | 968 | 6 | 1 | ✔️ | ✔️ | ✔️                                 
| **The Empire State Building** | 2066 | 6 | 1 | ✔️ | ✔️ | ✔️     
| **Dolphin** | 14672 | 8 | 1 | ✔️ | ✔️ | ✔️     
| **The Utah Teapot** | 9438 | 8 | 1 | ✔️ | ✔️ | ✔️     
| **The Eiffel Tower** | 17060 | 8 | 1.5 | ✔️ | ✔️ | ✔️     
| **Buddha** | 72910 | 10 | 1.5 | ✔️ | ✔️ | ✔️     
| **The Stanford Dragon** | 100000 | 12 | 1.5 | ✔️ | ✔️ | ✔️     
| **The Stanford Armadillo** | 112402 | 14 | 2.5 | ✔️ | ✔️ | ✔️     
| **The Stanford Bunny** | 112414 | 16 | 2.5 | ✔️ | ✔️ | ✔️     
| **The Phlegmatic Dragon (Smoothed)** | 480076 | 18 | 2.5 | ✔️ | ✔️ | ✔️     

## Concrete algorithm steps:
01.  Pick the modelSpawnPoint along the axis between the origin and the game limits
02.  Pick the winningPoint along the axis between the modelSpawnPoint and the game limits
03.  Initialize the origin, winningPoint, and modelSpawnPoint as invisible spheres
04.  Initialize the model at modelSpawnPoint
05.  Slice the model such that a slice is sliced individually each time
06.1 Fill the faces of the sliced side
06.2 Parent the slices to modelSpawnPoint
07.  Distribute the slices
07.1 Translate the slice along the axis
07.2 Scale the slice by newDistance / oldDistance
08.  Parent modelSpawnPoint to winningPoint
09.  Rotate the winningPoint at random angles
10.  Parent winningPoint to origin
11.  Rotate origin
(Optional) Add random slices

# In code
1. Initializer
2. Slicer
3. Distributor
4. Rotator
5. Results
