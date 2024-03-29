﻿using Broadway_Boogie_Weggie.Services;
using Broadway_Boogie_Weggie.Services.Interfaces;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Broadway_Boogie_Weggie.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ICollisionService, CollisionService>();
            SimpleIoc.Default.Register<IAlgorithmService, AlgorithmService>();
            SimpleIoc.Default.Register<MainViewModel>();

        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

    }
}