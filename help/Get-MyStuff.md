---
external help file: moduleDll.dll-Help.xml
Module Name: moduleDll
online version:
schema: 2.0.0
---

# Get-MyStuff

## SYNOPSIS

Testing the DLL module with inputs.

## SYNTAX

```
Get-MyStuff [-MyNumber] <Int32> -MyUri <String> [[-MyBool] <Boolean>] [[-MyDecimal] <Decimal>]
 [<CommonParameters>]
```

## DESCRIPTION

This module accepts a number, URL, boolean and decimal as input.

## EXAMPLES

### Example 1

```powershell
Get-MyStuff -MyNumber 7 -MyUri http://google.com
```

This passes a number and a URL Google.com.  

### Example 2

```powershell
Get-MyStuff -MyNumber 7 -MyUri http://google.com -MyBool $true -MyDecimal 123.456
```

This passes a number, the URL Google.com, the boolean value of true, and a decimal number of 123.456 .  

## PARAMETERS

### -MyBool

Boolean value

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MyDecimal

Decimal number

```yaml
Type: Decimal
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -MyNumber

Pass a integer number

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

### -MyUri
Pass a URL

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Int32

### System.String

### System.Boolean

### System.Decimal

## OUTPUTS

### moduleDll.MyStuff

## NOTES

## RELATED LINKS
