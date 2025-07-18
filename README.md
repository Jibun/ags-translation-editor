# AGS Translation Editor #
This project is a continuation —or a variation, if you will— of the BitBucket version of [AGS_TranslationEditor](https://bitbucket.org/Taktloss/ags_translationeditor/src/master/) by [Taktloss](https://github.com/Taktloss).  
I just recently realized that a more recent [version](https://github.com/Taktloss/AGS_TranslationEditor) has always been available on GitHub. At this point, the differences have grown too large to create a proper fork and merge them.  
Keep in mind that while this version fixes some issues and introduces new features, it also lacks some of the more recent ones found in the original version.

### What can you do with AGS Translation Editor ? ###
* You can open and see TRS and TRA files in a comprehensible way.
* You can edit translations in a simple way and save them back as TRS files.
* You can convert TRS files into TRA to be read in-game.
* You can see statistics of your translation progress. 

### Pending Features ###
* Create new TRS file.
* Allow wrap arond search.

### Known Issues ###
The version detection of the AGS files is hardcoded, as I haven't found a clear pattern. As such, it may fail in games that I haven't tested.

### Changelog ###
#### 2.1.1 ####
* Fixed TRA creation and Game Information retrieval for old ASG version games (messed up in last update).
* Added AGS as a supported file format for Game Information retrieval and TRA creation.

#### 2.1.0 ####
* Added support for newer AGS games.

#### 2.0.0 ####
* Names of some forms, methods, variables, etc. changed.
* Some components moved to folders 'Forms' and 'Classes'.
* Fixed TRA reading and writing methods. Special characters are now correctly loaded, displayed and saved. Some other minor fixes.
* Fixed save option behaviour that automatically overrode TRA files with TRS content, making them unusable.
* New 'go to line' and 'search text' options added.

## AGS Files Related ##

### Where do I get the script of a game ? ###
* Unfortunately, this version does not provide that feature. However, you can either manually search for "__NEWSCRIPTSTART_" in the game executable and copy & paste the script into a new file, or use the script available in the [original version](https://github.com/Taktloss/AGS_TranslationEditor).

### TRS file format ###
A TRS file should look like this example

```
#!python

&1 Original Text1

&2 Original Text2
&2 Test translation
&3 Original Text3


```
