using E_biblioteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_biblioteka.Services
{
    public interface ICartService
    {
        double Total();
        List<Book> Books();
    }
}