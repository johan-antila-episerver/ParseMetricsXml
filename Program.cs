using System.Xml;
using Newtonsoft.Json;
using ParseMetricsXml;

var file = "";
var output = "";

if (args.Length > 0)
{
    file = args[0];
}

if (args.Length > 1)
{
    output = args[1];
}

if (string.IsNullOrEmpty(file))
{
    Console.WriteLine("You need to specify a file name");
    return;
}

var xml = File.ReadAllText(file);

// Load the metrics xml report 
XmlDocument xmlDoc = new XmlDocument();
xmlDoc.LoadXml(xml); 

// Use Newtonsoft.Json.JsonConvert to convert the XML to a JSON string
string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc).Replace("@","");

var internalObject = JsonConvert.DeserializeObject<Rootobject>(json);

var cyclomaticComplexity = internalObject.CodeMetricsReport.Targets.Target.Assembly.Metrics.Metric.Where(x => x.Name == "CyclomaticComplexity").Select(x => x.Value).FirstOrDefault();
var MaintainabilityIndex = internalObject.CodeMetricsReport.Targets.Target.Assembly.Metrics.Metric.Where(x => x.Name == "MaintainabilityIndex").Select(x => x.Value).FirstOrDefault();
var ClassCoupling = internalObject.CodeMetricsReport.Targets.Target.Assembly.Metrics.Metric.Where(x => x.Name == "ClassCoupling").Select(x => x.Value).FirstOrDefault();
var DepthOfInheritance = internalObject.CodeMetricsReport.Targets.Target.Assembly.Metrics.Metric.Where(x => x.Name == "DepthOfInheritance").Select(x => x.Value).FirstOrDefault();
var LinesOfCode = internalObject.CodeMetricsReport.Targets.Target.Assembly.Metrics.Metric.Where(x => x.Name == "SourceLines").Select(x => x.Value).FirstOrDefault();

Console.WriteLine("Project level statistics");
Console.WriteLine($"Cyclomatic Complexity: {cyclomaticComplexity}");
Console.WriteLine($"Maintainability Index: {MaintainabilityIndex}");
Console.WriteLine($"Class Coupling: {ClassCoupling}");
Console.WriteLine($"Depth of Inheritance: {DepthOfInheritance}");
Console.WriteLine($"Lines of Code: {LinesOfCode}");

// get header
foreach (var assemblyNamespace in internalObject.CodeMetricsReport.Targets.Target.Assembly.Namespaces.Namespace)
{
    var str = $"Namespace";
    foreach (var metric in assemblyNamespace.Metrics.Metric)
    {
        str += $"\t{metric.Name}";

    }
    Console.WriteLine(str);
    break;
}

var outputStr = "";
foreach (var assemblyNamespace in internalObject.CodeMetricsReport.Targets.Target.Assembly.Namespaces.Namespace)
{
    var str = $"{assemblyNamespace.Name}";
    foreach (var metric in assemblyNamespace.Metrics.Metric)
    {
        str += $"\t{metric.Value}";
        
    }
    Console.WriteLine(str);
    outputStr += str + Environment.NewLine;
}

if (!string.IsNullOrEmpty(output))
{
    File.WriteAllText(output, outputStr);
}

Console.ReadKey(true);