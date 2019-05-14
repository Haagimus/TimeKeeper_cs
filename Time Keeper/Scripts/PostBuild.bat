SET config="..\Release\Time Keeper.exe.config"
SET app="..\Release\Time Keeper.exe"

IF EXIST "X:\PE\00 New Hire Stuff\" (
	SET dest="X:\PE\00 New Hire Stuff\"
) ELSE (
	MKDIR "X:\PE\00 New Hire Stuff\"
)

ECHO Copying application file
COPY %app% %dest% /Y
ECHO Copying config file
COPY %config% %dest% /Y