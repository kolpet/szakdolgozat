﻿using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Persistence;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Model.Events;
using Szakdolgozat.Persistence.Structures;

namespace Szakdolgozat.Model
{
    public partial class AppModel : ModelBase
    {
        public AppModel(PersistenceBase persistence)
        {
            Context.Persistence = persistence;
        }

        public void SaveData()
        {
            SaveAsData(null);
        }

        public void SaveAsData(string path)
        {
            SaveData data = new SaveData
            {
                Group1Name = Context.Group1Name,
                Group2Name = Context.Group2Name,
                Participants = Context.Participants.Select(x => new UnitSave
                {
                    Id = x.ID,
                    Name = x.Name,
                    Group = x.Group == MarriageGroup.Group1 ? 1 : 2
                }).ToList()
            };

            foreach(AlgorithmData algorithm in Context.Algorithms)
            {
                IAlgorithmVisitor visitor = new AlgorithmVisitorParam((x) => SaveGaleShapley(data, algorithm.Name),
                    (x) => SaveGenetic(data, algorithm.Name, x.Settings));
                visitor.Visit(algorithm.Element);
            }

            Context.Persistence.Save(data, path);
        }

        public SaveData LoadData(string path)
        {
            return Context.Persistence.Load(path);
        }

        private void SaveGaleShapley(SaveData data, string name)
        {
            GaleShapleyAlgorithmSave save = new GaleShapleyAlgorithmSave()
            {
                AlgorithmName = name
            };
            data.Algorithms.Add(save);
        }

        private void SaveGenetic(SaveData data, string name, GeneticSettings settings)
        {
            GeneticAlgorithmSave save = new GeneticAlgorithmSave()
            {
                AlgorithmName = name,
                AbsoluteSelection = settings.AbsoluteSelection,
                SelectionRate = settings.SelectionRate,
                MutationChance = settings.MutationChance,
                StablePairWeight = settings.StablePairWeight,
                GroupHappinessWeight = settings.GroupHappinessWeight,
                EgalitarianHappinessWeight = settings.EgalitarianHappinessWeight,
                Generations = settings.Generations,
                Size = settings.Size
            };
            data.Algorithms.Add(save);
        }
    }
}
