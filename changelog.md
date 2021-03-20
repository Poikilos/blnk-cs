# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [first git version] - 2021-03-20
### Added
- Add the first git version (from computer; some of these files are not
  committed due to `.gitignore`)
```
-rw-rw-r-- 1 owner owner   779 Feb  9  2016 blnk.sln
-rw-rw-r-- 1 owner owner   637 Feb  9  2016 Program.cs
-rw-rw-r-- 1 owner owner 15514 Feb 10  2016 Icon - Blnk.xcf
-rw-rw-r-- 1 owner owner  1983 Feb 10  2016 Icon - Blnk 1a.png
-rw-rw-r-- 1 owner owner 12206 Feb 10  2016 blnk.ico
-rw-rw-r-- 1 owner owner 24429 Feb 10  2016 MainForm.resx
-rw-rw-r-- 1 owner owner  3518 Feb 10  2016 MainForm.Designer.cs
-rw-rw-r-- 1 owner owner  4748 Feb 10  2016 BlnkShortcut.cs
-rw-rw-r-- 1 owner owner  3148 Feb 12  2016 blnk.csproj
-rw-rw-r-- 1 owner owner   484 Feb 12  2016 BlnkLocation.cs
-rw-rw-r-- 1 owner owner   408 Aug  5  2017 blnk.userprefs
-rw-rw-r-- 1 owner owner  5750 Aug  5  2017 blnk_monodevelop.sln
-rw-rw-r-- 1 owner owner 19677 Aug  5  2017 MainForm.cs
-rw-rw-r-- 1 owner owner  2404 Aug  5  2017 blnk_monodevelop.csproj
-rw-rw-r-- 1 owner owner   963 Aug  5  2017 blnk_monodevelop.userprefs
-rw-r--r-- 1 owner owner 35149 Mar 20 17:11 license.txt
-rw-r--r-- 1 owner owner  1735 Mar 20 17:17 readme.md

obj:
total 0
drwxrwxr-x 1 owner owner 24 Nov  7  2018 x86

Properties:
total 4
-rw-rw-r-- 1 owner owner 1065 Feb  9  2016 AssemblyInfo.cs

dist:
total 0
drwxr-xr-x 1 owner owner 116 Mar 20 17:15 share

bin:
total 100
-rw-rw-r-- 1 owner owner 38400 Feb 12  2016 blnk.pdb
-rw-rw-r-- 1 owner owner 57344 Feb 17  2016 blnk.exe
-rw-rw-r-- 1 owner owner     0 Feb 17  2016 out.txt
-rw-rw-r-- 1 owner owner   147 Feb 17  2016 err.txt
drwxrwxr-x 1 owner owner    32 Nov  7  2018 Debug
```

dist/share:
```
total 20
-rw-rw-r-- 1 owner owner 4285 Feb  9  2016 lnk-to-blnk.vbs
-rw-rw-r-- 1 owner owner  133 Feb 12  2016 alternates.yml
-rw-rw-r-- 1 owner owner  142 Feb 12  2016 blnk in Z d.cs.bat
-rw-rw-r-- 1 owner owner  199 Feb 17  2016 install.bat
```

NOTE: Files in bin and some other files are ignored by `.gitignore`.

### Changed
- Move files from bin to dist/share that are not transient and must be committed.
