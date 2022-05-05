;#DropDB KraanDevExpressEasyTest

#Application KraanDevExpressWin
;#Application KraanDevExpressWeb
#IfDef KraanDevExpressWin

*Action About...
 
*Action OK

#EndIf

#IfDef KraanDevExpressWeb
!Action Navigation(My Details)
#EndIf
