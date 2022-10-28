cd Services\Shop\API
wt -w 0 dotnet watch run
cd..
cd..
cd..
cd ApiGateways\OcelotApiGw
wt -w 0 dotnet watch run
cd..
cd..
cd client
REM wt -w 0 -d . powershell "ng s"
wt -w 0 -d . cmd /k "ng s"
pause