using Heavy.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Heavy.Domain.Core.Commands
{
    public abstract class Command:Message
    {
        public DateTime Timestamp { get; protected set; }

        public FluentValidation.Results.ValidationResult validationResult { get; set; }
        protected Command()
        {
            this.Timestamp = DateTime.Now;
        }
        public abstract bool IsValid();
    }
}
