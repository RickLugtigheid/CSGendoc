@echo off
set ARGS=""
IF /i "%1" == "help" (
	echo Usage:
	echo 	rungendoc.bat		- Runs csgendoc
	echo 	rungendoc.bat verbose	- Runs csgendoc in verbose mode
	EXIT /B
)
IF /i "%1" == "verbose" (
	set ARGS="-v"
)

..\..\CSGendoc.Console\bin\Debug\net6.0\csgendoc %ARGS%