# ParseMetricsXml
This is a simple tool to parse the xml output of the roslyn Code Metrics tool from XML to CSV so you can import it in Excel or whatever tool you fancy.
## Prerequisites
You need to install the [Microsoft.CodeAnalysis.NetAnalyzers](https://github.com/dotnet/roslyn-analyzers) and compile it or add this NuGet package to your solution. Follow [this guide](https://learn.microsoft.com/en-us/visualstudio/code-quality/how-to-generate-code-metrics-data?view=vs-2019) for instructions.
If you opt for the command line tool, make sure you run the output binary in a Visual Studio Command Prompt, if you forget that, you will likely see this error message:

`Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'System.Collections.Immutable, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The system cannot find the file specified.
   at Metrics.Program.RunAsync(String[] args, CancellationToken cancellationToken)
   at Metrics.Program.Main(String[] args) in C:\Users\Johan Antila\source\repos\roslyn-analyzers\src\Tools\Metrics\Program.cs:line 30`

## Usage
Assuming you ran the metrics.exe tool, you now have an xml output file, feed this to the program and it will output a csv file, you can ommit the csv file parameter.

`parsemetricsxml.exe myfile.xml myoutputfile.csv` 
