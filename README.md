# CS437-Project
Implementation of the KNN Algorithm per a particular application.

The program was developed by Taylor Flatt for a CS 437 Machine Learning project. It is programmed in C#.The plan is to continue to implement the various components of the k-NN algorithm such as cross-validation and the ability to handle discrete data.

The program makes several assumptions in order for it to work. All of which I think are reasonable assumptions and easy to work with. 

<strong>Assumption 1</strong>: I assume an excel spreadsheet (*.xlsx). 

<strong>Assumption 2</strong>: I assume that the first row of the spreadsheet are the descriptors of the data.

<strong>Assumption 3</strong>: I assume that the first column represents the output class (name) of the data point. 

<strong>Assumption 4</strong>: I assume that the second column represents the data point label. 

<strong>Assumption 5</strong>: I assume all attribute data is continous data. 

<strong>Assumption 6</strong>: I assume (for now) that you are able to determine a k value and enter it into the application. 

<hr />
Consider the following scenario:

I have a list of cars from 5 different manufacturers. I want to know if I develop a car, who my closest manufacturing competitor will be. So I know that there are a few attributes that really influence the competitiveness of a car.

For instance, the average MPG, fuel capacity, # of options, sale (MSRP) price, and horsepower are all attributes that would put my car in competition with another car. So I have a list of those manufacturers with all their cars with those attributes put in and I run the k-NN algorithm with my car as the input to determine the closest manufacturer to me.
 
In that case, my classes are the manufacturers (Example: Dodge). My data point label were the model of the cars (Example: Charger). My attributes are the average MPG, fuel capacity, # of options, sale price, and horsepower.

