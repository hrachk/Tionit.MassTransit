 
using System.ComponentModel;

namespace Tionit.MassTransit.Consumer.Models
{
    
    public class CustomerModel
    {
        public int Id { get; set; }
       
        public string? CustomerName { get; set; }
        
        public int CustomerAge { get; set; }
    }
}
