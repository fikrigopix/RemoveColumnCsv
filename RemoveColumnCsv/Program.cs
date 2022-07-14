// See https://aka.ms/new-console-template for more information
using RemoveColumnCsv.Model;

var inputFUser = GetUserInput();

if (!File.Exists(inputFUser.PathCsvInput))
{
    Console.WriteLine("Could not find file " + inputFUser.PathCsvInput);
    return;
}

if (RemoveColumnByIndex(inputFUser.PathCsvInput, inputFUser.IndexWantToBeRemoved, inputFUser.PathCsvOutput))
{
    Console.WriteLine("Csv file created in : " + inputFUser.PathCsvOutput);
} 

Console.WriteLine("Finish, Press Enter to close the program");
Console.ReadLine();

static UserInput GetUserInput()
{
    var input = new UserInput();

    try
    {
        input.PathCsvInput = GetUserInputDetail("Path CSV Input");
        input.PathCsvOutput = GetUserInputDetail("Path CSV Output");
        input.IndexWantToBeRemoved = Convert.ToInt16(GetUserInputDetail("Remove Column Index"));
    }
    catch (Exception)
    {
        Console.WriteLine("Wrong input, Press Enter to close the program");
        Console.ReadLine();
        Environment.Exit(0);
    }
    return input;
}

static string GetUserInputDetail(string inputName)
{
    string input = string.Empty;
    while (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine();
        Console.WriteLine(inputName + " is required");
        Console.WriteLine("Enter " + inputName + ": ");
        input = Console.ReadLine();
    }
    return input;
}

static bool RemoveColumnByIndex(string path, int index, string pathOutput) //path = csv path file , index = index column to be removed
{
    bool success = true;
    try
    {
        List<string> lines = new();
        using (StreamReader reader = new StreamReader(path))
        {
            var line = reader.ReadLine();
            List<string> values = new List<string>();
            while (line != null)
            {
                values.Clear();
                var cols = line.Split(',');
                for (int i = 0; i < cols.Length; i++)
                {
                    if (i != index)
                        values.Add(cols[i]);
                }
                var newLine = string.Join(",", values);
                lines.Add(newLine);
                line = reader.ReadLine();
            }
        }

        if (!Directory.Exists(pathOutput))
        {
            Directory.CreateDirectory(pathOutput);
        }

        var filename = path.Substring(path.LastIndexOf('\\') + 1);
        var pathFileOutput = Path.Combine(pathOutput, filename);

        using (StreamWriter writer = new StreamWriter(pathFileOutput, false))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
    }
    catch (Exception ex)
    {
        success = false;
        Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
    }

    return success;
}