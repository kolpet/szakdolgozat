using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.Model
{
    public class AlgorithmModel : ModelBase
    {
        public AlgorithmModel()
        {
            Context.Algorithms = new List<AlgorithmData>();
            Context.AlgorithmsChanged = true;

            Initialize();
        }

        public void Initialize()
        {
            if(Context.PreferencesChanged)
            {
                Context.Algorithms.Clear();
                UnitSet set1 = new UnitSet(Context.Participants.Where(x => x.Group == MarriageGroup.Group1).Select(x => x.ID));
                UnitSet set2 = new UnitSet(Context.Participants.Where(x => x.Group == MarriageGroup.Group2).Select(x => x.ID));

                Context.StableMarriage = new StableMarriage(set1, set2, Context.Priorities);
                Context.PreferencesChanged = false;
            }
        }

        public void CreateGaleShapleyAlgorithm()
        {
            GaleShapleyAlgorithm algorithm = new GaleShapleyAlgorithm(Context.StableMarriage);

            Context.Algorithms.Add(new AlgorithmData{
                Name = "Gale-Shapley Algoritmus",
                Algorithm = algorithm,
                Element = algorithm
            });
            Context.AlgorithmsChanged = true;
        }

        public GeneticSettings CreateGeneticAlgorithm()
        {
            GeneticSettings settings = new GeneticSettings
            {
                SelectionRate = 0.2,
                AbsoluteSelection = 0.01,
                MutationChance = 0.05,
                StablePairWeight = 1,
                GroupHappinessWeight = 0,
                EgalitarianHappinessWeight = 0,
                Size = 1000,
                Generations = 1000
            };
            GeneticAlgorithm algorithm = new GeneticAlgorithm(Context.StableMarriage, settings);

            Context.Algorithms.Add(new AlgorithmData
            {
                Name = "Genetikus Algoritmus",
                Algorithm = algorithm,
                Element = algorithm
            });
            Context.AlgorithmsChanged = true;
            return settings;
        }

        public IGeneticSettings UpdateAlgorithm(int index, IGeneticSettings settings)
        {
            if(Context.Algorithms[index].Algorithm is GeneticAlgorithm)
            {
                GeneticSettings newSettings = new GeneticSettings
                {
                    SelectionRate = settings.SelectionRate,
                    AbsoluteSelection = settings.AbsoluteSelection,
                    MutationChance = settings.MutationChance,
                    StablePairWeight = settings.StablePairWeight,
                    GroupHappinessWeight = settings.GroupHappinessWeight,
                    EgalitarianHappinessWeight = settings.EgalitarianHappinessWeight,
                    Size = settings.Size,
                    Generations = settings.Generations
                };

                ((GeneticAlgorithm)Context.Algorithms[index].Algorithm).Settings = newSettings;

                settings = newSettings;
            }
            Context.AlgorithmsChanged = true;
            return settings;
        }

        public void DeleteAlgorithm(int index)
        {
            Context.Algorithms.RemoveAt(index);
            Context.AlgorithmsChanged = true;
        }
    }
}
