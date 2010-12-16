﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSTS;

namespace ORTS.Interlocking
{

   /// <summary>
   /// Defines possible states of the switch. For now, using underlying
   /// value to indicate position, instead of normal/reverse.
   /// </summary>
   public enum SwitchState
   {
      Position_0,
      Position_1,
      GoingTo_0,
      GoingTo_1,
      Trailed
   }


   /// <summary>
   /// Provides an abstraction atop an underlying switch object.
   /// </summary>
   public class InterlockingSwitch : InterlockingItem
   {


      public TrJunctionNode Switch { get; private set; }

      public InterlockingSwitch(Simulator simulator, TrJunctionNode switchObject)
         : base(simulator)
      {
         Switch = switchObject;
      }

      /// <summary>
      /// True when switch is manually locked by the dispatcher.
      /// </summary>
      public bool IsManuallyLocked { get; private set; }

      /// <summary>
      /// True when the switch has been locked as part of a route.
      /// </summary>
      public bool IsRouteLocked { get; private set; }


      /// <summary>
      /// True if locked for any reason.
      /// </summary>
      public bool IsLocked
      {
         get
         {
            bool returnValue = false;

            if (IsManuallyLocked)
            {
               returnValue = true;
            }

            if (IsRouteLocked)
            {
               returnValue = true;
            }


            return returnValue;
         }
      }


      /// <summary>
      /// Returns true when this switch can be thrown (commanded to change position)
      /// </summary>
      public bool CanThrow
      {
         get
         {
            bool returnValue = true;

            if (IsLocked)
            {
               returnValue = false;
            }

            return returnValue;
         }
      }


      /// <summary>
      /// Throws switch if possible.
      /// </summary>
      public void Throw()
      {
         if (CanThrow)
         {
            // TODO: switch throws instantaneously. there should be 
            // some intermediate "out of correspondence" state that matches
            // any switch animation duration

            if (Switch.SelectedRoute == 0)
            {
               Switch.SelectedRoute = 1;
            }
            else
            {
               Switch.SelectedRoute = 0;
            }
         }
      }

        
   }
}
