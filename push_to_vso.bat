@echo off

For /f "tokens=2-4 delims=/ " %%a in ('date /t') do (set mydate=%%c-%%a-%%b)
For /f "tokens=1-2 delims=/:" %%a in ('time /t') do (set mytime=%%a%%b)

set "dateTime=%mydate% at %mytime%"
set "commitMessage=Update from %dateTime%"
echo %commitMessage%

@echo on

git add .
git commit -m %commitMessage%
git push vso master