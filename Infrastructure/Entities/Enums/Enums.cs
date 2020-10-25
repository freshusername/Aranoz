using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure
{
  public class Enums
  {
        public enum Season { Hot, Cold, Demiseason }
        public enum RoomType 
        { 
            Single, 
            Double, 
            Triple, 
            Quad, 
            Queen, 
            King, 
            Twin, 
            Studio, 
            Suite, 
            Apartments,
            [Display(Name = "Junior Suite")]
            JuniorSuite,
            [Display(Name = "President Suite")]
            PresidentSuite,
            [Display(Name = "Connecting rooms")]
            ConnectingRooms 
        }
  }
}
