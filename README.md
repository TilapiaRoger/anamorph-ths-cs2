# Automatic Slicing and Distribution of 3D Models Using Anamorphosis
The application associated with the thesis, **Anamorph Puzzle** is mainly about solving automated models automatically sliced and then distributed in an anamorphic manner using algorithms. The application was developed in Unity 2021.1.12f1 using C#, and runs on Windows personal computers. Although the automatic distribution algorithm used is entirely developed by the thesis researchers, the base automatic slicing algorithm used is by Whirle (2021). The base slicing algorithm was used along with an algorithm for the slicing blade that slices six times. The first three times are sliced vertically, and the last three times are sliced horizontally. 

The winning view algorithm randomly sets the model spawn point, the winning point, and the winning angle. The model spawn point, is where the selected model is placed. Then, the winning point, or winning sphere, is an invisible sphere set at a distance from the model spawn point. The winning angle is a random rotation that rotates the origin, which has the winning angle, winning point, model spawn point, and the model as children.

The application allows the user to solve a model in any of the 3 model processing methods, or solving modes:
1. Manual Slicing and Distribution 
2. Manual Slicing and Automatic Distribution, or Manual Slicing, Auto Distribution
3. Automatic or Auto Slicing and Distribution

## Test Models
The test models are the following 10 models available for solving in each of of the 3 solving modes. They are already provided inside the application. Thus, the user no longer needs to download the models.

## Samples of the Solving Modes
### Manual Slicing and Distribution
The models were both pre-sliced and pre-distributed in Blender before being imported to the application's Unity project.

### Manual Slicing and Automatic Distribution
The models were pre-sliced in Blender, but automatically distributed in the application using an algorithm.

### Automatic Slicing and Distribution
The models were both automatically sliced and distributed in the application with algorithms.

## Skyboxes
The following skyboxes were used in the application:

### AllSky Free - 10 Sky / Skybox Set by rpgwhitelock (2021)


### Fantasy Skybox FREE by Render Knight (2020)


### 3D Skybox PRO - Mediterranean Freebies by Crosstales (2020)


### City Street Skyboxes Vol. 1 (2019)


## Getting Started
### Installation 
1. Click on [this Google Drive link](https://drive.google.com/drive/folders/1QaYy-SKNvhFxNSrBXMH7rque4I_qqR2U?usp=sharing).
2. Install the ZIP file **Anamorph Puzzle.zip** in the link. Save to a directory of your choice.
3. Once download is finished, find and unzip the file in the selected directory.
4. Go inside the folder from the unzipped file, and click on **Anamorph Puzzle** to run the application.

### User Manual
[User Manual](https://github.com/TilapiaRoger/anamorph-ths-cs2/blob/629bb780557568ec4cd4e856c9b5cc53a7225122/%5BTHS-CS3%5D%20User%20Manual%20for%20Automatic%20Slicing%20and%20Distribution%20of%203D%20Models%20For%20Puzzles%20Using%20Anamorphosis.pdf)

### References
- Whirle,  D. (2021). Unity assets. https://github.com/BLINDED-AM-ME/UnityAssets. GitHub
- Crosstales (2020). 3D Skybox PRO - Mediterranean Freebies. https://assetstore.unity.com/packages/2d/textures-materials/sky/3d-skybox-pro-mediterranean-freebies-98721. Unity Asset Store
- MoodWare (2019). City Street Skyboxes Vol. 1. https://assetstore.unity.com/packages/2d/textures-materials/sky/city-street-skyboxes-vol-1-157401. Unity Asset Store
- Render Knight (2020). Fantasy Skybox FREE. https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353. Unity Asset Store
- rpgwhitelock (2021). AllSky Free - 10 Sky / Skybox Set. https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014. Unity Asset Store
