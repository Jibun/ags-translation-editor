# AGS Translation Editor #

### What can you do with AGS Translation Editor ? ###
* Read, edit, convert and create TRS and TRA Translation Files for AGS Games.

### Where do i get the script of a game ? ###
* Unfortunatly i didn't found a proper to get the scripts with an application, however you can manually search for "__NEWSCRIPTSTART_" in the Game Exe and copy&paste the textinto a new one it's only a bit tedious ;).

### TRS file format ###
A TRS file should look like this example

```
#!python

&1 Original Text1

&2 Original Text2
&2 Test translation
&3 Original Text3


```

### Known Issues ###
* there are problems with special characters like äöüßèà since Adventure Games Studio only supports the ASCII character (thought there are workarounds you can read more about it here http://www.adventuregamestudio.co.uk/wiki/Fonts#The_MOST_IMPORTANT_part:_define_your_needs)