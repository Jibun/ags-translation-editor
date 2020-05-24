# AGS Translation Editor #

### What can you do with AGS Translation Editor ? ###
* You can open and see TRS and TRA files in a comprehensible way.
* You can edit translations in a simple way and save them back as TRS files.
* You can convert TRS files into TRA to be read in-game.
* You can see statistics of your translation progress. 

### Pending Features ###
* Create new TRS file.
* Allow wrap arond search.

### Known Issues ###

### Changelog ###
#### 2.0.0 ####
* Names of some forms, methods, variables, etc. changed.
* Some components moved to folders 'Forms' and 'Classes'.
* Fixed TRA reading and writing methods. Special characters are now correctly loaded, displayed and saved. Some other minor fixes.
* Fixed save option behaviour that automatically overrode TRA files with TRS content, making them unusable.
* New 'go to line' and 'search text' options added.

## AGS Files Related ##

### Where do I get the script of a game ? ###
* Unfortunatly I didn't found a proper way to get the scripts with an application, however you can manually search for "__NEWSCRIPTSTART_" in the Game Exe and copy&paste the text into a new one it's only a bit tedious ;).

### TRS file format ###
A TRS file should look like this example

```
#!python

&1 Original Text1

&2 Original Text2
&2 Test translation
&3 Original Text3


```
