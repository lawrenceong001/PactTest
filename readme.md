# A Solution to Showcase the use of Pact for Contract Testing in the .Net world

The purpose of this project is to provide a skeletal example of using Pact for .Net projects, specifically .Net 5 and above. This project may work for .Net Core 3 as well but would require a reconfiguration.


# Consumer Tests
## Steps to execute current test
1. go into the tests/consumer.tests folder
	``cd ./tests/consumer.tests``
2. rebuild the project
	``dotnet build``
3. run the tests
	``dotnet test``
4. go into the pacts folder to view the generated JSON file
	``cd ../../pacts``
5. there will be a file called ``consumer1-testapi1.json`` which is the PACT file generated if the test was successful

## Steps to Create your own test

1. create new project 
		``dotnet new xunit``
2. add pactnet (replace .Windows with something else if this is being implemented on a different platform)
		``dotnet add package pactnet.windows``
3. add a mock fixture: see PactMock.cs
4. add an API Client: see ApiClient.cs
5. add your tests: see Consumer1Tests.cs
6. run your tests 
		``dotnet test``
7. locate your pacts folder and view the generated pact file in JSON format