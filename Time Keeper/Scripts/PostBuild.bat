SET config="..\Release\Time Keeper.exe.config"
SET app="..\Release\Time Keeper.exe"

IF EXIST "X:\PE\00 New Hire Stuff\" (
	SET dest="X:\PE\00 New Hire Stuff\"
) ELSE (
	MKDIR "X:\PE\00 New Hire Stuff\"
)

COPY %config% %dest% /Y
COPY %app% %dest% /Y