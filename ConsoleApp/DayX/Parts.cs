﻿namespace ConsoleApp.DayX;

public static class Parts
{
    private static void ParseInput(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
    }

    public static int One(string fileName = "DayX/input.txt")
    {
        Console.WriteLine("Day x Task 1 answer is: " + 1);

        return 1;
    }

    public static int Two(string fileName = "DayX/input.txt")
    {
        Console.WriteLine("Day x Task 2 answer is: " + 1);

        return 1;
    }
}