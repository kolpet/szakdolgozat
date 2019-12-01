﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Szakdolgozat.Model;
using Szakdolgozat.ViewModel.Controls;
using Szakdolgozat.ViewModel.Structures;
using ViewModel.Adapters;

namespace Szakdolgozat.ViewModel.Pages
{
    public class PreferencesViewModel : ViewModelBase, IPageTurn
    {
        private PreferencesModel _model;

        public DelegateCommand RandomizeCommand { get; private set; }

        public DelegateCommand ToParticipantsCommand { get; private set; }

        public DelegateCommand ToAlgorithmCommand { get; private set; }

        public ObservableCollection<PreferenceCell> PreferenceGrid { get; set; }

        public List<int> ParticipantList { get; set; }

        public int PreferenceGridRows { get => _model.GetContext.TotalSize; }

        public int PreferenceGridColumns { get => _model.GetContext.GroupSize; }

        public event EventHandler NextPage;

        public event EventHandler PreviousPage;

        public PreferencesViewModel()
        {
            _model = new PreferencesModel();

            RandomizeCommand = new DelegateCommand(param => OnRandomizeCommand());
            ToParticipantsCommand = new DelegateCommand(param => OnToParticipantsCommand());
            ToAlgorithmCommand = new DelegateCommand(param => OnToAlgorithmCommand());

            PreferenceGrid = new ObservableCollection<PreferenceCell>();
            ParticipantList = new List<int>();
        }

        public void RefreshPage()
        {
            _model.Initialize();

            PreferenceGrid.Clear();
            ParticipantList.Clear();
            List<Preference> preferenceList = new PrioritiesAdapter(_model.GetContext.Priorities);
            for(int i = 0; i < PreferenceGridRows; i++)
            {
                ParticipantList.Add(preferenceList[i].ID);

                for(int j = 0; j < PreferenceGridColumns; j++)
                {
                    PreferenceGrid.Add(new PreferenceCell()
                    {
                        X = j,
                        Y = i,
                        Preferences = preferenceList[i].Preferences,
                        Selected = j
                    });
                    PreferenceGrid[j].SelectedChanged += new EventHandler(PreferenceCell_SelectedChanged);
                }
            }
        }

        private void PreferenceCell_SelectedChanged(object sender, EventArgs e)
        {
            PreferenceCell cell = (PreferenceCell)sender;
            _model.EditPreference(ParticipantList[cell.Y], cell.X, cell.Selected);
        }

        private void OnRandomizeCommand()
        {
            _model.Randomize();
            RefreshPage();
        }

        private void OnToParticipantsCommand()
        {
            PreviousPage?.Invoke(this, null);
        }

        private void OnToAlgorithmCommand()
        {
            try
            {
                _model.Validate();
                NextPage?.Invoke(this, null);
            }
            catch(Exception)
            {

            }
        }
    }
}