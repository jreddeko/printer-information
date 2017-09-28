# printer-information
Printer Information

The below executables can be found in the .\Executables\ folder.

## PrintInformationWinForm.exe

Shows the following information for the selected printer, paper source is where you will find the printer trays.
- Name
- Source
- Page Sizes

## print-info.exe

A console app that spits out all printer information found by querying the WIN32 api.

### Args:
- -o : "Output file to write results to"
- -n : "Name of printer" // if no name given all printers found will be queried.

eg. 
> print-info.exe -o output.txt
