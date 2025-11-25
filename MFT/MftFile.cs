using System.Collections.Generic;
using System.IO;
using MFT.Attributes;

namespace MFT;

public static class MftFile
{
    public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffff";

    public static Mft Load(string mftPath, bool recoverFromSlack)
    {
        return Load(mftPath, recoverFromSlack, null);
    }

    /// <summary>
    /// Load an MFT file with optional attribute filtering for performance optimization.
    /// </summary>
    /// <param name="mftPath">Path to the $MFT file</param>
    /// <param name="recoverFromSlack">Whether to recover entries from slack space</param>
    /// <param name="attributesToParse">Optional set of attribute types to parse. If null, all attributes are parsed.
    /// For file listing operations, use: StandardInformation, FileName, Data</param>
    /// <returns>Parsed MFT object</returns>
    public static Mft Load(string mftPath, bool recoverFromSlack, HashSet<AttributeType> attributesToParse)
    {
        if (File.Exists(mftPath) == false)
        {
            throw new FileNotFoundException($"'{mftPath}' not found");
        }

        using var fs = new FileStream(mftPath, FileMode.Open, FileAccess.Read);
        return new Mft(fs, recoverFromSlack, attributesToParse);
    }
}