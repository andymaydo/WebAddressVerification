﻿using Domain;
using System;
using System.Collections.Generic;
using System.Text;




namespace Aplication
{
    public interface ICountryCode
    {
        public List<CountryCode> CountryCodeGetAll();
    }
}