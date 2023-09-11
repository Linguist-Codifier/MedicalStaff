# Pretty and Simple ASP.NET Core REST API with Entity Framework Core

## Context

The goal is to develop an API that allows doctors to create, edit
and delete records, while patients will be able to view their own information. The
project aims to offer security, efficiency and ease of access to clinical data,
ensuring patient privacy and facilitating collaboration between doctors and others
Health professionals. The medical staff as well as the patients will be able to create their own user accounts.

# Setup

##### - How to install Visual Studio 2022

<h6><img src="./Docs/Images/DownloadsPage.PNG"/></h6>

Microsoft Visual Studio is an integrated development environment (IDE) from Microsoft. It is used to develop computer programs for Microsoft Windows. Visual Studio is one stop shop for all applications built on the .Net platform. One can develop, debug and run applications using Visual Studio.
Both Forms-based and web-based applications can be designed and developed using this IDE. [See more.](https://visualstudio.microsoft.com/pt-br/downloads/)

- [Download Visual Studio 2022 Community](https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&workload=dotnet-dotnetwebcloud&passive=false#dotnet)
- [Installation Guide](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022) - By installing, choose the ASP.NET and Web Applications workload container and follow along with the installation guide.

### Executing the API

- #### Donwload the Solution
```bash
  #Clone this repository or download it as ZIP
  $ git clone https://github.com/Linguist-Codifier/MedicalStaff.git
```
- #### Open the Solution

<h6><img src="./Docs/Images/SolutionFile.PNG"/></h6> 

##### Make sure you open it up with Visual Studio 2022 in case there are any other versions already installed.

- After openning the Solution, Visual Studio will check any missing dependency and will try to download them from NuGet.

- After everything is set up, compile the solution by pressing CTRL + SHIFT + B and run it as follows:

<h6><img src="./Docs/Images/ExecutingSolution.PNG"/></h6>

There are two main modes of execution, one is in Debugging mode and the other is without debugging at runtime. It can also be done by either pressing CTRL + F5 (For runtime debugging) or F5 (No debugging).

