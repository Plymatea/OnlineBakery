# _Pierre's Online Bakery_

#### By **_Andy Plymate_**

#### _A webpage that allows treats and flavors to be mixed and matched while utilizing Identity to control access._

## Technologies Used

* _C#_
* _HTML5_
* _CSS_
* _Razor_
* _Bootstrap_
* _ASP.Net Core_
* _Entity_
* _MVC_
* _Identity_
* _ViewModels_

## Description

_The goal of this application is to create a many-to-many relationship between "Treats" and "Flavors". Limit the create, edit, and delete data functions to authorized users, while allowing any user the ability to read the data._
_Another goal of this project was to show all the treats and flavors on the home page which was accomplished through a ViewModel. Using the same ViewModel, the relationships are added on each treat/flavor details page. Once a relationship has been added to the join table, the option to add a duplicate relationship is restricted by restricting the options in the selectlist._
_The application has been designed with expansion in mind and could easily include more properties for either class if required._

## Setup/Installation Requirements
* _Requires VSCode and MySql_

* _You can find the github repository [here](https://github.com/Plymatea/OnlineBakery.git)_
* _In your preferred terminal, navigate to the directory you would like to store the project_
* _$ `git clone https://github.com/Plymatea/OnlineBakery.git`_
* _Now that the repository is cloned to your computer, right click on the folder and click "open with vs code"_

_**In order to access a usable version of the sql database you will need to do the following:**_

* _Create a file named appsettings.json in the Bakery directory_
* _The file should contain this block of code with your own username and password for the server ( [ ] brackets as well as information inside brackets are private information and not included):_
```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=to_do_list;uid=[Your Id Goes Here];pwd=[Your Password Here];"
  }
}
```
* _$ `dotnet build`_
* _$ `dotnet ef database update`_

 * _Once all of the above is completed you can view the project on your local server by running "dotnet run"_


## Known Bugs

* _No Known Bugs_

## Road Map

* _Create feedback to user when log in information is entered incorrectly._
* _Create feedback to user when trying to create a null object._
* _Checkbox on home page that when active would display the relationships between treats and flavors._
* _Expand properties of class for use in sales environment._
* _Add Order basket module._


## License - [MIT](https://opensource.org/licenses/MIT)


Copyright (c) _2022_ _Andy Plymate_