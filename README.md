# anamorph-ths-cs2
Repository for the thesis Automatic Splitting and Distribution of 3D Models For Puzzles Using Anamorphosis

## Thesis Group 
- Hans Dylan Lao
- Jessamyn Kristi Lapan

## Models List and Their Stats

| Model | # of Triangles | # of Times the Model is Sliced | Average Distance between Slices (in meters) | Fully Manual | Manual Slicing and Automatic Distribution | Fully Automatic | 
| :---- | :-------------- | :------------------------------ | :------------------------------------------- | :------------ | :----------------------------------------- | :--------------- |
| **Suzanne** | 968 | 6 | 1 | ✔️ | ✔️ | ✔️                                 
| **The Empire State Building** | 2066 | 6 | 1 | ✔️ | ✔️ | ✔️     
| **Dolphin** | 14672 | 8 | 1 | ✔️ | ✔️ | ✔️     
| **The Utah Teapot** | 9438 | 8 | 1 | ✔️ | ✔️ | ✔️     
| **The Eiffel Tower** | 17060 | 8 | 1.5 | ✔️ | ✔️ | ✔️     
| **Buddha** | 72910 | 10 | 1.5 |  | h✔️ | ✔️     
| **The Stanford Dragon** | 100000 | 12 | 1.5 |  | h✔️ | ✔️     
| **The Stanford Armadillo** | 112402 | 14 | 2.5 |  | h✔️ | ✔️     
| **The Stanford Bunny** | 112414 | 16 | 2.5 |  | h✔️ | ✔️     
| **The Phlegmatic Dragon (Smoothed)** | 480076 | 18 | 2.5 |  | h✔️ | ✔️     

h✔️ - half done

## Concrete algorithm steps:
01.  Pick the winning point
02.  Pick the the winning distance
03.  Set the one of the axes as the winning angles
04.  Pick the spawn point of the model based on the winning distance and angles
05.  Slice the model such that a slice is sliced individually each time
05.1 Set the origins of the slices to the spawn point of the model
05.2 Fill the faces of the sliced side
07.  Distribute the slices
08.  Pick random winning angles
09.  Delete the spawn point of the model
10.  Rotate the entire collection of slices with the winning point as the center according to the winning angles
(Optional) Add random slices
