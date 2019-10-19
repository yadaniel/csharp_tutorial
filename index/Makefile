#!/usr/bin/env make

CS = "/cygdrive/c/Program Files (x86)/Mono/bin/csc"

PROG = index
MAIN ?= App

.PHONY: prog clean

prog: main.cs 
	$(CS) /out:$(PROG).exe /target:exe /main:$(MAIN) /platform:x86 main.cs

clean:
	@echo "cleaning up ..."
	rm -f $(PROG).exe

