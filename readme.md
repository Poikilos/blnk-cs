# Blnk-cs
(formerly Blnk)
Try poikilos/blnk instead. This is the old C# version of BLink to run blnk files.

"Blink and your program appears on any OS regardless of home folders parent directory location"

A link format that intentionally behaves differently than your os' or desktop's links.

## Features
* Converts lnk to blnk when received
* Optionally deletes the lnk file (if blnk file exists after attempting to create one) if you drag it to the clearly labeled dark area of the window
* Great for shortcuts from one place to another within your ownCloud folder in your user profile, possibly better than lnk files which keep being synced
* Sets StartInfo.UseShellExecute=true if blnk file contains the line 'Terminal: true'

## Known Issues
* Doesn't run on linux if compiled using MonoDevelop, due to:
```
System.Resources.MissingManifestResourceException
Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure "blnk.MainForm.resources" was correctly embedded or linked into assembly "blnk" at compile time, or that all the satellite assemblies required are loadable and fully signed.
```
at:
```
this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
```
	* even after doing the following steps in MonoDevelop and recompiling:
		* adding Assembly References manually to System.Core, System.Xml.Linq, and System.Data.DataSetExtensions
		* right-clicked on MainForm.resx, but Build Action was already "EmbeddedResource" which is correct
* needs to run blnk in console if Terminal is true

## BLNK file format documentation
* first line of blnk file should be:
Content-Type: text/blnk
otherwise, the blnk format attempts to match the xdg shortcut (.desktop) format
