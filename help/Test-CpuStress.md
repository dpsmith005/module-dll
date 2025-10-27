---
external help file: moduleDll.dll-Help.xml
Module Name: moduleDll
online version:
schema: 2.0.0
---

# Test-CpuStress

## SYNOPSIS

Stress test the CPU with the specified percent

## SYNTAX

```
Test-CpuStress [-Percent] <Int32> [<CommonParameters>]
```

## DESCRIPTION

Stress teach CPU on the system by the specified percent value.  A propmpt will be presented to stop the test.  The test will continue to run until a key is pressed.

## EXAMPLES

### Example 1

```powershell
PS C:\> Test-CpuStress -Percent 80
```

This will runn all CPU's around 80 percent for the duration the command is allowed to run.

### Example 2

```powershell
PS C:\> Test-CpuStress 90
```

This will runn all CPU's around 90 percent for the duration the command is allowed to run.

## PARAMETERS

### -Percent

This is the percent of cpu processing that will be consumed

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Int32

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS
