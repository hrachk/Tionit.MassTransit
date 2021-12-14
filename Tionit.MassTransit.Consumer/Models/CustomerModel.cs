using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tionit.MassTransit.Consumer.Models
{
    public interface ValueEntered
    {
        string Value { get; }
    }
    public class CustomerModel
    {
        public int Name { get; set; }
        public int Age  { get; set; }
    }
}
