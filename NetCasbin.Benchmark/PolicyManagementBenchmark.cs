using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetCasbin.Benchmark
{
    [SimpleJob(RunStrategy.Throughput, targetCount: 3)]
    [MinColumn, MaxColumn, MedianColumn]
    public class PolicyManagementBenchmark
    {
        private readonly Enforcer _enforcer;

        public PolicyManagementBenchmark()
        {
            _enforcer = new Enforcer("examples/rbac_model.conf");
        }

        [Params(10, 100, 1000)] 
        public int Times { get; set; }

        [Params(50, 500, 5000)] 
        public int PolicyCount { get; set; }

        [GlobalSetup(Targets = new []{nameof(AddPolicy), nameof(HasPolicy)})]
        public void GlobalSetupForHasPolicy()
        {
            var random = new Random();
            for (var i = 0; i < PolicyCount; i++)
            {
                _enforcer.AddPolicy($"group{random.Next(0, PolicyCount * 20)}", $"obj{i}", "read");
            }

            Console.WriteLine($"Already set {PolicyCount} policy.");
        }

        [Benchmark]
        public void HasPolicy()
        {
            for (var i = 0; i < Times; i++)
            {
                _enforcer.HasPolicy($"name{i}", $"data{i}", "read");
            }
        }

        [Benchmark]
        public void AddPolicy()
        {
            for (var i = 0; i < Times; i++)
            {
                _enforcer.AddPolicy($"name{i}", $"data{i}", "read");
            }
        }
    }
}
