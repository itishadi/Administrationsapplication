using System;

namespace Administrationsapplication.Services;

public class ApplicationService
{
    public static void CloseApplication()
    {
        Environment.Exit(0);
    }
}