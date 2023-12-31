﻿using ConsoleApp.Day5;

namespace ConsoleApp.Tests.Day5;

public class Tests
{
    [Fact]
    public void PartOne_Ok()
    {
        Parts.One("Day5/example.txt").Should().Be(35);
    }

    [Fact]
    public void PartTwo_Ok()
    {
        Parts.Two("Day5/example.txt").Should().Be(46);
    }

    [Fact]
    public void PartTwoBruteForce_Ok()
    {
        Parts.TwoBruteForce("Day5/example.txt").Should().Be(46);
    }

    [Fact]
    public void ChunkInterval_ExactIntersection()
    {
        var target = new Parts.Range(74, 14);

        var entry = new Parts.GardenMapEntry(
            new Parts.Range(45, 14),
            new Parts.Range(74, 14));

        var intersection = Parts.GetIntersection(target, entry.Source)!.Value;

        intersection.Should().BeEquivalentTo(new Parts.Range(74, 14));

        var newTargets = Parts.ChunkAndMapIntervalByIntersection(intersection, target, entry);

        newTargets.Count.Should().Be(1);
        newTargets[0].Should().BeEquivalentTo(new Parts.Range(45, 14));
    }

    [Fact]
    public void ChunkInterval_IntersectionAtAnd()
    {
        var target = new Parts.Range(74, 14);

        var entry = new Parts.GardenMapEntry(
            new Parts.Range(45, 23),
            new Parts.Range(77, 23));

        var intersection = Parts.GetIntersection(target, entry.Source)!.Value;

        intersection.Should().BeEquivalentTo(new Parts.Range(77, 11));

        var newTargets = Parts.ChunkAndMapIntervalByIntersection(intersection, target, entry);

        newTargets.Count.Should().Be(2);
        newTargets[0].Should().BeEquivalentTo(new Parts.Range(45, 11));
        newTargets[1].Should().BeEquivalentTo(new Parts.Range(74, 3));
    }

    [Fact]
    public void ChunkInterval_IntersectionAtStart()
    {
        var target = new Parts.Range(77, 30);

        var entry = new Parts.GardenMapEntry(
            new Parts.Range(45, 23),
            new Parts.Range(77, 23));

        var intersection = Parts.GetIntersection(target, entry.Source)!.Value;

        intersection.Should().BeEquivalentTo(new Parts.Range(77, 23));

        var newTargets = Parts.ChunkAndMapIntervalByIntersection(intersection, target, entry);

        newTargets.Count.Should().Be(2);
        newTargets[0].Should().BeEquivalentTo(new Parts.Range(45, 23));
        newTargets[1].Should().BeEquivalentTo(new Parts.Range(100, 7));
    }

    [Fact]
    public void ChunkInterval_IntersectionAtMiddle()
    {
        var target = new Parts.Range(74, 14);

        var entry = new Parts.GardenMapEntry(
            new Parts.Range(45, 4),
            new Parts.Range(77, 4));

        var intersection = Parts.GetIntersection(target, entry.Source)!.Value;

        intersection.Should().BeEquivalentTo(new Parts.Range(77, 4));

        var newTargets = Parts.ChunkAndMapIntervalByIntersection(intersection, target, entry);

        newTargets.Count.Should().Be(3);
        newTargets[0].Should().BeEquivalentTo(new Parts.Range(45, 4));
        newTargets[1].Should().BeEquivalentTo(new Parts.Range(74, 3));
        newTargets[2].Should().BeEquivalentTo(new Parts.Range(81, 7));
    }
}