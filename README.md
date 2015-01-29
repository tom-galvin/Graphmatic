Graphmatic
==========

This is my coursework for the AQA Computing A2 coursework. Features do (or will) include:

| Feature | Stage |
|:--------|:------|
|Entering of expressions in the style of a *Casio fx-83* series calculator|Feature complete|
|Parsing the entered expressions|Feature complete|
|Plotting those equations in graphical form|Feature complete|
|Entering of data sets|Feature complete|
|Plotting those data sets in graphical form, alongside equations|Feature complete|
|Performing statistical calculations on those data sets|Implementation complete|
|Annotating both graphs and data sets, for demonstrations on an Interactive Whiteboard|Feature complete|
|Extensive bug testing|Not started|

There are 6 stages:

| Stage | Meaning |
|:------|:--------|
|Not Started|The feature has not been started yet.|
|Started|Work has started on the feature. Classes may have been set up, but no implementation is present yet.|
|Active Development|The feature is being actively worked on. Issues are being resolved regarding the issue.|
|Implementation Complete|The main body of the feature is complete, but some utilities may be missing, or the user experience may not be fine tuned yet (eg. no keyboard shortcuts.)|
|Feature Complete|The feature is complete, but has not been extensively bug-tested or tested by the user.|
|Complete|The feature is solid, bug-free, user-tested, and works as expected and described. No more work is being done on the feature except to resolve any newly discovered bugs.|

information
-----------

Bear in mind that this project was done with satisfying the mark scheme for the AQA Computing A2 coursework project rather than being a future-maintainable project.
For this reason, some questionable decisions such as using WinForms and rolling my own serialization code had to be undertaken, in order to artificially inflate the
complexity of the code base. I didn't like doing this, but the project still works as it is, so I won't go out of my way to port this to WPF any time soon, as good
as that would be.

The usage of WinForms also leads to the fact that there is a lot of code cruft to work with the older bits of the .NET framework, especially in the WinForms classes
(such as in `Main.PageEditor.cs`). As I have said, this project was written to be on-time and functional so the code is not necessarily as clean as it would be had I
wrote it as a personal project from the start.