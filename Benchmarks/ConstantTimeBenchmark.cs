using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using TackleBigONetCore.Domain;
using TackleBigONetCore.Models;

namespace TackleBigONetCore.Benchmarks
{
    public class ConstantTimeBenchmark
    {
        private const int N = 999;

        private readonly Dictionary<int, LabRat> _labRatLookup;
        private readonly ILookup<int, Race> _raceLookup;
        private readonly IDictionary<int, Maze> _mazeLookup;

        public ConstantTimeBenchmark()
        {
            IList<LabRat> labRats = new List<LabRat>(N);

            for (int i = 0; i < N; i++)
            {
                labRats.Add(new LabRat
                {
                    TrackingId = i,
                    Color = (Color)(i % 3)
                });
            }
            
            _labRatLookup = labRats.ToDictionary(l => l.TrackingId);
            
            IList<Maze> mazes = new List<Maze>(N / 3);
            
            for (int i = 0; i < N; i++)
            {
                mazes.Add(new Maze
                {
                    MazeNumber = i,
                    Difficulty = (Difficulty)(i % 3),
                    LabRat1 = i,
                    LabRat2 = i + N / 3,
                    LabRat3 = i + N / 3 * 2
                });
            }

            _mazeLookup = mazes.ToDictionary(m => m.MazeNumber);

            IList<Race> races = new List<Race>(N);

            for (int i = 0; i < N; i++)
            {
                races.Add(new Race
                {
                    MazeNumber = i % (N / 3),
                    Participant = i,
                    FinishTime = (FinishTime)(i % 3)
                });
            }

            _raceLookup = races.ToLookup(r => r.MazeNumber, r => r);
        }

        [Benchmark]
        public int DummyBenchmark()
        {
            var race = _raceLookup[N / 3 - 1].ToArray();
            var labRat = _labRatLookup[race[2].Participant];
            var maze = _mazeLookup[race[2].MazeNumber];

            return maze.MazeNumber == race[2].MazeNumber && labRat.TrackingId == race[2].Participant
                ? (int) race[2].FinishTime
                : 0;
        }
    }
}