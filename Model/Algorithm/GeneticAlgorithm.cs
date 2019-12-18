using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model.Algorithm
{
    public class GeneticAlgorithm : GeneticAlgorithmBase<double>, IAlgorithmElement
    {
        public GeneticAlgorithm(StableMarriage stableMarriage, GeneticSettings settings) : base(stableMarriage, settings)
        {
        }

        /// <summary>
        /// Initializes the population of Species
        /// </summary>
        /// <returns>async Task</returns>
        protected override void Initialization()
        {
            _population = new List<Species<double>>();
            for (int i = 0; i < Settings.Size; i++)
            {
                UnitSet participants1 = new UnitSet(_stableMarriage.Units1.OrderBy(x => _random.Next()));
                UnitSet participants2 = new UnitSet(_stableMarriage.Units2.OrderBy(x => _random.Next()));

                //Randomized solution
                Solution genes = new Solution(participants1.Zip(participants2, (x, y) => new Tuple<int, int>(x, y)).ToList());
                double fitness = CalculateFitness(genes);

                _population.Add(new Species<double>()
                {
                    Genes = genes,
                    Fitness = fitness
                });
            }

            _population.OrderByDescending(x => x.Fitness);
        }

        protected override bool Condition()
        {
            return true;
        }

        /// <summary>
        /// Through random selection, deletes a set amount of Species, favoring more fit species
        /// </summary>
        protected override void Selection()
        {
            List<Species<double>> newPopulation = new List<Species<double>>(Settings.Size);

            for (int i = 0; i < Settings.AbsoluteSelection * Settings.Size; i++)
            {
                newPopulation.Add(_population[0]);
                _population.RemoveAt(0);
            }
            for(int i = 0; i < Settings.SelectionRate * Settings.Size; i++)
            {
                int selected = SelectSpecie();
                newPopulation.Add(_population[selected]);
                _population.RemoveAt(selected);
            }

            _population = newPopulation.OrderByDescending(x => x.Fitness).ToList();
        }

        /// <summary>
        /// Repopulates the population by crossing over surviving species and mutates them by chance
        /// </summary>
        protected override void CrossoverAndMutate()
        {
            List<Task<Species<double>>> crossoverTasks = new List<Task<Species<double>>>(); 

            for (int i = _population.Count(); i < Settings.Size; i++) {
                int pos1, pos2;
                do
                {
                    pos1 = SelectSpecie();
                    pos2 = SelectSpecie();
                } while (pos1 == pos2);

                crossoverTasks.Add(CrossoverAsync(_population[pos1], _population[pos2]));
            }

            while (crossoverTasks.Any())
            {
                Task<Species<double>> finished = Task.WhenAny(crossoverTasks).Result;
                crossoverTasks.Remove(finished);
                Species<double> newSpecie = finished.Result;

                if(_random.NextDouble() < Settings.MutationChance)
                {
                    newSpecie = MutationAsync(newSpecie).Result;
                }

                _population.Add(newSpecie);
            }

            _population.OrderByDescending(x => x.Fitness);
        }

        /// <summary>
        /// Crosses over the genes of two Species, returning the new Species
        /// </summary>
        /// <param name="a">The first specie</param>
        /// <param name="b">The second specie</param>
        /// <returns>The new Specie produced by crossover</returns>
        private Task<Species<double>> CrossoverAsync(Species<double> a, Species<double> b)
        {
            Solution newGenes = new Solution();

            int start = a.Genes[_random.Next() % a.Genes.Count].Item1;
            int point = start,
                pair;
            do
            {
                pair = b.Genes.GetPair(point);
                //Add pair from B
                newGenes.Add(new Tuple<int, int>(point, pair));
                point = a.Genes.GetPair(pair);
            } while (start != point);

            //Add remaining pairs from A
            newGenes.AddRange(a.Genes.Where(x => !newGenes.Select(y => y.Item1).Contains(x.Item1)));

            double newFitness = CalculateFitness(newGenes);
            return Task.FromResult(new Species<double>()
            {
                Genes = newGenes,
                Fitness = newFitness
            });
        }

        /// <summary>
        /// Mutates a specie's genes into a new, different one
        /// </summary>
        /// <param name="x">The specie to be mutated</param>
        /// <returns>The mutated specie</returns>
        private async Task<Species<double>> MutationAsync(Species<double> x)
        {
            await Task.Delay(0).ConfigureAwait(false);
            Solution newGenes = new Solution(x.Genes);

            /* Reverse Mutation
            int start = _random.Next() % _stableMarriage.GroupSize;
            int end = ((_random.Next() % (_stableMarriage.GroupSize - 1)) + start + 1) % _stableMarriage.GroupSize;
            if(start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            //while(start < end)
            {
                int temp = newGenes[start].Item2;
                newGenes[start] = new Tuple<int, int>(newGenes[start].Item1, newGenes[end].Item2);
                newGenes[end] = new Tuple<int, int>(newGenes[end].Item1, temp);
                start++;
                end--;
            }*/

            double selection = _random.Next() % (_stableMarriage.GroupSize - 1) + 1;
            double rate = selection / (double)_stableMarriage.GroupSize;
            List<int> shuffle = new List<int>();
            if(rate > 0)
            {
                for(int i = 0; i < newGenes.Count; i++)
                {
                    if(_random.NextDouble() < rate)
                    {
                        shuffle.Add(newGenes[i].Item2);
                        newGenes[i] = null;
                    }
                }
                for(int i = 0; i < newGenes.Count && shuffle.Count > 0; i++)
                {
                    if(newGenes[i] == null)
                    {
                        int select = _random.Next() % shuffle.Count;
                        newGenes[i] = new Tuple<int, int>(x.Genes[i].Item1, shuffle[select]);
                        shuffle.RemoveAt(select);
                    }
                }
            }

            double fitness = CalculateFitness(newGenes);
            return new Species<double>()
            {
                Genes = newGenes,
                Fitness = fitness
            };
        }

        /// <summary>
        /// Calculates the fitness of a solution depending on settings
        /// </summary>
        /// <param name="solution">The solution</param>
        /// <returns>The fitness</returns>
        protected override double CalculateFitness(Solution solution)
        {
            StablePairsEvaluation stablePairsEvaluation = new StablePairsEvaluation();
            GroupHappinessEvaluation groupHappinessEvaluation = new GroupHappinessEvaluation();
            EgalitarianHappinessEvaluation egalitarianHappinessEvaluation = new EgalitarianHappinessEvaluation();

            return stablePairsEvaluation.Evaluate(_stableMarriage, solution) * Settings.StablePairWeight +
                   groupHappinessEvaluation.Evaluate(_stableMarriage, solution) * Settings.GroupHappinessWeight -
                   egalitarianHappinessEvaluation.Evaluate(_stableMarriage, solution) * Settings.EgalitarianHappinessWeight;
        }

        /// <summary>
        /// Selects a random specie from the population, favoring the more fit ones
        /// </summary>
        /// <returns>The random specie selected</returns>
        private int SelectSpecie()
        {
            int sum = _population.Sum(x => Convert.ToInt32(Math.Round(x.Fitness)));
            int selected = 0;
            if (sum > 0)
            {
                int select = _random.Next() % sum;
                while (sum - Math.Round(_population[selected].Fitness) > select)
                {
                    sum -= Convert.ToInt32(Math.Round(_population[selected].Fitness));
                    selected++;
                }
            }
            return selected;
        }

        public void Accept(IAlgorithmVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
