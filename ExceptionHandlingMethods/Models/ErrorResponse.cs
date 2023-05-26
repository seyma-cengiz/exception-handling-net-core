﻿using System.Text.Json;

namespace ExceptionHandlingMethods.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
