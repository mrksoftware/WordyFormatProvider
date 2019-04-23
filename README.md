# WordyFormatProvider
Converts number in word representation (supported language: ITA)

Simple usage:

```c#
var f = WordyFormatProvider.Create("it-IT");
var numberInWords = string.Format(f, "{0:W}", 5);
// This will print: cinque
```

At the moment only italian language is supported
