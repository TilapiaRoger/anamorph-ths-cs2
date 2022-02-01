## Getting Started
### Installation 
1. Click on [this Google Drive link](https://drive.google.com/drive/folders/1QaYy-SKNvhFxNSrBXMH7rque4I_qqR2U?usp=sharing).
2. Install the ZIP file **Anamorph Puzzle.zip** in the link. Save to a directory of your choice.
3. Once download is finished, find and unzip the file in the selected directory.
4. Go inside the folder from the unzipped file, and click on **Anamorph Puzzle** to run the application.

# Overview

## Anamorphosis
Anamorphosis is the perspective technique where a complete picture can be viewed from a certain viewpoint, but distorted otherwise. This version of anamorphosis involves slicing the model up and distributing its pieces along the intended line of sight.

## Solving Modes
In the app, there are 10 models that have been processed in 3 different ways. Each automating an aspect of setting up the puzzle. The following modes have been used:
1. Fully manual
2. Manual Slicing Automatic Distribution (MSAD), 
3. Fully Automatic

Fully manual means models had been sliced up and its slices be distributed by the researcher's hand in Blender. MSAD means the models were manually sliced in Blender, but its slices are automatically distributed by the app. Fully automatic, means both the slicing of the model and the distribution of its slices are all handled by the app.

# System Specifications
The application associated with the thesis, **Anamorph Puzzle** is mainly about solving automated models automatically sliced and then distributed in an anamorphic manner using algorithms. The application was developed in Unity 2021.1.12f1 using C#, and runs on Windows personal computers.


# Automatic Slicing and Distribution of 3D Models Using Anamorphosis
Although the automatic distribution algorithm used is entirely developed by the thesis researchers, the base automatic slicing algorithm used is by Whirle (2021). The base slicing algorithm was used along with an algorithm for the slicing blade that slices six times. The first three times are sliced vertically, and the last three times are sliced horizontally. 

The winning view algorithm randomly sets the model spawn point, the winning point, and the winning angle. The model spawn point, is where the selected model is placed. Then, the winning point, or winning sphere, is an invisible sphere set at a distance from the model spawn point. The winning angle is a random rotation that rotates the origin, which has the winning angle, winning point, model spawn point, and the model as children.

## Test Models
The test models are the following 10 models available for solving in each of of the 3 solving modes. They are already provided inside the application. Thus, the user no longer needs to download the models. The test models are:
![Suzanne](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/01.%20Suzanne.jpg)
**Suzanne**

![The Empire State Building](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/02.%20The%20Empire%20State%20Building.jpg)
**Empire State Building**

![Dolphin](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/01.%20Suzanne.jpg)
**Dolphin**

![Utah Teapot](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/04.%20Utah%20Teapot.jpg)
**Utah Teapot**

![Eiffel Tower](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/05.%20Eiffel%20Tower.jpg)
**Eiffel Tower**

![TV Set](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/06.%20TV%20Stand%20Set%20LG.jpg)
**TV Set**

![Standford Dragon](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/07.%20Stanford%20Dragon.jpg)
**Stanford Dragon**

![Standford Armadillo](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/08.%20StanfordArmadillo.png)
**Stanford Armadillo**

![Standford Bunny](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/09.%20Stanford%20Bunny.png)
**Stanford Bunny**

![Phlegmatic Dragon](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Models/Samples/10.%20Phlegmatic%20Dragon.jpg)
**Phlegmatic Dragon**

### Samples
#### Manual Slicing and Distribution
The models were both pre-sliced and pre-distributed in Blender before being imported to the application's Unity project. The charts show the views of the player upon starting a puzzle and of upon the player finishing the puzzle. Both views show the Player's first person view in game and the Bird's Eye View in the scene view at Unity, respectively. The sphere in the Bird's Eye View images are of the player avatar.

![Sample Manual Slicing and Distribution Models Chart](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Manual%20Models%20Sample.png)

#### Manual Slicing and Automatic Distribution
The models were pre-sliced in Blender, but automatically distributed in the application using an algorithm.

![Sample Manual Slicing and Automatic Distribution Models Chart](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Manual%20Slicing,%20Auto%20Distribution%20Sample.png)

#### Automatic Slicing and Distribution
The models were both automatically sliced and distributed in the application with algorithms.

![Sample Automatic Slicing and Distribution Models Chart](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Auto%20Slicing%20and%20Distribution%20Sample.png)

## Skyboxes
The following skyboxes were used in the application:

### AllSky Free - 10 Sky / Skybox Set by rpgwhitelock (2021)
![Epic Glorious Pink](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Skyboxes/Epic%20Glorious%20Pink.png)
**Epic_GloriousPink.mat**

![AllSky Space Another Planet](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Skyboxes/AllSky%20Space%20Another%20Planet.png)
**AllSky_Space_AnotherPlanet.mat**

### Fantasy Skybox FREE by Render Knight (2020)
![FS000_Day_06](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Skyboxes/FS000%20Day%2006.png)
**FS000_Day_06.mat**

### 3D Skybox PRO - Mediterranean Freebies by Crosstales (2020)
![03-Old Draw Well](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Skyboxes/03%20Old%20Draw%20Well.png)
**03-Old Draw Well.mat**

### City Street Skyboxes Vol. 1 by MoodWare (2019)
![Skybox 24_pan](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/Technical%20Manual%20Images/Skyboxes/Skybox%2024_pan.png)
**Skybox 24_pan.mat**

### User Manual
[User Manual](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/main/%5BTHS-CS3%5D%20User%20Manual%20for%20Automatic%20Slicing%20and%20Distribution%20of%203D%20Models%20For%20Puzzles%20Using%20Anamorphosis.pdf)

### References
- Whirle,  D. (2021). Unity assets. https://github.com/BLINDED-AM-ME/UnityAssets. GitHub
- Crosstales (2020). 3D Skybox PRO - Mediterranean Freebies. https://assetstore.unity.com/packages/2d/textures-materials/sky/3d-skybox-pro-mediterranean-freebies-98721. Unity Asset Store
- MoodWare (2019). City Street Skyboxes Vol. 1. https://assetstore.unity.com/packages/2d/textures-materials/sky/city-street-skyboxes-vol-1-157401. Unity Asset Store
- Render Knight (2020). Fantasy Skybox FREE. https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353. Unity Asset Store
- rpgwhitelock (2021). AllSky Free - 10 Sky / Skybox Set. https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014. Unity Asset Store
