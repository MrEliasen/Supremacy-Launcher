Supremacy Launcher
========
I wanted to learn about C# I decided to code a game launcher for ARMA (since I was coding a framework for it as well). It will download and keep your server mod up to date. This is my first C# application and I don't know any "best practises" in this language yet. Feedback is absolutely more than welcome.  
It is likely to have bugs as it was originally propraritary and not intended for anything other than one specific mod. You will need to know some C# or be willing to learn if you want to use this software.

## Current Features
 * Download and keeps mod up to date from specified url.
 * Launcher ARMA 3 and connects directly to your server.

## Usage/Installation
It was coded in Microsoft Virtual Studio.  

1. Upload the web files.
2. Upload your mod directory to the "downloads" directory of the web files you just uploaded and run the "file-indexer.php" (upload exactly as they are from your arma directory).
3. Open "Supremacy Launcher v2.sln".
4. Make any changes needed to the "Form1.cs" code (like download url etc).
5. Update the Project -> Configuration Manager with your urls etc.
6. Compile and deploy.
7. You should probably as a minimum rename the file-indexer.php file or in some way lock it down to avoid malicious users from running it.

## 0.1.0.1 (23-11-2015 GMT)
* Added multi directory support.
* Fixed bug when checking files for updates.

## Support / Feedback / Issues
If you need help, have feedback, requests or like, please visit [The Forums](https://www.sirmre.com/forums/).   
When it comes to code help, I won't just code stuff for you - try yourself first, show me you made an effort and I'll be happy to assist to the best of my ability. Please note I am doing this is my spare time and I am quite busy so don't expect anything.

## Copyright & License
Code released under [CC BY-NC 3.0 License](https://creativecommons.org/licenses/by-nc/3.0/legalcode).  
To read the human-readable summary of the CC BY-NC 3.0 License, [click here](https://creativecommons.org/licenses/by-nc/3.0/).  
Copyright (c) 2015 Mark Eliasen.