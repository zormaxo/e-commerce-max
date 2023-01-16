cd Services\Shop\API
wt -w 0 dotnet watch run
cd..
cd..
cd..
REM cd ApiGateways\OcelotApiGw
REM wt -w 0 dotnet watch run
REM cd..
REM cd..
cd client
REM wt -w 0 -d . powershell "ng s"
wt -w 0 -d . cmd /k "ng s"
pause