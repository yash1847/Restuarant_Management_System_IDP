﻿using Restuarant_Management_System_IDP.Models;

namespace Restuarant_Management_System_IDP.Repository.IRepository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}
