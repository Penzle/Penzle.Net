﻿namespace Penzle.Core.Models;

public class ApiOptions
{
    public ApiOptions(string project, string environment)
    {
        Project = project;
        Environment = environment;
    }

    /// <summary>
    ///     Default API Options include the Main project with Main environment
    /// </summary>
    public static ApiOptions Default => new(project: "main", environment: "main");

    /// <summary>
    ///     Specify the project.
    /// </summary>
    public string Project { get; set; }

    /// <summary>
    ///     Specify the Environment.
    /// </summary>
    public string Environment { get; set; }
}
