#!/usr/bin/env make

CS = "/cygdrive/c/Program Files (x86)/Mono/bin/csc"
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework64/v4.0.30319/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework/v4.0.30319/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework64/v3.5/csc.exe
#CS = /cygdrive/c/Windows/Microsoft.NET/Framework/v3.5/csc.exe

# REF_DLL1 = ./NationalInstruments.DAQmx.dll
# REF_DLL2 = ./NationalInstruments.Common.dll

EXE: main.cs 
	@echo "compiling EXE ..."
	$(CS) /out:property.exe /target:exe /main:Test /platform:x86 main.cs
	# $(CS) /out:property.exe /target:exe /main:Test /platform:x86 /reference:$(LIBS) /reference:$(DAQMX) /linkresource:$(DAQMX) /reference:$(COMMON) /linkresource:$(COMMON) main.cs cDAQ.cs

DLL: main.cs 
	@echo "compiling DLL ..."
	$(CS) /out:property.dll /target:library main.cs
	# $(CS) /target:library /reference:$(LIBS) /linkresource:$(DAQMX) /linkresource:$(COMMON) main.cs cDAQ.cs

clean:
	@echo "cleaning up ..."
	rm -rf property.{exe,dll}

