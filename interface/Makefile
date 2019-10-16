#!/usr/bin/env make

CS = "/cygdrive/c/Program Files (x86)/Mono/bin/csc"
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework/v4.0.30319/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework64/v3.5/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework/v3.5/csc.exe

MAIN ?= App

EXE: main.cs 
	@echo "compiling EXE ..."
	$(CS) /out:$(MAIN).exe /target:exe /main:$(MAIN) /platform:x86 main.cs

clean:
	@echo "cleaning up ..."
	rm -rf $(MAIN).exe

