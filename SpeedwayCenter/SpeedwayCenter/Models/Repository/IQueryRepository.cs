﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayCenter.Models.Repository
{
    public interface IQueryRepository<T> where T: class
    {
        T FindFirst(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}