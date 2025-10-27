---
external help file: moduleDll.dll-Help.xml
Module Name: moduleDll
online version:
schema: 2.0.0
---

# Test-SampleCmdlet2

## SYNOPSIS

Simple command to test DLL powershell module

## SYNTAX

```
Test-SampleCmdlet2 [-FavoriteNumber] <Int32> [[-FavoritePet] <String>] [<CommonParameters>]
```

## DESCRIPTION

This is a DLL test module commandlet that accepts 2 inputs and displays results.

## EXAMPLES

### Example 1

```powershell
PS C:\> Test-SampleCmdlet2 -FavoriteNumber 7 -FavoritePet Dog
```

This will pass the mandatory FavoriteNumber and Favorite pet.

## PARAMETERS

### -FavoriteNumber

Enter your favorite number

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

### -FavoritePet

Enter your favorite pet

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Accepted values: Cat, Dog, Horse, Cow, Pig, Chicken

Required: False
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Int32

### System.String

## OUTPUTS

### moduleDll.FavoriteStuff

## NOTES

## RELATED LINKS
