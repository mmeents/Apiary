# Apiary
C# Windows Form App to help follow users on GitHub.  Showcase FileTable and what AppSmith can do.  

You need to set up Access Token: <br>
In GitHub under <br>
profile/settings/developer settings <br>
Personal Access Tokens / Fine-grained tokens:<br>
You need to add one, the name is the Access Token Name.  The secret is the value.  The following Access must be graned to key:
  - Followers, Starring, Watching should be set to read write.
  - Interaction limist I set to readonly. <br><br>

Record the key in a password manager, it won't be shown again. 

Run Visual Studio, clone a branch and run. App should look like next picture.  

![Configure Image](https://mmeents.github.io/files/Apiary.png)
Use the Access Token Name and Token value to populate the two text boxes to setup authentication to GitHub Api.

Then click the Explor tab to get to next page. 

![Configure Image](https://mmeents.github.io/files/Apiary1.png)

Top left to right, Countdown when timer is enabled.  Checkbox to enable the timer, Rate limits to track current rate limit status.

When starting out the file to hold the users to follow is empty.  you will need to manually add user in Follows Textbox to get started.

When Follows runs it follows the users and downloads all the users the user follows and adds to the list to follow.

When user is donloaded a second time then the FollowCount in incremented.

When Set Next runs it grabs the top 5 to follow ordered by followcount descending.

So as 5 users are added the counts change to only follow the top 5 most followed at a time. 

Enjoy Matt.
