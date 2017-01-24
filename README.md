# AzureFunctions.Sample

### SAMPLE PROJECT OBJECTIVE:
##### Given the way Azure Function works today...
* Demonstrate completely Automatic CD
* Based purely on Azure and GIT, minimal scripting
* Use a “normal” .NET development environment 
* Support Building of Shared Assembly on Deploy
* Support Nuget download for scripts and assemblies
* Never manually deploy a binary

#### Project Structure
![alt text](screenshots/Azure Function - Project Structure.png "A "Typical" .NET Project Structure")

#### Motivation
The practical motivation is that the current recommended options surrounding dynamic compilation, shared libraries and pre-compiled binaries are not elegant. 

#### Things to Know
There's no magic really, it's just a project convention and a build script.
  
The deploy.cmd enumerates all subdirectories for projects and builds them.  
  
It puts the assemblies into the /bin subdirectory so that run.csx can reach it.   

run.csx "relays" or "proxy's" the call from Azure Function
* Thus, you have to ensure the method signatures and return types between the two make sense
* With async and Task<>, you can't just copy and paste the signatures 
  
run.csx references Function1.dll by filename.
* The filename of the DLL is based on the __NAMESPACE__ in the .cs file.  
* Thus, if you change the namespace, you'll need to manually update the run.csx


#### Room for Improvement
This model works today, but there are some annoyances. 

For example, deploy.cmd performs one build of the projects, and Kudu automatically builds the .CSX files. Also, the .csx script only exists to satisfy Azure Functions, and it's a bit of a pain to manage relaying the call and the DLL reference.

Ideally Microsoft could implement a form of our build.cmd based on a standard project structure they define. Then they could also implement the strategy documented here for "Precompiled Binaries" to bypass the CSX:  
https://github.com/Azure/azure-webjobs-sdk-script/wiki/Precompiled-functions

#### TODO

The only thing that I know works is Function1.  
*Deploy to azure function with GIT and it will build. Then you can use the portal to test.*  

Over the next week or two, need to:
* Look at Function2 and make it work (might already be)
* Look at the Unit Tests and make them work (might already be)
* Add working integration test and try the integrated debugging
  
